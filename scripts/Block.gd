extends StaticBody2D

## Script do Bloco
## Define o comportamento básico de um bloco destruível

# Pontos que este bloco vale quando destruído
@export var points: int = 10

# Cor do bloco (para visual)
@export var block_color: Color = Color.WHITE

func _ready():
	# Adiciona o bloco ao grupo "blocks" para facilitar detecção
	add_to_group("blocks")
	
	# Define a cor visual do bloco (se houver um ColorRect ou Sprite)
	var color_rect = $ColorRect
	if color_rect:
		color_rect.color = block_color

