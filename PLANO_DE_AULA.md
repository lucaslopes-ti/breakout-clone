# Plano de Aula - Breakout Clone
## Programação de Jogos Digitais - SENAI

**Duração Total:** 4-6 aulas (dependendo do ritmo da turma)  
**Objetivo:** Desenvolver um jogo completo estilo Breakout usando Godot Engine e GDScript/C#

---

## 📚 Módulo 1: Fundamentos de Programação e Godot Engine
**Duração:** 1-2 aulas

### 1.1 Conceitos Fundamentais de Programação

#### Teoria

**Variáveis e Tipos de Dados**
- **Variáveis**: Espaços na memória que armazenam valores
- **Tipos básicos em GDScript/C#**:
  - `int` / `int`: Números inteiros (ex: 10, -5, 0)
  - `float` / `float`: Números decimais (ex: 3.14, -2.5)
  - `bool` / `bool`: Valores verdadeiro/falso (true/false)
  - `String` / `string`: Texto (ex: "Hello World")
  - `Vector2` / `Vector2`: Par de coordenadas (x, y)

**Exemplo:**
```gdscript
# GDScript
var score: int = 0
var speed: float = 350.0
var is_moving: bool = false
var player_name: String = "Jogador"
var position: Vector2 = Vector2(100, 200)
```

```csharp
// C#
int score = 0;
float speed = 350.0f;
bool isMoving = false;
string playerName = "Jogador";
Vector2 position = new Vector2(100, 200);
```

