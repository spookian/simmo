extends Node2D

@export var progBar : ProgressBar
@export var barContainer : Node2D
@export var animTree : AnimationTree
var dead = false
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	var b = (100 - progBar.value) / 100
	barContainer.position.x = -143 * b
	
	if (progBar.value == 0 and dead == false):
		dead = true
		animTree.set("parameters/StateMachine/conditions/dead", true)
	pass
