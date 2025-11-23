# Guia Rápido de Migração - GDScript para C#

Este guia mostra como migrar seu projeto Breakout Clone de GDScript para C#.

## 📋 Checklist de Migração

### 1. Preparar o Ambiente
- [ ] Instalar Godot Engine .NET (não a versão Standard)
- [ ] Instalar .NET SDK 6.0 ou superior
- [ ] Verificar instalação: `dotnet --version`

### 2. Configurar o Projeto
- [ ] Abrir o projeto no Godot .NET
- [ ] Criar solução C#: **Project > Tools > C# > Create C# solution**
- [ ] Verificar se o arquivo `.csproj` foi criado

### 3. Substituir os Scripts

Para cada cena, siga estes passos:

#### Ball.tscn
1. Selecionar o nó `Ball` (CharacterBody2D)
2. No Inspector, clicar no ícone de script
3. Selecionar "Detach Script" (remover Ball.gd)
4. Clicar em "Attach Script"
5. Selecionar linguagem **C#**
6. Escolher o arquivo `csharp_version/scripts/Ball.cs`
7. Salvar

#### Paddle.tscn
1. Selecionar o nó `Paddle` (CharacterBody2D)
2. No Inspector, clicar no ícone de script
3. Selecionar "Detach Script" (remover Paddle.gd)
4. Clicar em "Attach Script"
5. Selecionar linguagem **C#**
6. Escolher o arquivo `csharp_version/scripts/Paddle.cs`
7. Verificar se o nó está no grupo "paddle"
8. Salvar

#### Block.tscn
1. Selecionar o nó `Block` (StaticBody2D)
2. No Inspector, clicar no ícone de script
3. Selecionar "Detach Script" (remover Block.gd)
4. Clicar em "Attach Script"
5. Selecionar linguagem **C#**
6. Escolher o arquivo `csharp_version/scripts/Block.cs`
7. Salvar

#### Main.tscn
1. Selecionar o nó `GameManager` (Node)
2. No Inspector, clicar no ícone de script
3. Selecionar "Detach Script" (remover GameManager.gd)
4. Clicar em "Attach Script"
5. Selecionar linguagem **C#**
6. Escolher o arquivo `csharp_version/scripts/GameManager.cs`
7. Verificar se o nó está no grupo "game_manager"
8. Verificar estrutura da UI:
   - `UI/ScoreLabel` (Label)
   - `UI/LivesLabel` (Label)
   - `UI/GameOverLabel` (Label)
   - `UI/RestartButton` (Button)
9. Verificar se existe `BlocksContainer` (Node2D)
10. Salvar

### 4. Verificar Grupos

Certifique-se de que os seguintes grupos estão configurados:

- [ ] `game_manager` - no nó GameManager
- [ ] `paddle` - no nó Paddle
- [ ] `blocks` - será adicionado automaticamente pelos blocos
- [ ] `walls` - nas paredes laterais
- [ ] `ceiling` - no teto (se houver)

### 5. Verificar Input Map

No **Project Settings > Input Map**, verifique se existem:

- [ ] `move_left` - tecla A ou Seta Esquerda
- [ ] `move_right` - tecla D ou Seta Direita

### 6. Testar o Jogo

- [ ] Compilar o projeto (F5)
- [ ] Verificar se a bola se move
- [ ] Verificar se a raquete responde aos controles
- [ ] Verificar se os blocos são destruídos
- [ ] Verificar se a pontuação aumenta
- [ ] Verificar se as vidas diminuem
- [ ] Verificar se o game over funciona
- [ ] Verificar se o botão de reiniciar funciona

## 🔍 Diferenças Importantes

### Nomes de Propriedades

| GDScript | C# |
|----------|-----|
| `is_moving` | `IsMoving` |
| `block_color` | `BlockColor` |
| `speed` | `Speed` |

### Métodos Comuns

| GDScript | C# |
|----------|-----|
| `get_node()` | `GetNode<T>()` |
| `get_node_or_null()` | `GetNodeOrNull<T>()` |
| `add_to_group()` | `AddToGroup()` |
| `is_in_group()` | `IsInGroup()` |
| `queue_free()` | `QueueFree()` |
| `queue_redraw()` | `QueueRedraw()` |

### Constantes

| GDScript | C# |
|----------|-----|
| `Color.WHITE` | `Colors.White` |
| `Color.RED` | `Colors.Red` |
| `Vector2.ZERO` | `Vector2.Zero` |
| `TAU` | `Mathf.Tau` |

### Await/Async

| GDScript | C# |
|----------|-----|
| `await get_tree().process_frame` | `await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame)` |
| `await get_tree().create_timer(1.0).timeout` | `await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout)` |

## ⚠️ Problemas Comuns

### Erro: "Cannot find type"
- **Solução**: Certifique-se de estar usando Godot .NET, não a versão Standard

### Erro: "The name 'GetNode' does not exist"
- **Solução**: Verifique se o `using Godot;` está no topo do arquivo

### A bola não se move
- **Solução**: Verifique se o script está anexado ao nó correto e se é um `CharacterBody2D`

### Os blocos não são destruídos
- **Solução**: Verifique se os blocos estão no grupo "blocks" e se o GameManager está no grupo "game_manager"

### Erro de compilação
- **Solução**: Tente recriar a solução: **Project > Tools > C# > Create C# solution**

## 📚 Próximos Passos

Após a migração bem-sucedida:

1. Teste todas as funcionalidades
2. Compare o comportamento com a versão GDScript
3. Ajuste parâmetros se necessário
4. Considere adicionar novos recursos aproveitando as capacidades do C#

## 🎯 Benefícios da Versão C#

- Melhor integração com ferramentas .NET
- Acesso a bibliotecas .NET
- Melhor suporte de IDE (IntelliSense, debugging)
- Performance potencialmente melhor em alguns casos
- Facilita integração com outros sistemas .NET

---

**Boa sorte com a migração!** 🚀

