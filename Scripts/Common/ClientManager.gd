extends Node
class_name ClientManager

# preload builderhud

var cur_player : CharacterBody3D = null # set after creation of player
var camera : ClientCamera
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	camera = ClientCamera.new()
	var lineDrawer = Node2D.new()
	camera.add_child(lineDrawer)
	lineDrawer.draw.connect(camera._on_node_2d_draw)
	camera.lineDrawer = lineDrawer
	add_child(camera)
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
# eventually this will handle stuff like the main menu, creating a local world and syncing up with the server
# i fully intend for the client server managers to manage the whole game state
func _process(_delta: float) -> void:
	# resolve camera issues
	if (cur_player != null):
		camera.focus_point = cur_player.position
	pass
