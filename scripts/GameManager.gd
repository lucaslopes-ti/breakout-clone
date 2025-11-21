extends Node

## GameManager - Controla a lógica principal do jogo
## Gerencia pontuação, vidas, fim de jogo e criação de blocos

# Pontuação atual do jogador
var score: int = 0

# Número de vidas restantes
var lives: int = 3

# Referências aos nós da UI
@onready var score_label: Label = $UI/ScoreLabel
@onready var lives_label: Label = $UI/LivesLabel
@onready var game_over_label: Label = $UI/GameOverLabel
@onready var restart_button: Button = $UI/RestartButton

# Referência ao container de blocos
@onready var blocks_container: Node2D = $BlocksContainer

# Cena da bola (para instanciar)
var ball_scene = preload("res://scenes/Ball.tscn")

# Referência à bola atual
var current_ball: Node2D = null

func _ready():
	# Adiciona ao grupo para facilitar acesso
	add_to_group("game_manager")
	
	# Esconde elementos de game over
	game_over_label.visible = false
	restart_button.visible = false
	
	# Cria os blocos
	create_blocks()
	
	# Atualiza a UI inicial
	update_ui()
	
	# Cria a primeira bola
	spawn_ball()

func create_blocks():
	## Cria uma grade de blocos na parte superior da tela
	
	# Carrega a cena do bloco
	var block_scene = preload("res://scenes/Block.tscn")
	
	# Configurações da grade
	var rows: int = 5  # Número de linhas
	var cols: int = 10  # Número de colunas
	var block_width: float = 80.0
	var block_height: float = 30.0
	var spacing: float = 10.0
	
	# Obtém o tamanho da tela de forma mais confiável
	# Aguarda um frame para garantir que o viewport está pronto
	await get_tree().process_frame
	var screen_width = get_viewport().get_visible_rect().size.x
	if screen_width == 0:
		# Fallback: usa o tamanho da janela do projeto
		screen_width = DisplayServer.window_get_size().x
	
	# Calcula o ponto de partida (centrado horizontalmente)
	var start_x = (screen_width - (cols * (block_width + spacing) - spacing)) / 2
	var start_y = 100.0
	
	# Cores diferentes para cada linha (opcional)
	var colors = [
		Color.RED,
		Color.ORANGE,
		Color.YELLOW,
		Color.GREEN,
		Color.BLUE
	]
	
	# Cria os blocos
	for row in range(rows):
		for col in range(cols):
			# Instancia um novo bloco
			var block = block_scene.instantiate()
			
			# Define a posição
			block.position = Vector2(
				start_x + col * (block_width + spacing),
				start_y + row * (block_height + spacing)
			)
			
			# Define a cor (opcional)
			if row < colors.size():
				var block_script = block.get_script()
				if block_script:
					block.block_color = colors[row]
					# Atualiza a cor visual
					var color_rect = block.get_node("ColorRect")
					if color_rect:
						color_rect.color = colors[row]
			
			# Adiciona ao container
			blocks_container.add_child(block)

func spawn_ball():
	## Cria uma nova bola na cena
	
	# Aguarda alguns frames para garantir que a raquete está posicionada
	await get_tree().process_frame
	await get_tree().process_frame
	
	# Remove a bola anterior se existir
	if current_ball:
		current_ball.queue_free()
	
	# Instancia nova bola
	current_ball = ball_scene.instantiate()
	
	# Posiciona acima da raquete
	var paddle = get_tree().get_first_node_in_group("paddle")
	if paddle:
		# Aguarda mais um frame para garantir que a raquete está na posição correta
		await get_tree().process_frame
		# Centraliza a bola na raquete
		current_ball.position = Vector2(paddle.position.x, paddle.position.y - 40)
	else:
		# Se não encontrar a raquete, posiciona no centro da tela
		var screen_size = get_viewport().get_visible_rect().size
		if screen_size.x == 0:
			screen_size = DisplayServer.window_get_size()
		current_ball.position = Vector2(screen_size.x / 2, screen_size.y - 100)
	
	# Adiciona à cena
	add_child(current_ball)

func on_block_destroyed():
	## Chamado quando um bloco é destruído
	
	# Adiciona pontos
	score += 10
	update_ui()
	
	# Aguarda um frame para garantir que o bloco foi removido da árvore
	# antes de verificar a condição de vitória
	await get_tree().process_frame
	check_win_condition()

func check_win_condition():
	## Verifica se o jogador venceu (destruiu todos os blocos)
	
	# Verifica diretamente os filhos do container de blocos
	# Remove blocos que estão marcados para serem removidos
	var valid_blocks = []
	for block in blocks_container.get_children():
		if is_instance_valid(block) and not block.is_queued_for_deletion():
			valid_blocks.append(block)
	
	# Se não há mais blocos válidos, o jogador venceu!
	if valid_blocks.size() == 0:
		# Jogador venceu!
		game_over_label.text = "VITÓRIA!\nPontuação: " + str(score)
		game_over_label.visible = true
		restart_button.visible = true
		
		# Para a bola
		if current_ball:
			current_ball.is_moving = false
		
		# Para o movimento da raquete também (opcional)
		var paddle = get_tree().get_first_node_in_group("paddle")
		if paddle:
			paddle.set_physics_process(false)

func on_ball_lost():
	## Chamado quando a bola sai da tela (perdeu uma vida)
	
	lives -= 1
	update_ui()
	
	if lives <= 0:
		# Game Over
		game_over_label.text = "GAME OVER!\nPontuação Final: " + str(score)
		game_over_label.visible = true
		restart_button.visible = true
		
		# Para a bola
		if current_ball:
			current_ball.is_moving = false
	else:
		# Reseta a bola
		if current_ball:
			current_ball.reset_ball()

func update_ui():
	## Atualiza os elementos da interface
	score_label.text = "Pontuação: " + str(score)
	lives_label.text = "Vidas: " + str(lives)

func _on_restart_button_pressed():
	## Reinicia o jogo
	get_tree().reload_current_scene()
