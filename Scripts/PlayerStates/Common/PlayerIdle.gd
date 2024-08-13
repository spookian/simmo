extends PlayerState
class_name PlayerIdle

const SPEED = 4.0
var lastDirection : Vector3

# Called when the node enters the scene tree for the first time.
func begin():
	player.animationPlayer.play("Stand")
	pass # Replace with function body.

# note to self: merge with air function?
func handleMovement(delta):
	var input_dir = Input.get_vector("strafe_left", "strafe_right", "move_up", "move_down")
	var cam_rot = Basis.from_euler(Vector3(0, -deg_to_rad(player.cam.disprot), 0))
	var direction = (Vector3(input_dir.x, 0, input_dir.y) * cam_rot)
	
	if direction:
		player.animationPlayer.play("Walk")
		player.mesh.rotation.y = atan2(direction.x, direction.z)
		player.velocity.x = direction.x * SPEED
		player.velocity.z = direction.z * SPEED
		
		lastDirection = direction
		print(lastDirection)
	else:
		player.animationPlayer.play("Stand")
		player.velocity.x = 0;
		player.velocity.z = 0
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func update(delta):
	# walk
	var air = not player.is_on_floor()
	if air:
		player.state = PlayerFall.new(player)
		queue_free()
		return

	if Input.is_action_just_pressed("ui_accept"):
		player.state = PlayerJump.new(player)
		queue_free()
		return

	handleMovement(delta)
	if Input.is_action_just_pressed("shift_skill"):
		player.state = PlayerRoll.new(player, lastDirection)
		queue_free()
		return
	
	player.move_and_slide()
	pass
