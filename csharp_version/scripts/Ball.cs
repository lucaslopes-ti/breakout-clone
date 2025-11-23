using Godot;

/// <summary>
/// Script da Bola
/// Controla o movimento da bola, colisões e rebatidas
/// </summary>
public partial class Ball : CharacterBody2D
{
    // Velocidade inicial da bola (reduzida para melhor controle)
    [Export]
    public float Speed { get; set; } = 350.0f;

    // Referência ao GameManager para comunicação
    private GameManager gameManager;

    // Flag para controlar se a bola está em movimento
    public bool IsMoving { get; set; } = false;

    public override void _Ready()
    {
        // Encontra o GameManager na cena
        gameManager = GetTree().GetFirstNodeInGroup("game_manager") as GameManager;

        // Melhora o visual do ColorRect para parecer mais com uma bola
        var colorRect = GetNodeOrNull<ColorRect>("ColorRect");
        if (colorRect != null)
        {
            // Mantém visível mas garante que a cor seja branca
            colorRect.Color = Colors.White;
            colorRect.Visible = true;
        }

        // Força o redesenho para garantir que o círculo seja desenhado
        QueueRedraw();

        // Define a direção inicial da bola (para cima e levemente para a direita)
        // Vector2 com valores aleatórios para tornar o jogo mais interessante
        // Limita o ângulo para evitar trajetórias muito horizontais
        float randomAngle = GD.RandRange(-0.3f, 0.3f); // -17 a +17 graus (mais vertical)
        Velocity = new Vector2(Mathf.Sin(randomAngle), -Mathf.Abs(Mathf.Cos(randomAngle))) * Speed;

        // Aguarda um pouco antes de começar a mover (opcional)
        GetTree().CreateTimer(1.0f).Timeout += () => { IsMoving = true; };
    }

    public override void _Draw()
    {
        // Desenha um círculo branco para a bola (raio 10)
        DrawCircle(Vector2.Zero, 10.0f, Colors.White);
        // Desenha uma borda preta para destacar
        DrawArc(Vector2.Zero, 10.0f, 0, Mathf.Tau, 32, Colors.Black, 1.0f);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsMoving)
        {
            return;
        }

        // Move a bola
        MoveAndSlide();

        // Verifica colisões
        HandleCollisions();

