extends PlayerFall
class_name PlayerJump

const JUMP_VELOCITY = 6
const RELEASE_MULTIPLIER = 0.40

# Called when the node enters the scene tree for the first time.
func begin():
	super()
	gravity = 16.0
	player.velocity.y = JUMP_VELOCITY
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func update(delta):
	if (player.velocity.y <= 0):
		player.state = PlayerFall.new(player)
		queue_free()
	else:
		if (not Input.is_action_pressed("ui_accept")):
			player.velocity.y *= RELEASE_MULTIPLIER
			player.state = PlayerFall.new(player)
			queue_free()
	
	super(delta)
	pass
