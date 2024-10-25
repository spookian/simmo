extends Node

var defaultCursor = load("res://Assets/Sprites/cursor.png")
var selectedEntity

# the skill signals for a selection to be made; it will not execute without it
# but how will it know when the selection is made?
# and when will the selected entity get flushed?

# Called when the node enters the scene tree for the first time.
func _ready():
	#Input.set_custom_mouse_cursor(defaultCursor, 0, Vector2(79,5))
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
