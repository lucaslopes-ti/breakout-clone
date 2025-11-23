# Breakout Clone - Versão C#

Esta é a versão convertida do jogo Breakout Clone de GDScript para C#.

## 📋 Sobre a Conversão

Todos os scripts do jogo foram convertidos de GDScript para C#, mantendo a mesma funcionalidade e lógica do jogo original. A conversão inclui:

- **Ball.cs** - Controla o movimento da bola, colisões e rebatidas
- **Block.cs** - Define o comportamento básico de um bloco destruível
- **Paddle.cs** - Controla o movimento horizontal da raquete
- **GameManager.cs** - Gerencia pontuação, vidas, fim de jogo e criação de blocos

## 🔧 Pré-requisitos

Para usar esta versão em C#, você precisa:

1. **Godot Engine 4.5 ou superior** com suporte a .NET
   - Download: https://godotengine.org/download
   - **IMPORTANTE**: Você precisa da versão **.NET** do Godot, não a versão Standard
   - A versão .NET permite usar C# no Godot

2. **.NET SDK 6.0 ou superior**
   - Download: https://dotnet.microsoft.com/download
   - Verifique a instalação com: `dotnet --version`

## 📦 Como Usar

### Passo 1: Configurar o Projeto Godot para C#

1. Abra o projeto no Godot Engine (versão .NET)
2. Vá em **Project > Project Settings > General > Application > Config > Name**
3. Certifique-se de que o projeto está configurado para usar C#

### Passo 2: Adicionar os Scripts C#

1. No Godot, navegue até a pasta `scripts` do seu projeto
2. Para cada cena que usa GDScript:
   - Selecione o nó na cena
   - No Inspector, clique no ícone de script ao lado do nome do script
   - Selecione "Detach Script" para remover o script GDScript antigo
   - Clique em "Attach Script" novamente
   - Selecione a linguagem **C#** (não GDScript)
   - Escolha o arquivo correspondente da pasta `csharp_version/scripts/`
   - Exemplo:
     - `Ball.tscn` → use `Ball.cs`
     - `Paddle.tscn` → use `Paddle.cs`
     - `Block.tscn` → use `Block.cs`
     - `Main.tscn` → use `GameManager.cs` no nó GameManager

### Passo 3: Configurar as Cenas

#### Ball.tscn
- O nó raiz deve ser um `CharacterBody2D`
- Adicione um `CollisionShape2D` como filho
- Configure o script para usar `Ball.cs`

#### Paddle.tscn
- O nó raiz deve ser um `CharacterBody2D`
- Adicione um `CollisionShape2D` como filho
- Configure o script para usar `Paddle.cs`
- Adicione o nó ao grupo "paddle"

#### Block.tscn
- O nó raiz deve ser um `StaticBody2D`
- Adicione um `CollisionShape2D` como filho
- Adicione um `ColorRect` como filho (opcional, para visual)
- Configure o script para usar `Block.cs`

#### Main.tscn
- Deve ter um nó `GameManager` (tipo `Node`) na raiz
- Configure o script para usar `GameManager.cs`
- Adicione o nó ao grupo "game_manager"
- Estrutura sugerida:
  ```
  Main (Node)
  ├── GameManager (Node) - Script: GameManager.cs
  │   ├── UI (Control)
  │   │   ├── ScoreLabel (Label)
  │   │   ├── LivesLabel (Label)
  │   │   ├── GameOverLabel (Label)
  │   │   └── RestartButton (Button)
  │   └── BlocksContainer (Node2D)
  ├── Paddle (CharacterBody2D) - Script: Paddle.cs
  └── Walls (StaticBody2D) - Grupo: "walls"
  ```

### Passo 4: Compilar o Projeto

1. No Godot, vá em **Project > Tools > C# > Create C# solution**
2. Isso criará os arquivos `.csproj` necessários
3. O Godot compilará automaticamente quando você executar o projeto

### Passo 5: Executar o Jogo

