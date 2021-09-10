extends KinematicBody


# Declare member variables here. Examples:
var move_speed = 300
var look_sense = 1
var move_vec = Vector3()
var mouse_delta = Vector2()

# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _input(event):
	if event is InputEventMouseMotion:
		rotation_degrees.y -= look_sense * event.relative.x
		rotation_degrees.x -= look_sense * event.relative.y

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	move_vec = Vector3()
	if Input.is_action_pressed("ui_up"):
		move_vec.z = -1
	if Input.is_action_pressed("ui_down"):
		move_vec.z = 1
	if Input.is_action_pressed("ui_left"):
		move_vec.x = -1
	if Input.is_action_pressed("ui_right"):
		move_vec.x = 1
	if Input.is_action_pressed("ui_page_up"):
		move_vec.y = 1
	if Input.is_action_pressed("ui_page_down"):
		move_vec.y = -1
	if Input.is_action_pressed("ui_end"):
		get_tree().quit()
	
	move_vec = move_vec.rotated(Vector3(0, 1, 0), rotation.y)
	move_and_slide(move_vec * move_speed * delta)
	