**Operadores Matemáticos**
- Adição: `+`
- Subtração: `-`
- Multiplicação: `*`
- Divisão: `/`
- Módulo (resto): `%`
- Potência: `**` (GDScript) ou `Mathf.Pow()` (C#)

**Operadores de Comparação**
- Igual: `==`
- Diferente: `!=`
- Maior: `>`
- Menor: `<`
- Maior ou igual: `>=`
- Menor ou igual: `<=`

**Estruturas Condicionais**
```gdscript
# GDScript
if score > 100:
    print("Pontuação alta!")
elif score > 50:
    print("Pontuação média")
else:
    print("Continue tentando")
```

```csharp
// C#
if (score > 100)
{
    GD.Print("Pontuação alta!");
}
else if (score > 50)
{
    GD.Print("Pontuação média");
}
else
{
    GD.Print("Continue tentando");
}
```

**Loops (Repetições)**
```gdscript
# GDScript - Loop for
for i in range(10):
    print(i)  # Imprime 0 a 9

# Loop while
var count = 0
while count < 5:
    print(count)
    count += 1
```

```csharp
// C# - Loop for
for (int i = 0; i < 10; i++)
{
    GD.Print(i);  // Imprime 0 a 9
}

// Loop while
int count = 0;
while (count < 5)
{
    GD.Print(count);
    count++;
}
```

#### Exercícios Práticos

**Exercício 1.1: Calculadora Simples**
Crie um script que:
- Declare duas variáveis numéricas
- Realize as 4 operações básicas
- Imprima os resultados

**Exercício 1.2: Verificador de Idade**
Crie um script que:
- Receba uma idade
- Verifique se é maior de 18 anos
- Imprima mensagens diferentes para maior/menor de idade

**Exercício 1.3: Contador**
Crie um script que:
- Use um loop para contar de 1 a 100
- Imprima apenas números pares
- Some todos os números e imprima o total

---

### 1.2 Introdução ao Godot Engine

#### Teoria

**O que é o Godot?**
- Engine de jogos 2D e 3D gratuita e open-source
- Editor visual integrado
- Sistema de cenas (Scene System)
- Linguagens: GDScript (nativo) e C# (via .NET)

**Conceitos Fundamentais do Godot**

**1. Cenas (Scenes)**
- Uma cena é uma árvore de nós (nodes)
- Cada elemento do jogo é um nó
- Cenas podem ser salvas e reutilizadas
- Formato: `.tscn` (text scene)

**2. Nós (Nodes)**
- Unidade básica do Godot
- Cada nó tem uma função específica
- Nós podem ter filhos (hierarquia)
- Exemplos:
  - `Node`: Nó básico
  - `Node2D`: Nó 2D com posição
  - `CharacterBody2D`: Para objetos que se movem
  - `StaticBody2D`: Para objetos estáticos
  - `Label`: Para texto
  - `Button`: Para botões

**3. Scripts**
- Adicionam lógica aos nós
- GDScript: `.gd`
- C#: `.cs`
- Cada nó pode ter um script

**4. Inspector**
- Painel que mostra propriedades do nó selecionado
- Permite modificar valores sem código

**5. Sistema de Coordenadas**
- Origem (0, 0) no canto superior esquerdo
- Eixo X: aumenta da esquerda para direita
- Eixo Y: aumenta de cima para baixo
- Unidade: pixels

```
(0,0) ──────────────> X
  │
  │
  │
  │
  ▼
  Y
```

**Funções Principais do Godot**

**`_ready()`**
- Chamada quando o nó entra na árvore da cena
- Use para inicialização

**`_process(delta)`**
- Chamada a cada frame
- `delta`: tempo desde o último frame (em segundos)
- Use para lógica que precisa atualizar constantemente

**`_physics_process(delta)`**
- Chamada em intervalos fixos (60 vezes por segundo por padrão)
- Use para física e movimento

**Exemplo Básico:**
```gdscript
# GDScript
extends Node2D

var speed: float = 100.0

func _ready():
    print("Nó inicializado!")
    position = Vector2(100, 100)

func _process(delta):
    position.x += speed * delta
```

```csharp
// C#
using Godot;

public partial class MyNode : Node2D
{
    private float speed = 100.0f;

    public override void _Ready()
    {
        GD.Print("Nó inicializado!");
        Position = new Vector2(100, 100);
    }

    public override void _Process(double delta)
    {
        Position = new Vector2(Position.X + speed * (float)delta, Position.Y);
    }
}
```

#### Exercícios Práticos

**Exercício 1.4: Primeira Cena**
1. Crie uma nova cena
2. Adicione um nó `Node2D` como raiz
3. Adicione um script ao nó
4. No `_ready()`, imprima "Olá, Godot!"
5. Execute a cena (F5)

**Exercício 1.5: Objeto em Movimento**
1. Crie uma cena com um `CharacterBody2D`
2. Adicione um `CollisionShape2D` como filho
3. Configure uma forma de colisão retangular
4. Adicione um script que mova o objeto da esquerda para direita
5. Use `_physics_process()` e `move_and_slide()`

**Exercício 1.6: Contador Visual**
1. Crie uma cena com um `Label`
2. Adicione um script que conte de 0 a 100
3. Atualize o texto do label a cada segundo
4. Use `_process()` e controle o tempo com `delta`

---

## 📚 Módulo 2: Matemática e Física para Jogos
**Duração:** 1 aula

### 2.1 Vetores e Coordenadas

#### Teoria

**O que é um Vetor?**
- Representa direção e magnitude
- Em 2D: `Vector2(x, y)`
- Usado para posição, velocidade, aceleração

**Operações com Vetores**

**1. Soma de Vetores**
```gdscript
var v1 = Vector2(10, 20)
var v2 = Vector2(5, -10)
var resultado = v1 + v2  # (15, 10)
```

**2. Multiplicação por Escalar**
```gdscript
var vetor = Vector2(10, 20)
var escalado = vetor * 2  # (20, 40)
```

**3. Normalização**
- Torna o vetor com comprimento 1 (direção apenas)
```gdscript
var vetor = Vector2(10, 20)
var normalizado = vetor.normalized()
```

**4. Comprimento (Magnitude)**
```gdscript
var vetor = Vector2(3, 4)
var comprimento = vetor.length()  # 5.0 (hipotenusa)
```

**5. Distância entre Dois Pontos**
```gdscript
var ponto1 = Vector2(0, 0)
var ponto2 = Vector2(3, 4)
var distancia = ponto1.distance_to(ponto2)  # 5.0
```

**Sistema de Coordenadas no Breakout**
- Posição da bola: `position`
- Velocidade da bola: `velocity` (Vector2)
- Posição da raquete: `position.x` (horizontal)

#### Exercícios Práticos

**Exercício 2.1: Calculadora de Vetores**
Crie um script que:
- Declare dois vetores
- Calcule soma, subtração, multiplicação
- Calcule distância entre eles
- Normalize um vetor e mostre seu comprimento

**Exercício 2.2: Objeto Seguindo o Mouse**
1. Crie um `CharacterBody2D`
2. No `_process()`, calcule a direção do mouse
3. Mova o objeto em direção ao mouse
4. Use `get_global_mouse_position()` para obter posição do mouse

**Exercício 2.3: Movimento em Círculo**
1. Crie um objeto que se mova em círculo
2. Use funções trigonométricas: `sin()` e `cos()`
3. Fórmula: `x = raio * cos(ângulo)`, `y = raio * sin(ângulo)`
4. Incremente o ângulo a cada frame

---

### 2.2 Trigonometria Básica

#### Teoria

**Por que precisamos de trigonometria?**
- Calcular ângulos de rebatida
- Movimento em círculo
- Rotação de objetos
- Direção de movimento

**Funções Trigonométricas**

**Seno (sin) e Cosseno (cos)**
- Convertem ângulos em coordenadas
- `sin(ângulo)`: componente Y
- `cos(ângulo)`: componente X

**Exemplo: Movimento em Direção**
```gdscript
# Movimento em 45 graus
var angle = deg_to_rad(45)  # Converte graus para radianos
var direction = Vector2(cos(angle), sin(angle))
var velocity = direction * speed
```

**Conversão de Graus para Radianos**
- Godot usa radianos internamente
- 180 graus = π radianos
- 360 graus = 2π radianos (TAU)
- Funções: `deg_to_rad()` e `rad_to_deg()`

**Cálculo de Ângulo a partir de Vetor**
```gdscript
var vetor = Vector2(1, 1)
var angulo = atan2(vetor.y, vetor.x)  # Retorna em radianos
var angulo_graus = rad_to_deg(angulo)  # Converte para graus
```

**No Breakout:**
- Usamos trigonometria para calcular o ângulo de rebatida da bola
- Quanto mais longe do centro da raquete, mais inclinado o ângulo

#### Exercícios Práticos

**Exercício 2.4: Calculadora de Ângulos**
1. Crie um script que receba um ângulo em graus
2. Converta para radianos
3. Calcule seno e cosseno
4. Crie um vetor direção usando esses valores
5. Mostre o vetor normalizado

**Exercício 2.5: Bola com Direção Aleatória**
1. Crie uma bola que se move em direção aleatória
2. Gere um ângulo aleatório entre -45 e 45 graus
3. Use `sin()` e `cos()` para criar o vetor velocidade
4. Garanta que a bola sempre vá para cima (Y negativo)

**Exercício 2.6: Rebatida Simples**
1. Crie uma bola e uma parede
2. Quando a bola colidir, calcule o ângulo de reflexão
3. Use a normal da colisão para refletir a velocidade
4. Implemente `velocity.bounce(normal)`

---

## 📚 Módulo 3: Input e Controle
**Duração:** 1 aula

### 3.1 Sistema de Input do Godot

#### Teoria

**Input Actions (Ações de Entrada)**
- Configuradas em **Project Settings > Input Map**
- Permitem mapear teclas, botões, etc.
- Exemplo: "move_left", "move_right", "jump"

**Como Configurar:**
1. Vá em **Project > Project Settings**
2. Aba **Input Map**
3. Adicione nova ação (ex: "move_left")
4. Clique no "+" e escolha a tecla (ex: A ou Seta Esquerda)

**Verificando Input no Código**

**GDScript:**
```gdscript
# Verificar se tecla está pressionada
if Input.is_action_pressed("move_left"):
    # Move para esquerda
    position.x -= speed * delta

# Verificar força da ação (0.0 a 1.0)
var strength = Input.get_action_strength("move_right")
velocity.x = strength * speed

# Verificar se tecla foi pressionada neste frame
if Input.is_action_just_pressed("jump"):
    jump()
```

**C#:**
```csharp
// Verificar se tecla está pressionada
if (Input.IsActionPressed("move_left"))
{
    Position = new Vector2(Position.X - speed * (float)delta, Position.Y);
}

// Verificar força da ação (0.0 a 1.0)
float strength = Input.GetActionStrength("move_right");
Velocity = new Vector2(strength * speed, Velocity.Y);

// Verificar se tecla foi pressionada neste frame
if (Input.IsActionJustPressed("jump"))
{
    Jump();
}
```

**Input Direto (sem Actions)**
```gdscript
# GDScript
if Input.is_key_pressed(KEY_A):
    # Tecla A pressionada

# Mouse
var mouse_pos = get_global_mouse_position()
if Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT):
    # Botão esquerdo pressionado
```

```csharp
// C#
if (Input.IsKeyPressed(Key.A))
{
    // Tecla A pressionada
}

// Mouse
Vector2 mousePos = GetGlobalMousePosition();
if (Input.IsMouseButtonPressed(MouseButton.Left))
{
    // Botão esquerdo pressionado
}
```

**No Breakout:**
- Usamos `move_left` e `move_right` para controlar a raquete
- `get_action_strength()` permite movimento suave

#### Exercícios Práticos

**Exercício 3.1: Objeto Controlado por Teclado**
1. Crie um objeto que se move com as setas ou WASD
2. Configure as ações no Input Map
3. Use `get_action_strength()` para movimento suave
4. Limite o movimento dentro da tela

**Exercício 3.2: Objeto Seguindo o Mouse**
1. Crie um objeto que segue o cursor do mouse
2. Use `get_global_mouse_position()`
3. Calcule a direção e mova suavemente
4. Adicione um limite de velocidade máxima

**Exercício 3.3: Controles Múltiplos**
1. Crie um objeto com múltiplos controles:
   - Setas: movimento básico
   - WASD: movimento alternativo
   - Mouse: movimento direto
2. Implemente todos os métodos
3. Priorize mouse > WASD > Setas

---

## 📚 Módulo 4: Física e Colisões
**Duração:** 1-2 aulas

### 4.1 Tipos de Corpos Físicos

#### Teoria

**CharacterBody2D**
- Para objetos controlados pelo jogador
- Movimento manual via código
- Usa `move_and_slide()` para movimento com colisão
- Exemplo: jogador, raquete, bola

**StaticBody2D**
- Para objetos imóveis
- Não se move, mas pode colidir
- Exemplo: paredes, blocos, plataformas

**RigidBody2D**
- Para objetos com física automática
- Afetado por gravidade e forças
- Exemplo: objetos que caem, projéteis

**Area2D**
- Para detecção de área
- Não tem colisão física
- Usado para triggers, power-ups, etc.

**CollisionShape2D**
- Define a forma de colisão
- Deve ser filho do corpo físico
- Formas: Rectangle, Circle, Capsule, etc.

**No Breakout:**
- Bola: `CharacterBody2D`
- Raquete: `CharacterBody2D`
- Blocos: `StaticBody2D`
- Paredes: `StaticBody2D`

#### Exercícios Práticos

**Exercício 4.1: Objeto com Colisão**
1. Crie um `CharacterBody2D`
2. Adicione `CollisionShape2D` como filho
3. Configure uma forma retangular
4. Adicione movimento e teste colisão com paredes
5. Use `move_and_slide()`

**Exercício 4.2: Múltiplos Objetos Colidindo**
1. Crie vários objetos estáticos
2. Crie um objeto dinâmico que colide com eles
3. Detecte qual objeto foi colidido
4. Imprima o nome do objeto colidido

---

### 4.2 Detecção de Colisões

#### Teoria

**move_and_slide()**
- Move o objeto e detecta colisões automaticamente
- Retorna `true` se houve colisão
- Armazena informações das colisões

**Obtendo Informações de Colisão**

**GDScript:**
```gdscript
func _physics_process(delta):
    move_and_slide()
    
    # Verificar todas as colisões
    for i in get_slide_collision_count():
        var collision = get_slide_collision(i)
        var collider = collision.get_collider()  # Objeto colidido
        var normal = collision.get_normal()      # Normal da colisão
        
        # Verificar tipo do objeto
        if collider.is_in_group("blocks"):
            # Colidiu com um bloco
            collider.queue_free()  # Destrói o bloco
```

**C#:**
```csharp
public override void _PhysicsProcess(double delta)
{
    MoveAndSlide();
    
    // Verificar todas as colisões
    for (int i = 0; i < GetSlideCollisionCount(); i++)
    {
        var collision = GetSlideCollision(i);
        var collider = collision.GetCollider();  // Objeto colidido
        var normal = collision.GetNormal();      // Normal da colisão
        
        // Verificar tipo do objeto
        if (collider is Node node && node.IsInGroup("blocks"))
        {
            // Colidiu com um bloco
            node.QueueFree();  // Destrói o bloco
        }
    }
}
```

**Normal da Colisão**
- Vetor perpendicular à superfície
- Aponta para fora do objeto colidido
- Usado para calcular reflexão

**Reflexão (Bounce)**
```gdscript
# GDScript
var reflected = velocity.bounce(normal)
velocity = reflected.normalized() * speed
```

```csharp
// C#
var reflected = Velocity.Bounce(normal);
Velocity = reflected.Normalized() * speed;
```

**Grupos (Groups)**
- Permitem identificar objetos por categoria
- Adicione no Inspector ou via código:
  - `add_to_group("blocks")` (GDScript)
  - `AddToGroup("blocks")` (C#)
- Verifique: `is_in_group("blocks")` (GDScript) ou `IsInGroup("blocks")` (C#)

**No Breakout:**
- Grupos usados: "paddle", "blocks", "walls", "ceiling", "game_manager"

#### Exercícios Práticos

**Exercício 4.3: Bola Rebote Simples**
1. Crie uma bola que se move
2. Crie paredes nas bordas da tela
3. Quando colidir, reflete a velocidade
4. Use `velocity.bounce(normal)`

**Exercício 4.4: Destruição de Objetos**
1. Crie vários blocos estáticos
2. Adicione-os ao grupo "blocks"
3. Crie uma bola que, ao colidir, destrói o bloco
4. Use `queue_free()` para destruir

**Exercício 4.5: Rebatida com Ângulo**
1. Crie uma raquete horizontal
2. Quando a bola colidir, calcule o ângulo baseado na posição de impacto
3. Quanto mais longe do centro, mais inclinado
4. Use trigonometria para calcular nova direção

---

## 📚 Módulo 5: Interface de Usuário (UI)
**Duração:** 1 aula

### 5.1 Criando UI no Godot

#### Teoria

**Nós de UI**
- `Control`: Nó base para UI
- `Label`: Exibe texto
- `Button`: Botão clicável
- `VBoxContainer` / `HBoxContainer`: Organiza elementos vertical/horizontalmente
- `MarginContainer`: Adiciona margens

**Criando UI**
1. Adicione um nó `Control` como container
2. Adicione elementos filhos (Label, Button, etc.)
3. Configure posição e tamanho
4. Conecte sinais (ex: botão pressionado)

**Acessando Elementos UI**

**GDScript:**
```gdscript
@onready var score_label: Label = $UI/ScoreLabel
@onready var lives_label: Label = $UI/LivesLabel

func _ready():
    score_label.text = "Pontuação: 0"
    lives_label.text = "Vidas: 3"
```

**C#:**
```csharp
private Label scoreLabel;
private Label livesLabel;

public override void _Ready()
{
    scoreLabel = GetNode<Label>("UI/ScoreLabel");
    livesLabel = GetNode<Label>("UI/LivesLabel");
    
    scoreLabel.Text = "Pontuação: 0";
    livesLabel.Text = "Vidas: 3";
}
```

**Atualizando UI**
- Atualize sempre que o valor mudar
- Exemplo: quando pontuação aumenta, atualize o label

**Sinais de Botão**
- Conecte o sinal `pressed` do botão
- GDScript: Use o editor de sinais ou `button.pressed.connect(_on_button_pressed)`
- C#: Use `button.Pressed += OnButtonPressed;`

**No Breakout:**
- UI mostra: pontuação, vidas, game over, botão de reiniciar

#### Exercícios Práticos

**Exercício 5.1: Contador Visual**
1. Crie uma cena com um `Label`
2. Adicione um script que conte de 0 a 100
3. Atualize o texto do label a cada segundo
4. Formate o texto: "Contador: 50"

**Exercício 5.2: Painel de Informações**
1. Crie uma UI com:
   - Label de pontuação
   - Label de vidas
   - Label de tempo
2. Atualize os valores dinamicamente
3. Use `VBoxContainer` para organizar

**Exercício 5.3: Botão Funcional**
1. Crie um botão na UI
2. Conecte o sinal `pressed`
3. Quando clicado, reinicie a cena
4. Use `get_tree().reload_current_scene()`

---

## 📚 Módulo 6: Gerenciamento de Estado e Comunicação entre Objetos
**Duração:** 1 aula

### 6.1 Singleton e GameManager

#### Teoria

**O que é um GameManager?**
- Objeto que gerencia o estado global do jogo
- Controla: pontuação, vidas, fim de jogo
- Comunica com outros objetos

**Comunicação entre Objetos**

**1. Referência Direta**
```gdscript
# GDScript
var game_manager: Node

func _ready():
    game_manager = get_tree().get_first_node_in_group("game_manager")
    game_manager.on_block_destroyed()
```

```csharp
// C#
private GameManager gameManager;

public override void _Ready()
{
    gameManager = GetTree().GetFirstNodeInGroup("game_manager") as GameManager;
    gameManager.OnBlockDestroyed();
}
```

**2. Grupos**
- Use grupos para encontrar objetos
- `get_tree().get_first_node_in_group("paddle")`
- `GetTree().GetFirstNodeInGroup("paddle")` (C#)

**3. Sinais (Signals)**
- Sistema de eventos do Godot
- Um objeto emite um sinal, outros escutam
- Desacopla objetos

**Exemplo de Sinal:**
```gdscript
# GDScript - Definir sinal
signal block_destroyed

# Emitir sinal
block_destroyed.emit()

# Conectar sinal
func _ready():
    ball.block_destroyed.connect(_on_block_destroyed)
```

```csharp
// C# - Definir sinal
[Signal]
public delegate void BlockDestroyedEventHandler();

// Emitir sinal
EmitSignal(SignalName.BlockDestroyed);

// Conectar sinal
public override void _Ready()
{
    ball.BlockDestroyed += OnBlockDestroyed;
}
```

**No Breakout:**
- GameManager gerencia pontuação e vidas
- Bola notifica GameManager quando destrói bloco
- GameManager verifica condições de vitória/derrota

#### Exercícios Práticos

**Exercício 6.1: Sistema de Pontuação**
1. Crie um GameManager
2. Adicione variável de pontuação
3. Crie um método `add_points(points)`
4. Atualize a UI quando pontos mudarem

**Exercício 6.2: Comunicação entre Objetos**
1. Crie dois objetos: ObjetoA e ObjetoB
2. ObjetoA deve encontrar ObjetoB usando grupos
3. ObjetoA chama um método de ObjetoB
4. ObjetoB responde alterando sua cor

**Exercício 6.3: Sistema de Vidas**
1. Crie um sistema de vidas no GameManager
2. Quando vida chegar a 0, mostre "Game Over"
3. Adicione botão de reiniciar
4. Implemente reinício da cena

---

## 🎮 Projeto Final: Breakout Clone

### Checklist de Implementação

#### Fase 1: Estrutura Básica
- [ ] Criar cena principal (Main.tscn)
- [ ] Criar cena da bola (Ball.tscn)
- [ ] Criar cena da raquete (Paddle.tscn)
- [ ] Criar cena do bloco (Block.tscn)

#### Fase 2: Movimento
- [ ] Implementar movimento da bola
- [ ] Implementar controle da raquete
- [ ] Adicionar limites de tela

#### Fase 3: Colisões
- [ ] Colisão bola-parede (rebote)
- [ ] Colisão bola-raquete (rebote com ângulo)
- [ ] Colisão bola-bloco (destruição)

#### Fase 4: Gameplay
- [ ] Sistema de pontuação
- [ ] Sistema de vidas
- [ ] Criação de blocos
- [ ] Detecção de vitória/derrota

#### Fase 5: UI e Polimento
- [ ] Interface de usuário
- [ ] Tela de game over
- [ ] Botão de reiniciar
- [ ] Ajustes de dificuldade

---

## 📝 Exercícios de Revisão

### Exercício R1: Mini-Jogo de Rebatida
Crie um jogo simples onde:
- Uma bola se move automaticamente
- Uma raquete controlada pelo jogador rebate a bola
- Objetivo: manter a bola em jogo o máximo de tempo possível
- Mostre tempo de sobrevivência na tela

### Exercício R2: Destruidor de Blocos
Crie um jogo onde:
- Vários blocos são criados na tela
- Uma bola destrói os blocos ao colidir
- Conte quantos blocos foram destruídos
- Mostre mensagem quando todos forem destruídos

### Exercício R3: Sistema Completo
Combine os exercícios anteriores:
- Bola que se move
- Raquete controlável
- Blocos que são destruídos
- Sistema de pontuação
- Sistema de vidas
- UI completa

---

## 🎯 Objetivos de Aprendizado

Ao final deste plano de aula, os alunos devem ser capazes de:

1. ✅ Entender conceitos fundamentais de programação
2. ✅ Trabalhar com o Godot Engine
3. ✅ Implementar movimento e física
4. ✅ Detectar e responder a colisões
5. ✅ Criar interfaces de usuário
6. ✅ Gerenciar estado do jogo
7. ✅ Comunicar entre objetos
8. ✅ Desenvolver um jogo completo do zero

---

## 📚 Recursos Adicionais

### Documentação Oficial
- [Godot Docs](https://docs.godotengine.org/)
- [GDScript Reference](https://docs.godotengine.org/en/stable/tutorials/scripting/gdscript/index.html)
- [C# Documentation](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/index.html)

### Tutoriais Recomendados
- Godot 2D Game Tutorial (oficial)
- Heartbeast Action RPG Tutorial
- GDQuest Learn GDScript

### Ferramentas Úteis
- Godot Engine (gratuito)
- Visual Studio Code (com extensão Godot)
- GIMP ou Photoshop (para sprites, opcional)

---

## ⏱️ Cronograma Sugerido

**Aula 1-2:** Módulo 1 (Fundamentos)
- Teoria: 30 min
- Exercícios: 60 min
- Revisão: 30 min

**Aula 3:** Módulo 2 (Matemática)
- Teoria: 30 min
- Exercícios: 60 min
- Revisão: 30 min

**Aula 4:** Módulo 3 (Input)
- Teoria: 20 min
- Exercícios: 70 min
- Revisão: 30 min

**Aula 5-6:** Módulo 4 (Física)
- Teoria: 40 min
- Exercícios: 80 min
- Revisão: 30 min

**Aula 7:** Módulo 5 (UI)
- Teoria: 30 min
- Exercícios: 60 min
- Revisão: 30 min

**Aula 8:** Módulo 6 (GameManager)
- Teoria: 30 min
- Exercícios: 60 min
- Revisão: 30 min

**Aula 9-10:** Projeto Final
- Implementação: 120 min
- Testes e ajustes: 60 min

---

## 🎓 Avaliação

### Critérios de Avaliação do Projeto Final

**Funcionalidade (40%)**
- Bola se move corretamente
- Raquete controlável
- Colisões funcionam
- Blocos são destruídos
- Sistema de pontuação
- Sistema de vidas
- Detecção de vitória/derrota

**Código (30%)**
- Código organizado e comentado
- Uso adequado de variáveis e funções
- Sem código duplicado
- Boas práticas de programação

**Interface (20%)**
- UI funcional e clara
- Informações visíveis
- Botões funcionais

**Criatividade (10%)**
- Melhorias ou features extras
- Visual personalizado
- Mecânicas adicionais

---

**Boa sorte com o desenvolvimento! 🚀**

*Este plano de aula foi desenvolvido para o curso de Programação de Jogos Digitais no SENAI.*

