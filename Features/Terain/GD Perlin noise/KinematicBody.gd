extends KinematicBody


var move_speed = 500


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var move_vec = Vector3()
	move_vec.x = rand_range(0, 1)
	move_vec.z = rand_range(1, 0)
	
	move_and_slide(move_vec * move_speed * delta)
	
