extends Spatial

var plane_mesh = PlaneMesh.new()

var surface_tool = SurfaceTool.new()

var array_plane

var data_tool = MeshDataTool.new()

var mesh_instance = MeshInstance.new()

var collision = CollisionShape.new()

# Called when the node enters the scene tree for the first time.
func _ready():
	randomize()
	var noise = OpenSimplexNoise.new()
	noise.period = rand_range(1, 100)
	noise.octaves = 6
	
	#var plane_mesh = PlaneMesh.new()
	plane_mesh.size = Vector2(400, 400)
	plane_mesh.subdivide_depth = 200
	plane_mesh.subdivide_width = 200
	
	#var surface_tool = SurfaceTool.new()
	surface_tool.create_from(plane_mesh, 0)
	
	array_plane = surface_tool.commit()
	
	#var data_tool = MeshDataTool.new()
	
	data_tool.create_from_surface(array_plane, 0)
	
	for i in range(data_tool.get_vertex_count()):
		var vertex = data_tool.get_vertex(i)
		vertex.y = noise.get_noise_3d(vertex.x, vertex.y, vertex.z) * 60
		
		if vertex.y > 120:
			data_tool.set_vertex_color(0, 1)
		else:
			data_tool.set_vertex_color(1, 0)
		
		data_tool.set_vertex(i, vertex)
		
	for i in range(array_plane.get_surface_count()):
		array_plane.surface_remove(i)
		
	data_tool.commit_to_surface(array_plane)
	surface_tool.begin(Mesh.PRIMITIVE_TRIANGLES)
	surface_tool.create_from(array_plane, 0)
	surface_tool.generate_normals()
	surface_tool.commit(array_plane)
	
	#var mesh_instance = MeshInstance.new()
	mesh_instance.mesh = surface_tool.commit()
	mesh_instance.create_trimesh_collision()
	#collision.shape = mesh_instance
	add_child(mesh_instance)
	

#func _process(delta):
	#breakpoint
#	print(array_plane)
#	print(data_tool.get_vertex_count())
#	data_tool.create_from_surface(array_plane, 0)
#	for i in range(data_tool.get_vertex_count()):
#		var vertex = data_tool.get_vertex(i)
#		var num = rand_range(0, 1000)
#		if num >= 800:
#			vertex.y = vertex.y - 1
#		data_tool.set_vertex(i, vertex)
#		i = i + 1
#	array_plane.surface_remove(0)
#	data_tool.commit_to_surface(array_plane)
#	#surface_tool.clear()
#	#surface_tool.begin(Mesh.PRIMITIVE_TRIANGLES)
#	surface_tool.create_from(array_plane, 0)
#	surface_tool.commit(array_plane)
#	surface_tool.generate_normals()
#	mesh_instance.mesh = surface_tool.commit()

func _on_Area_body_entered(body, extra_arg_0):
	var area = get_node("KinematicBody/Area/Spatial")
	var area_pos = area.get_translation()
	#print(array_plane)
	#print(data_tool.get_vertex_count())
	surface_tool.create_from(array_plane, 0)
	array_plane = surface_tool.commit()
	data_tool.create_from_surface(array_plane, 0)
	for i in range (data_tool.get_vertex_count()):
		var vertex = data_tool.get_vertex(i)
		if sqrt(vertex.x * vertex.x - area_pos.x * area_pos.x) <= 0.5 and sqrt(vertex.z * vertex.z - area_pos.z * area_pos.z) <= 0.5:
			vertex.y = vertex.y - 35
			data_tool.set_vertex(i, vertex)
			#i = i + 1
	#array_plane.surface_remove(0)
	data_tool.commit_to_surface(array_plane)
	#surface_tool.clear()
#	#surface_tool.begin(Mesh.PRIMITIVE_TRIANGLES)
#	surface_tool.create_from(array_plane, 0)
#	surface_tool.commit(array_plane)
#	surface_tool.generate_normals()
#	mesh_instance.mesh = surface_tool.commit()
#	mesh_instance.create_trimesh_collision()
#	#collision.shape = mesh_instance
#


func _on_Area_body_exited(body):
	array_plane.surface_remove(0)
	#data_tool.commit_to_surface(array_plane)
	#surface_tool.clear()
	#surface_tool.begin(Mesh.PRIMITIVE_TRIANGLES)
	surface_tool.create_from(array_plane, 0)
	#surface_tool.commit(array_plane)
	surface_tool.generate_normals()
	mesh_instance.mesh = array_plane
	mesh_instance.create_trimesh_collision()
	#collision.shape = mesh_instance
