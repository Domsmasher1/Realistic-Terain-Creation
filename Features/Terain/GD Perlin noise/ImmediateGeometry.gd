extends ImmediateGeometry


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _process(delta):
	self.begin(Mesh.PRIMITIVE_TRIANGLES)
	set_normal(Vector3(0,0,1))
	set_uv(Vector2(0,0))
	add_vertex(Vector3(-1, -1, 0))
	
	set_normal(Vector3(0,0,1))
	set_uv(Vector2(0,1))
	add_vertex(Vector3(-1, 1, 0))
	
	set_normal(Vector3(0,0,1))
	set_uv(Vector2(1,1))
	add_vertex(Vector3(1, 1, 0))
	
	set_normal(Vector3(0,0,1))
	set_uv(Vector2(1,0))
	add_vertex(Vector3(1, -1, 0))
	
	end()


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
