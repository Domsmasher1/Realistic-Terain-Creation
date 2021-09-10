extends KinematicBody2D

var state = "off"
var start = false
var chance = 10000
var decay = 0.95
var activated = false
var faction = ""
var move_vec = Vector2()
var speed = 10

func get_colliders():
	var colliders = []
	var area = get_node("Area2D")
	colliders = area.get_overlapping_bodies()
	var num = rand_range(1,10000)
	chance = chance + rand_range(0, 100)
	if num <= chance:
		var wait = 0
		while wait <= 2000:
			wait = wait + 1
		var object_num = colliders.size()
		var count_num = 0
		#print(colliders)
		while object_num > count_num:
			if colliders[count_num].state == "off":
				colliders[count_num].state = "on"
				colliders[count_num].faction = str(faction)
				colliders[count_num].chance = chance*decay
			count_num = count_num + 1
	else:
		decay = decay
	chance = chance*decay

func state_change():
	if state == "on" and activated == false:
		#print("active now")
		var sprite = get_node("Sprite")
		if faction == "red":
			sprite.modulate = Color(1, 0, 0)
		if faction == "green":
			sprite.modulate = Color(0, 1, 0)
		if faction == "yellow":
			sprite.modulate = Color(1, 1, 0)
		if faction == "purple":
			sprite.modulate = Color(1, 0, 1)
		activated = true

func _ready():
	randomize()

func _process(delta):
	if start == true:
		state = "on"
	if state == "on" and activated == false:
		get_colliders()
	state_change()
	move_and_slide(move_vec, speed)
