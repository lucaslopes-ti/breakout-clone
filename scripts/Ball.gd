extends CharacterBody2D

## Script da Bola
## Controla o movimento da bola, colisões e rebatidas

# Velocidade inicial da bola (reduzida para melhor controle)
@export var speed: float = 350.0

# Referência ao GameManager para comunicação
var game_manager: Node

# Flag para controlar se a bola está em movimento
var is_moving: bool = false

func _ready():
	# Encontra o GameManager na cena
	game_manager = get_tree().get_first_node_in_group("game_manager")
	
	# Melhora o visual do ColorRect para parecer mais com uma bola
	var color_rect = $ColorRect
	if color_rect:
		# Mantém visível mas garante que a cor seja branca
		color_rect.color = Color.WHITE
		color_rect.visible = true
	
	# Força o redesenho para garantir que o círculo seja desenhado
	queue_redraw()
	
	# Define a direção inicial da bola (para cima e levemente para a direita)
	# Vector2 com valores aleatórios para tornar o jogo mais interessante
	# Limita o ângulo para evitar trajetórias muito horizontais
	var random_angle = randf_range(-0.3, 0.3)  # -17 a +17 graus (mais vertical)
	velocity = Vector2(sin(random_angle), -abs(cos(random_angle))) * speed
	
	# Aguarda um pouco antes de começar a mover (opcional)
	await get_tree().create_timer(1.0).timeout
	is_moving = true

func _draw():
	# Desenha um círculo branco para a bola (raio 10)
	draw_circle(Vector2.ZERO, 10.0, Color.WHITE)
	# Desenha uma borda preta para destacar
	draw_arc(Vector2.ZERO, 10.0, 0, TAU, 32, Color.BLACK, 1.0)

func _physics_process(delta):
	if not is_moving:
		return
	
	# Move a bola
	move_and_slide()
	
	# Verifica colisões
	handle_collisions()
	
	# Verifica se a bola saiu da tela (perdeu)
	check_out_of_bounds()

