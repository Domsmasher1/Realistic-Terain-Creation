extends Spatial

export(int, 1, 512) onready var size setget _setPlaneSize
var start = float(OS.get_ticks_msec())
var mdt = MeshDataTool.new()
var st = SurfaceTool.new()
var plane_mesh = PlaneMesh.new()
var mesh = MeshInstance.new()
var array_plane = st.commit()

func _setPlaneSize(newval):
	breakpoint
	size = newval
	print(size, newval)
	var start = float(OS.get_ticks_msec())
	var mdt = MeshDataTool.new()
	var st = SurfaceTool.new()
	var plane_mesh = PlaneMesh.new()
	var mesh = MeshInstance.new()
	plane_mesh.subdivide_width = newval
	plane_mesh.subdivide_depth = newval
	plane_mesh.size = Vector2(newval, newval)
	st.create_from(plane_mesh, 0)
	var array_plane = st.commit()
	var error = mdt.create_from_surface(array_plane, 0)
	for i in range (mdt.get_vertex_count()):
		var vertex = mdt.get_vertex(i)
		vertex.y = rand_range(1, 5) * 1
		mdt.set_vertex(i, vertex)
	for s in range (array_plane.get_surface_count()):
		array_plane.surface_remove(s)
	breakpoint
	mdt.commit_to_surface(array_plane)
	st.create_from(array_plane, 0)
	st.generate_normals()
	add_child(mesh)
	mesh.mesh = st.commit()
#	for s in range (array_plane.get_surface_count()):
#		array_plane.surface_remove(s)
	return [mesh, mdt, st, array_plane]
	

func _rerandomize(mesh, mdt, st, array_plane):
	print("aaaaa")
	breakpoint
	var array_plane_2 = st.commit()
	mdt.create_from_surface(array_plane, 0)
	print(mdt.get_vertex_count())
	for i in range (mdt.get_vertex_count()):
		var vertex = mdt.get_vertex(i)
		vertex.y = vertex.y + rand_range(1, 5)
		mdt.set_vertex(i, vertex)
	mdt.commit_to_surface(array_plane_2)
	st.create_from(array_plane_2, 0)
	st.generate_normals()
	mesh.mesh = st.commit()
	

func _ready():
	[mesh, mdt, st, array_plane] = _setPlaneSize(size)
	print(array_plane)
	

func _process(delta):
	print(array_plane)
	if Input.is_action_pressed("ui_accept"):
		_rerandomize(mesh, mdt, st, array_plane)
	

