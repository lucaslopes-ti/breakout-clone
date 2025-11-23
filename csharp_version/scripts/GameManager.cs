using Godot;

/// <summary>
/// GameManager - Controla a lógica principal do jogo
/// Gerencia pontuação, vidas, fim de jogo e criação de blocos
/// </summary>
public partial class GameManager : Node
{
    // Pontuação atual do jogador
    public int Score { get; private set; } = 0;

    // Número de vidas restantes
    public int Lives { get; private set; } = 3;

    // Referências aos nós da UI
    private Label scoreLabel;
    private Label livesLabel;
    private Label gameOverLabel;
    private Button restartButton;

    // Referência ao container de blocos
    private Node2D blocksContainer;

    // Cena da bola (para instanciar)
    private PackedScene ballScene;

    // Referência à bola atual
    private Node2D currentBall;

    public override void _Ready()
    {
        // Adiciona ao grupo para facilitar acesso
        AddToGroup("game_manager");

        // Obtém referências aos nós da UI
        scoreLabel = GetNode<Label>("UI/ScoreLabel");
        livesLabel = GetNode<Label>("UI/LivesLabel");
        gameOverLabel = GetNode<Label>("UI/GameOverLabel");
        restartButton = GetNode<Button>("UI/RestartButton");

        // Referência ao container de blocos
        blocksContainer = GetNode<Node2D>("BlocksContainer");

        // Carrega a cena da bola
        ballScene = GD.Load<PackedScene>("res://scenes/Ball.tscn");

        // Esconde elementos de game over
        gameOverLabel.Visible = false;
        restartButton.Visible = false;

        // Conecta o sinal do botão
        restartButton.Pressed += OnRestartButtonPressed;

        // Cria os blocos
        CreateBlocks();

        // Atualiza a UI inicial
        UpdateUI();

        // Cria a primeira bola
        SpawnBall();
    }

    private async void CreateBlocks()
    {
        // Cria uma grade de blocos na parte superior da tela

        // Carrega a cena do bloco
        var blockScene = GD.Load<PackedScene>("res://scenes/Block.tscn");

        // Configurações da grade
        int rows = 5; // Número de linhas
        int cols = 10; // Número de colunas
        float blockWidth = 80.0f;
        float blockHeight = 30.0f;
        float spacing = 10.0f;

        // Obtém o tamanho da tela de forma mais confiável
        // Aguarda um frame para garantir que o viewport está pronto
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        float screenWidth = GetViewport().GetVisibleRect().Size.X;
        if (screenWidth == 0)
        {
            // Fallback: usa o tamanho da janela do projeto
            screenWidth = DisplayServer.WindowGetSize().X;
        }

        // Calcula o ponto de partida (centrado horizontalmente)
        float startX = (screenWidth - (cols * (blockWidth + spacing) - spacing)) / 2.0f;
        float startY = 100.0f;

        // Cores diferentes para cada linha (opcional)
        Color[] colors = new Color[]
        {
            Colors.Red,
            Colors.Orange,
            Colors.Yellow,
            Colors.Green,
            Colors.Blue
        };

        // Cria os blocos
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Instancia um novo bloco
                var block = blockScene.Instantiate<Block>();

                // Define a posição
                block.Position = new Vector2(
                    startX + col * (blockWidth + spacing),
                    startY + row * (blockHeight + spacing)
                );

                // Define a cor (opcional)
                if (row < colors.Length)
                {
                    block.BlockColor = colors[row];
                    // Atualiza a cor visual
                    var colorRect = block.GetNodeOrNull<ColorRect>("ColorRect");
                    if (colorRect != null)
                    {
                        colorRect.Color = colors[row];
                    }
                }

                // Adiciona ao container
                blocksContainer.AddChild(block);
            }
        }
    }

    private async void SpawnBall()
    {
        // Cria uma nova bola na cena

        // Aguarda alguns frames para garantir que a raquete está posicionada
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        // Remove a bola anterior se existir
        if (currentBall != null)
        {
            currentBall.QueueFree();
        }

        // Instancia nova bola
        currentBall = ballScene.Instantiate<Node2D>();

        // Posiciona acima da raquete
        var paddle = GetTree().GetFirstNodeInGroup("paddle") as Node2D;
        if (paddle != null)
        {
            // Aguarda mais um frame para garantir que a raquete está na posição correta
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            // Centraliza a bola na raquete
            currentBall.Position = new Vector2(paddle.Position.X, paddle.Position.Y - 40);
        }
        else
        {
            // Se não encontrar a raquete, posiciona no centro da tela
            var screenSize = GetViewport().GetVisibleRect().Size;
            if (screenSize.X == 0)
            {
                screenSize = DisplayServer.WindowGetSize();
            }
            currentBall.Position = new Vector2(screenSize.X / 2.0f, screenSize.Y - 100);
        }

        // Adiciona à cena
        AddChild(currentBall);
    }

    public async void OnBlockDestroyed()
    {
        // Chamado quando um bloco é destruído

        // Adiciona pontos
        Score += 10;
        UpdateUI();

        // Aguarda um frame para garantir que o bloco foi removido da árvore
        // antes de verificar a condição de vitória
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        // Verifica se o jogador venceu (destruiu todos os blocos)

        // Verifica diretamente os filhos do container de blocos
        // Remove blocos que estão marcados para serem removidos
        var validBlocks = new System.Collections.Generic.List<Node>();
        foreach (Node block in blocksContainer.GetChildren())
        {
            if (GodotObject.IsInstanceValid(block) && !block.IsQueuedForDeletion())
            {
                validBlocks.Add(block);
            }
        }

        // Se não há mais blocos válidos, o jogador venceu!
        if (validBlocks.Count == 0)
        {
            // Jogador venceu!
            gameOverLabel.Text = "VITÓRIA!\nPontuação: " + Score;
            gameOverLabel.Visible = true;
            restartButton.Visible = true;

            // Para a bola
            if (currentBall is Ball ball)
            {
                ball.IsMoving = false;
                ball.SetPhysicsProcess(false);
            }

            // Para o movimento da raquete também (opcional)
            var paddle = GetTree().GetFirstNodeInGroup("paddle") as Paddle;
            if (paddle != null)
            {
                paddle.SetPhysicsProcess(false);
            }
        }
    }

    public void OnBallLost()
    {
        // Chamado quando a bola sai da tela (perdeu uma vida)

        Lives--;
        UpdateUI();

        if (Lives <= 0)
        {
            // Game Over
            gameOverLabel.Text = "GAME OVER!\nPontuação Final: " + Score;
            gameOverLabel.Visible = true;
            restartButton.Visible = true;

            // Para a bola
            if (currentBall is Ball ball)
            {
                ball.IsMoving = false;
                ball.SetPhysicsProcess(false);
            }
        }
        else
        {
            // Reseta a bola
            if (currentBall is Ball ball)
            {
                ball.ResetBall();
            }
        }
    }

    private void UpdateUI()
    {
        // Atualiza os elementos da interface
        scoreLabel.Text = "Pontuação: " + Score;
        livesLabel.Text = "Vidas: " + Lives;
    }

    private void OnRestartButtonPressed()
    {
        // Reinicia o jogo
        GetTree().ReloadCurrentScene();
    }
}

