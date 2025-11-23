using Godot;

/// <summary>
/// Script do Bloco
/// Define o comportamento básico de um bloco destruível
/// </summary>
public partial class Block : StaticBody2D
{
    // Pontos que este bloco vale quando destruído
    [Export]
    public int Points { get; set; } = 10;

    // Cor do bloco (para visual)
    [Export]
    public Color BlockColor { get; set; } = Colors.White;

    public override void _Ready()
    {
        // Adiciona o bloco ao grupo "blocks" para facilitar detecção
        AddToGroup("blocks");

        // Define a cor visual do bloco (se houver um ColorRect ou Sprite)
        var colorRect = GetNodeOrNull<ColorRect>("ColorRect");
        if (colorRect != null)
        {
            colorRect.Color = BlockColor;
        }
    }
}