        // Verifica se a bola saiu da tela (perdeu)
        CheckOutOfBounds();
    }

    private void HandleCollisions()
    {
        // Obtém todas as colisões que ocorreram neste frame
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            var collider = collision.GetCollider();
            var normal = collision.GetNormal();

            // Se colidiu com a raquete
            if (collider is Node node && node.IsInGroup("paddle"))
            {
                // Calcula o ângulo de rebatida baseado na posição de impacto
                // Quanto mais longe do centro, mais inclinado o ângulo
                var paddle = node as Node2D;
                var collisionShape = paddle.GetNode<CollisionShape2D>("CollisionShape2D");
                var paddleWidth = ((RectangleShape2D)collisionShape.Shape).Size.X;
                float relativeIntersectX = (Position.X - paddle.Position.X) / (paddleWidth / 2.0f);

                // Limita o ângulo máximo (reduzido para evitar trajetórias muito horizontais)
                relativeIntersectX = Mathf.Clamp(relativeIntersectX, -1.0f, 1.0f);

                // Calcula nova direção (ângulo máximo de 45 graus para mais controle)
                float bounceAngle = relativeIntersectX * Mathf.DegToRad(45.0f); // Máximo 45 graus

                // Garante que a bola sempre vá para cima após bater na raquete
                Velocity = new Vector2(Mathf.Sin(bounceAngle), -Mathf.Abs(Mathf.Cos(bounceAngle))) * Speed;

                // Garante velocidade vertical mínima para cima (evita trajetórias muito horizontais)
                float minUpwardSpeed = Speed * 0.5f; // Mínimo 50% da velocidade deve ser para cima
                if (Mathf.Abs(Velocity.Y) < minUpwardSpeed)
                {
                    Velocity = new Vector2(Velocity.X, -minUpwardSpeed); // Força para cima
                    // Ajusta horizontal proporcionalmente
                    float remainingSpeed = Mathf.Sqrt(Speed * Speed - minUpwardSpeed * minUpwardSpeed);
                    Velocity = new Vector2(Mathf.Clamp(Velocity.X, -remainingSpeed, remainingSpeed), Velocity.Y);
                    Velocity = Velocity.Normalized() * Speed;
                }

                // Pequeno aumento de velocidade para dificultar com o tempo (mais suave)
                Speed = Mathf.Min(Speed * 1.01f, 600.0f); // Limita velocidade máxima e aumenta mais devagar
                return; // Evita processar outras colisões neste frame
            }

            // Se colidiu com um bloco
            else if (collider is Node blockNode && blockNode.IsInGroup("blocks"))
            {
                // Destrói o bloco
                blockNode.QueueFree();

                // Notifica o GameManager
                if (gameManager != null)
                {
                    gameManager.OnBlockDestroyed();
                }

                // Determina a direção da colisão baseado na normal
                // Se a normal aponta para baixo (normal.y > 0), a bola veio de cima
                // Se a normal aponta para cima (normal.y < 0), a bola veio de baixo

                // Reflete a velocidade baseado na normal da colisão
                var reflectedVelocity = Velocity.Bounce(normal);

                // Se a bola estava indo para cima e colidiu com a parte de baixo do bloco
                // ou se a normal indica que deve ir para baixo, força para baixo
                if (normal.Y > 0.3f) // Normal aponta para baixo (colisão vinda de cima)
                {
                    // A bola deve ir para baixo após colidir
                    reflectedVelocity = new Vector2(reflectedVelocity.X, Mathf.Abs(reflectedVelocity.Y)); // Força componente Y positivo (para baixo)
                }
                else if (normal.Y < -0.3f) // Normal aponta para cima (colisão vinda de baixo)
                {
                    // A bola deve ir para cima após colidir
                    reflectedVelocity = new Vector2(reflectedVelocity.X, -Mathf.Abs(reflectedVelocity.Y)); // Força componente Y negativo (para cima)
                }

                // Garante que a bola sempre tenha um componente vertical significativo
                // Isso evita que a bola fique muito horizontal e destrua vários blocos de uma vez
                float minVerticalSpeed = Speed * 0.4f; // Mínimo 40% da velocidade deve ser vertical

                // Garante velocidade vertical mínima
                if (Mathf.Abs(reflectedVelocity.Y) < minVerticalSpeed)
                {
                    // Se a velocidade vertical é muito pequena, aumenta mantendo a direção
                    float signY = reflectedVelocity.Y >= 0 ? 1.0f : -1.0f;
                    float horizontalComponent = reflectedVelocity.X;
                    reflectedVelocity = new Vector2(horizontalComponent, signY * minVerticalSpeed);
                    // Ajusta o componente horizontal proporcionalmente
                    float remainingSpeed = Mathf.Sqrt(Speed * Speed - minVerticalSpeed * minVerticalSpeed);
                    reflectedVelocity = new Vector2(Mathf.Clamp(horizontalComponent, -remainingSpeed, remainingSpeed), reflectedVelocity.Y);
                }

                // Normaliza e aplica velocidade mantendo a direção correta
                Velocity = reflectedVelocity.Normalized() * Speed;
                return; // Evita processar outras colisões neste frame
            }

            // Se colidiu com as paredes laterais ou teto
            else if ((collider is Node wallNode && wallNode.IsInGroup("walls")) ||
                     (collider is Node ceilingNode && ceilingNode.IsInGroup("ceiling")))
            {
                // Reflete baseado na normal
                Velocity = Velocity.Bounce(normal);
                Velocity = Velocity.Normalized() * Speed;

                // Garante que após bater nas paredes, a bola mantenha movimento vertical
                // Evita que fique presa em loops horizontais
                float minVerticalSpeed = Speed * 0.2f; // Mínimo 20% vertical
                if (Mathf.Abs(Velocity.Y) < minVerticalSpeed)
                {
                    // Se a velocidade vertical é muito pequena, aumenta levemente
                    float signY = Velocity.Y >= 0 ? 1.0f : -1.0f;
                    Velocity = new Vector2(Velocity.X, signY * minVerticalSpeed);
                    Velocity = Velocity.Normalized() * Speed;
                }

                return; // Evita processar outras colisões neste frame
            }
        }
    }

    private void CheckOutOfBounds()
    {
        // Se a bola saiu pela parte inferior da tela
        float screenHeight = GetViewport().GetVisibleRect().Size.Y;
        if (screenHeight == 0)
        {
            screenHeight = DisplayServer.WindowGetSize().Y;
        }
        if (Position.Y > screenHeight + 50)
        {
            // Notifica o GameManager que perdeu uma vida
            if (gameManager != null)
            {
                gameManager.OnBallLost();
            }

            // Reseta a posição da bola
            ResetBall();
        }
    }

    public async void ResetBall()
    {
        // Para a bola
        IsMoving = false;

        // Reseta a velocidade
        Speed = 350.0f;

        // Posiciona a bola acima da raquete
        var paddle = GetTree().GetFirstNodeInGroup("paddle") as Node2D;
        if (paddle != null)
        {
            Position = new Vector2(paddle.Position.X, paddle.Position.Y - 30);
        }

        // Define nova direção aleatória (mais vertical para evitar trajetórias horizontais)
        float randomAngle = GD.RandRange(-0.3f, 0.3f); // -17 a +17 graus
        Velocity = new Vector2(Mathf.Sin(randomAngle), -Mathf.Abs(Mathf.Cos(randomAngle))) * Speed;

        // Aguarda antes de começar a mover novamente
        await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
        IsMoving = true;
    }
}

