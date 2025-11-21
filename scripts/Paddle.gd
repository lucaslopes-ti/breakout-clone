extends CharacterBody2D

## Script da Raquete (Paddle)
## Controla o movimento horizontal da raquete usando as teclas A/D ou setas

# Velocidade de movimento da raquete
@export var speed: float = 500.0

# Largura da tela (será definida no _ready)
var screen_width: float

func _ready():
	# Aguarda um frame para garantir que o viewport está pronto
	await get_tree().process_frame
	
	# Obtém a largura da viewport/tela de forma mais confiável
	var viewport_size = get_viewport().get_visible_rect().size
	if viewport_size.x == 0:
		# Fallback: usa o tamanho da janela do projeto
		viewport_size = DisplayServer.window_get_size()
	
	screen_width = viewport_size.x
	
	# Garante que a raquete começa na posição correta
	# A raquete deve estar na parte inferior da tela
	position.y = viewport_size.y - 50

func _physics_process(delta):
	# Obtém a entrada do jogador
	# Input.get_action_strength retorna um valor entre 0 e 1
	# Se move_left: -1, se move_right: +1, se nenhum: 0
	var direction = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
	
	# Define a velocidade horizontal
	velocity.x = direction * speed
	velocity.y = 0  # A raquete não se move verticalmente
	
	# Move a raquete
	move_and_slide()
	
	# Limita a raquete dentro dos limites da tela
	# clamp garante que o valor fique entre min e max
	var half_width = $CollisionShape2D.shape.get_size().x / 2
	position.x = clamp(position.x, half_width, screen_width - half_width)
