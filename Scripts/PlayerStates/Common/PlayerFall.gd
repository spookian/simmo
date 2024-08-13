extends PlayerState
class_name PlayerFall

var gravity = 16.0
var max_velocity = 15
const AIR_DRAG = 10.0
const AIR_ACCEL = 15
const AIR_MAX_SPEED = 4

# Called when the node enters the scene tree for the first time.
func begin():
	player.animationPlayer.play("Jump")
	pass # Replace with function body.

func handleMovement(delta):
	var input_dir = Input.get_vector("strafe_left", "strafe_right", "move_up", "move_down")
	var cam_rot = Basis.from_euler(Vector3(0, -deg_to_rad(player.cam.disprot), 0))
	var direction = (Vector3(input_dir.x, 0, input_dir.y) * cam_rot)
	if (direction):
		player.mesh.rotation.y = atan2(direction.x, direction.z)
		player.velocity += direction * AIR_ACCEL
		
		var wiener = Vector2(player.velocity.x, player.velocity.z)
		if (wiener.length() > AIR_MAX_SPEED):
			var chode = wiener.normalized()
			player.velocity.x = chode.x * AIR_MAX_SPEED
			player.velocity.z = chode.y * AIR_MAX_SPEED
	else:
		#deceleration certainly
		if (player.velocity.x != 0):
			var s = -sign(player.velocity.x) * AIR_DRAG * delta
			if (abs(player.velocity.x + s) < 0.1):
				player.velocity.x = 0
			else:
				player.velocity.x += s
			
		if (player.velocity.z != 0):
			var s = -sign(player.velocity.z) * AIR_DRAG * delta
			if (abs(player.velocity.z + s) < 0.1):
				player.velocity.z = 0
			else:
				player.velocity.z += s
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func update(delta):
	handleMovement(delta)
	player.velocity.y -= gravity * delta
	player.move_and_slide()
	
	if (player.is_on_floor()):
		player.state = PlayerIdle.new(player)
		queue_free()
	
	pass
