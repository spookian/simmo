extends CharacterBody3D
class_name PlayerController

const SPEED = 5.0
const JUMP_VELOCITY = 5
@onready var cam = $Camera3D
@onready var mesh = $Mesh
@onready var animationPlayer = $Mesh/AnimationPlayer as AnimationPlayer
var state : PlayerState

var gravity = 16

func _ready():
	print(gravity)
	state = PlayerIdle.new(self)

func _physics_process(delta):
	state.update(delta)
	pass
	# Add the gravity.
	#if not is_on_floor():
		#velocity.y -= gravity * delta
#
	## Handle jump.
	#if Input.is_action_just_pressed("ui_accept") and is_on_floor():
		#velocity.y = JUMP_VELOCITY
#
	## Get the input direction and handle the movement/deceleration.
	## As good practice, you should replace UI actions with custom gameplay actions.
	#var input_dir = Input.get_vector("strafe_left", "strafe_right", "move_up", "move_down")
	#var cam_rot = Basis.from_euler(Vector3(0, -deg_to_rad(cam.disprot), 0))
	#var direction = (Vector3(input_dir.x, 0, input_dir.y) * cam_rot).normalized()
	#if direction:
		#velocity.x = direction.x * SPEED
		#velocity.z = direction.z * SPEED
	#else:
		#velocity.x = move_toward(velocity.x, 0, SPEED)
		#velocity.z = move_toward(velocity.z, 0, SPEED)
#
	#move_and_slide()