1. Pressione **F5** ou clique no botão Play
2. O jogo deve funcionar exatamente como a versão GDScript

## 🔄 Principais Diferenças entre GDScript e C#

### Sintaxe

**GDScript:**
```gdscript
extends CharacterBody2D
@export var speed: float = 350.0
func _ready():
    pass
```

**C#:**
```csharp
public partial class Ball : CharacterBody2D
{
    [Export]
    public float Speed { get; set; } = 350.0f;
    
    public override void _Ready()
    {
    }
}
```

### Acesso a Nós

**GDScript:**
```gdscript
var color_rect = $ColorRect
var label = get_node("UI/ScoreLabel")
```

**C#:**
```csharp
var colorRect = GetNodeOrNull<ColorRect>("ColorRect");
var label = GetNode<Label>("UI/ScoreLabel");
```

### Grupos

**GDScript:**
```gdscript
add_to_group("blocks")
if node.is_in_group("paddle"):
    pass
```

**C#:**
```csharp
AddToGroup("blocks");
if (node.IsInGroup("paddle"))
{
}
```

### Sinais e Timers

**GDScript:**
```gdscript
await get_tree().create_timer(1.0).timeout
await get_tree().process_frame
```

**C#:**
```csharp
await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
```

### Input

**GDScript:**
```gdscript
var direction = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
```

**C#:**
```csharp
float direction = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
```

## ⚠️ Notas Importantes

1. **Compatibilidade**: Esta versão C# requer Godot 4.5+ com suporte .NET
2. **Performance**: A versão C# pode ter performance ligeiramente diferente da GDScript
3. **Cenas**: As cenas (`.tscn`) permanecem as mesmas, apenas os scripts mudam
4. **Grupos**: Certifique-se de que os grupos estão configurados corretamente nas cenas:
   - "game_manager" - para o GameManager
   - "paddle" - para a raquete
   - "blocks" - para os blocos
   - "walls" - para as paredes
   - "ceiling" - para o teto

## 🐛 Solução de Problemas

### Erro: "Cannot find type CharacterBody2D"
- Certifique-se de estar usando a versão .NET do Godot
- Verifique se o projeto foi configurado corretamente para C#

### Erro: "The name 'GetNode' does not exist"
- Verifique se está herdando de `Node` ou `Node2D` corretamente
- Certifique-se de que os namespaces estão corretos (`using Godot;`)

### O jogo não compila
- Verifique se o .NET SDK está instalado
- Tente recriar a solução C#: **Project > Tools > C# > Create C# solution**
- Verifique se há erros no Output do Godot

### A bola não se move
- Verifique se o script `Ball.cs` está anexado ao nó correto
- Certifique-se de que o nó é um `CharacterBody2D`
- Verifique se o `CollisionShape2D` está configurado

## 📝 Estrutura de Arquivos

```
csharp_version/
├── scripts/
│   ├── Ball.cs          # Script da bola
│   ├── Block.cs          # Script dos blocos
│   ├── Paddle.cs         # Script da raquete
│   └── GameManager.cs    # Gerenciador do jogo
└── README.md             # Este arquivo
```

## 🎮 Funcionalidades

Todas as funcionalidades da versão GDScript foram mantidas:

- ✅ Movimento da bola com física realista
- ✅ Sistema de colisão e rebatida
- ✅ Controle da raquete com teclado
- ✅ Sistema de pontuação
- ✅ Sistema de vidas (3 vidas)
- ✅ Criação dinâmica de blocos
- ✅ Detecção de vitória/derrota
- ✅ Interface de usuário (UI)
- ✅ Botão de reiniciar
- ✅ Blocos coloridos por linha

## 📚 Recursos Adicionais

- [Documentação do Godot C#](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/index.html)
- [Guia de Migração GDScript para C#](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_differences.html)

## 📄 Licença

Este projeto é de código aberto e está disponível para uso livre.

---

**Desenvolvido como projeto acadêmico para as aulas de Programação de Jogos Digitais no SENAI.**

