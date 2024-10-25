class_name BuildingBlock

var width : float # x
var length : float # z
var position : Vector2

const HEIGHT = 3 # y

func _init(origin : Vector3, end : Vector3):
	var midpoint = (origin + end) / 2
	var position = Vector2(midpoint.x, midpoint.z)
	var dimensions = end - origin
	width = abs(dimensions.x)
	length = abs(dimensions.z)
	

func check_total_overlap(other : BuildingBlock):
	var selfleft = position.x - width/2
	var selfright = position.x + width/2
	var selfdown = position.y - length/2
	var selfup = position.y + length/2
	
	var otherleft = other.position.x - other.width/2
	var otherright = other.position.x + other.width/2
	var otherdown = other.position.y - other.length/2
	var otherup = other.position.y + other.length/2
	
	if (selfleft < otherleft):
		if (selfright > otherright):
			if (selfdown < otherdown):
				if (selfup > otherup):
					return true	
					
	return false

func create_csg() -> CSGBox3D:
	return CSGBox3D.new()

func create_collision() -> StaticBody3D:
	var main_coll = StaticBody3D.new() as StaticBody3D
	var coll_shape = CollisionShape3D.new()
	var shape = BoxShape3D.new() as BoxShape3D
	shape.size = Vector3(width, HEIGHT, length)
	coll_shape = shape
	
	main_coll.add_child(coll_shape)
	main_coll.position = Vector3(position.x, -HEIGHT/2, position.y)
	
	return main_coll
	
func create_save_data() -> PackedByteArray:
	return PackedByteArray()
