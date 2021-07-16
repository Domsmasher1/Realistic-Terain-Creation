using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public byte[,,] data;
    public int worldX = 16;
    public int worldY = 16;
    public int worldZ = 16;

    public GameObject worldGO;
    private World world;

    void Start()
    {
        data = new byte[worldX, worldY, worldZ];

        for (int x = 0; x < worldX; x++)
        {
            for (int y = 0; y < worldY; y++)
            {
                for (int z = 0; z < worldZ; z++)
                {

                    if (y <= 8)
                    {
                        data[x, y, z] = 1;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
