extends EntityState
class_name PlayerRoll

var timer : float
var savedDirection : Vector3
const ROLL_SPEED = 16
const ROLL_DECEL = 50
const ROLL_ROTSPEED = 0.45
const ROLL_DURATION = 0.3

func _init(player : PlayerController, direction: Vector3):
	if (direction):
		savedDirection = direction
	else:
		savedDirection.x = sin(player.mesh.rotation.y)
		savedDirection.z = cos(player.mesh.rotation.y)
		# NO. please store rotation in different variable instead of using model rotation
	super(player)
	pass

func begin():
	player.animationPlayer.play("Roll")
	player.velocity = savedDirection * ROLL_SPEED
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func update(delta):
	player.mesh.rotation.x += ROLL_ROTSPEED
	player.velocity = (savedDirection * ROLL_SPEED) - (savedDirection * ROLL_DECEL * timer)
	timer += delta
	player.move_and_slide()
	if timer >= ROLL_DURATION:
		player.mesh.rotation.x = 0
		change_state( PlayerIdle.new(player) )
	
	if not player.is_on_floor():
		player.mesh.rotation.x = 0
		change_state( PlayerFall.new(player) )
	pass
