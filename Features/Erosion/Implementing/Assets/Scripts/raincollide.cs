using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raincollide : MonoBehaviour
{
    World world;

    void OnCollisionEnter(Collision collision) //Detect collisions between the GameObjects with Colliders attached
    {
        if (collision.gameObject.tag == "Chunk") //Check for a match with the specific tag on any GameObject that collides with your GameObject
        {
            Vector3 position = collision.contacts[0].point;
            position+= (position * -0.5f);
           // Removedablock(position);

        }
    }


    /*public void Removedablock(Vector3 position)
    {
        //sets the specified block at these coordinates

        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        int z = Mathf.RoundToInt(position.z);

        print(x + ", " + y + ", " + z);

       SetBlockAt(x, y, z, world.block);
    }*/





    // Start is called before the first frame update
    void Start()
    {
        world = gameObject.GetComponent("World") as World;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