func handle_collisions():
	# Obtém todas as colisões que ocorreram neste frame
	for i in get_slide_collision_count():
		var collision = get_slide_collision(i)
		var collider = collision.get_collider()
		var normal = collision.get_normal()
		
		# Se colidiu com a raquete
		if collider.is_in_group("paddle"):
			# Calcula o ângulo de rebatida baseado na posição de impacto
			# Quanto mais longe do centro, mais inclinado o ângulo
			var paddle = collider
			var paddle_width = paddle.get_node("CollisionShape2D").shape.get_size().x
			var relative_intersect_x = (position.x - paddle.position.x) / (paddle_width / 2)
			
			# Limita o ângulo máximo (reduzido para evitar trajetórias muito horizontais)
			relative_intersect_x = clamp(relative_intersect_x, -1.0, 1.0)
			
			# Calcula nova direção (ângulo máximo de 45 graus para mais controle)
			var bounce_angle = relative_intersect_x * deg_to_rad(45)  # Máximo 45 graus
			
			# Garante que a bola sempre vá para cima após bater na raquete
			velocity = Vector2(sin(bounce_angle), -abs(cos(bounce_angle))) * speed
			
			# Garante velocidade vertical mínima para cima (evita trajetórias muito horizontais)
			var min_upward_speed = speed * 0.5  # Mínimo 50% da velocidade deve ser para cima
			if abs(velocity.y) < min_upward_speed:
				velocity.y = -min_upward_speed  # Força para cima
				# Ajusta horizontal proporcionalmente
				var remaining_speed = sqrt(speed * speed - min_upward_speed * min_upward_speed)
				velocity.x = clamp(velocity.x, -remaining_speed, remaining_speed)
				velocity = velocity.normalized() * speed
			
			# Pequeno aumento de velocidade para dificultar com o tempo (mais suave)
			speed = min(speed * 1.01, 600.0)  # Limita velocidade máxima e aumenta mais devagar
			return  # Evita processar outras colisões neste frame
		
		# Se colidiu com um bloco
		elif collider.is_in_group("blocks"):
			# Destrói o bloco
			collider.queue_free()
			
			# Notifica o GameManager
			if game_manager:
				game_manager.on_block_destroyed()
			
			# Determina a direção da colisão baseado na normal
			# Se a normal aponta para baixo (normal.y > 0), a bola veio de cima
			# Se a normal aponta para cima (normal.y < 0), a bola veio de baixo
			
			# Reflete a velocidade baseado na normal da colisão
			var reflected_velocity = velocity.bounce(normal)
			
			# Se a bola estava indo para cima e colidiu com a parte de baixo do bloco
			# ou se a normal indica que deve ir para baixo, força para baixo
			if normal.y > 0.3:  # Normal aponta para baixo (colisão vinda de cima)
				# A bola deve ir para baixo após colidir
				reflected_velocity.y = abs(reflected_velocity.y)  # Força componente Y positivo (para baixo)
			elif normal.y < -0.3:  # Normal aponta para cima (colisão vinda de baixo)
				# A bola deve ir para cima após colidir
				reflected_velocity.y = -abs(reflected_velocity.y)  # Força componente Y negativo (para cima)
			
			# Garante que a bola sempre tenha um componente vertical significativo
			# Isso evita que a bola fique muito horizontal e destrua vários blocos de uma vez
			var min_vertical_speed = speed * 0.4  # Mínimo 40% da velocidade deve ser vertical
			
			# Garante velocidade vertical mínima
			if abs(reflected_velocity.y) < min_vertical_speed:
				# Se a velocidade vertical é muito pequena, aumenta mantendo a direção
				var sign_y = 1 if reflected_velocity.y >= 0 else -1
				var horizontal_component = reflected_velocity.x
				reflected_velocity.y = sign_y * min_vertical_speed
				# Ajusta o componente horizontal proporcionalmente
				var remaining_speed = sqrt(speed * speed - min_vertical_speed * min_vertical_speed)
				reflected_velocity.x = clamp(horizontal_component, -remaining_speed, remaining_speed)
			
			# Normaliza e aplica velocidade mantendo a direção correta
			velocity = reflected_velocity.normalized() * speed
			return  # Evita processar outras colisões neste frame
		
		# Se colidiu com as paredes laterais ou teto
		elif collider.is_in_group("walls") or collider.is_in_group("ceiling"):
			# Reflete baseado na normal
			velocity = velocity.bounce(normal)
			velocity = velocity.normalized() * speed
			
			# Garante que após bater nas paredes, a bola mantenha movimento vertical
			# Evita que fique presa em loops horizontais
			var min_vertical_speed = speed * 0.2  # Mínimo 20% vertical
			if abs(velocity.y) < min_vertical_speed:
				# Se a velocidade vertical é muito pequena, aumenta levemente
				var sign_y = 1 if velocity.y >= 0 else -1
				velocity.y = sign_y * min_vertical_speed
				velocity = velocity.normalized() * speed
			
			return  # Evita processar outras colisões neste frame

func check_out_of_bounds():
	# Se a bola saiu pela parte inferior da tela
	var screen_height = get_viewport().get_visible_rect().size.y
	if screen_height == 0:
		screen_height = DisplayServer.window_get_size().y
	if position.y > screen_height + 50:
		# Notifica o GameManager que perdeu uma vida
		if game_manager:
			game_manager.on_ball_lost()
		
		# Reseta a posição da bola
		reset_ball()

func reset_ball():
	# Para a bola
	is_moving = false
	
	# Reseta a velocidade
	speed = 350.0
	
	# Posiciona a bola acima da raquete
	var paddle = get_tree().get_first_node_in_group("paddle")
	if paddle:
		position.x = paddle.position.x
		position.y = paddle.position.y - 30
	
	# Define nova direção aleatória (mais vertical para evitar trajetórias horizontais)
	var random_angle = randf_range(-0.3, 0.3)  # -17 a +17 graus
	velocity = Vector2(sin(random_angle), -abs(cos(random_angle))) * speed
	
	# Aguarda antes de começar a mover novamente
	await get_tree().create_timer(1.0).timeout
	is_moving = true
