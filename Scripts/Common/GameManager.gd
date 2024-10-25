extends Node

var is_client : bool = true

var clientManager : ClientManager
var serverManager : ServerManager

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if is_client == true:
		clientManager = ClientManager.new()
		add_child(clientManager)
	else:
		serverManager = ServerManager.new()
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
