using Godot;

/// <summary>
/// Script da Raquete (Paddle)
/// Controla o movimento horizontal da raquete usando as teclas A/D ou setas
/// </summary>
public partial class Paddle : CharacterBody2D
{
    // Velocidade de movimento da raquete
    [Export]
    public float Speed { get; set; } = 500.0f;

    // Largura da tela (será definida no _ready)
    private float screenWidth;

    public override async void _Ready()
    {
        // Aguarda um frame para garantir que o viewport está pronto
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        // Obtém a largura da viewport/tela de forma mais confiável
        var viewportSize = GetViewport().GetVisibleRect().Size;
        if (viewportSize.X == 0)
        {
            // Fallback: usa o tamanho da janela do projeto
            viewportSize = DisplayServer.WindowGetSize();
        }

        screenWidth = viewportSize.X;

        // Garante que a raquete começa na posição correta
        // A raquete deve estar na parte inferior da tela
        Position = new Vector2(Position.X, viewportSize.Y - 50);
    }

    public override void _PhysicsProcess(double delta)
    {
        // Obtém a entrada do jogador
        // Input.GetActionStrength retorna um valor entre 0 e 1
        // Se move_left: -1, se move_right: +1, se nenhum: 0
        float direction = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");

        // Define a velocidade horizontal
        Velocity = new Vector2(direction * Speed, 0); // A raquete não se move verticalmente

        // Move a raquete
        MoveAndSlide();

        // Limita a raquete dentro dos limites da tela
        // Clamp garante que o valor fique entre min e max
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        float halfWidth = ((RectangleShape2D)collisionShape.Shape).Size.X / 2.0f;
        Position = new Vector2(Mathf.Clamp(Position.X, halfWidth, screenWidth - halfWidth), Position.Y);
    }
}

