extends Camera3D
class_name ClientCamera

var baseVector = Vector3(0, 0, 10)
var real_position : Vector3 = position
var disprot = 0;
var baserot = 0 

var houseBuilder = false
var focus_point : Vector3

var dragcam = false
var mouse_origin : Vector2

var draghouse = false
var build_origin : Vector3
var build_far : Vector3
var lineDrawer

const LINE_WIDTH = 5.0

# Called when the node enters the scene tree for the first time.
func _ready():
	set_orthogonal(4.345, 0.05, 4000)
	pass # Replace with function body.

func drag_camera():
	var deltamove = 0
	if !dragcam:
		if (Input.is_mouse_button_pressed(MOUSE_BUTTON_RIGHT) and not draghouse):
			mouse_origin = get_viewport().get_mouse_position()
			dragcam = true
	else:
		deltamove = -((get_viewport().get_mouse_position()) - mouse_origin).x * 0.15
		if (!Input.is_mouse_button_pressed(MOUSE_BUTTON_RIGHT)):
			dragcam = false
			baserot += deltamove
			deltamove = 0
	
	disprot = baserot + deltamove
	
	pass

# this is a general purpose function that will be stored here until the prealpha refactor
# honestly? it might just stay here
func project_to_y(pos_y : float, mouse_pos = get_viewport().get_mouse_position()):
	var ray = project_ray_normal(mouse_pos) as Vector3
	var multiplier = (pos_y - project_ray_origin(mouse_pos).y) / ray.y
	return project_ray_origin(mouse_pos) + (ray * multiplier)

# this function is strictly for testing functionality and MUST be refactored later!

# generic function
#func generate_quad_from_endpoints(origin : Vector3, endpoint : Vector3):
	#var result_arr = Array()
	#result_arr.append(origin)
	#result_arr.append(Vector3(endpoint.x, 0, origin.z))
	#result_arr.append(endpoint)
	#result_arr.append(Vector3(origin.x, 0, endpoint.z))
	#
	#return result_arr

func drag_housebuild():
	var deltamove : Vector2
	
	if not draghouse:
		if Input.is_action_pressed("builder_lmb") and not dragcam:
			draghouse = true
			mouse_origin = get_viewport().get_mouse_position()
	else:
		#setup the second vertex
		build_origin = project_to_y(0, mouse_origin)
		build_far = project_to_y(0)
		lineDrawer.queue_redraw()
		if (!Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT)):
			draghouse = false
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
# note to self, move build logic out of camera
func _process(delta):
	drag_camera()
	drag_housebuild()
	
	var radrot = deg_to_rad(disprot)
	rotation.y = radrot
	position = baseVector * Basis.from_euler(Vector3(0, -radrot, 0)) + focus_point
	position.y = 4 + focus_point.y
	real_position = position
	rotation.x = deg_to_rad(-20)
	pass


func _on_node_2d_draw():
	if (draghouse and houseBuilder):
		var vec0 = unproject_position(build_origin)
		var vec1 = unproject_position(Vector3(build_far.x, 0, build_origin.z))
		var vec2 = unproject_position(build_far)
		var vec3 = unproject_position(Vector3(build_origin.x, 0, build_far.z))
		
		lineDrawer.draw_line(vec0, vec1, Color.RED, LINE_WIDTH)
		lineDrawer.draw_line(vec1, vec2, Color.RED, LINE_WIDTH)
		lineDrawer.draw_line(vec2, vec3, Color.RED, LINE_WIDTH)
		lineDrawer.draw_line(vec3, vec0, Color.RED, LINE_WIDTH)
	pass # Replace with function body.
