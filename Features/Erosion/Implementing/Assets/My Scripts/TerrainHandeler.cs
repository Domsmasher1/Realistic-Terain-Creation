using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHandeler: MonoBehaviour
{
    public byte[,,] data;
    public int TerrainHandelerX = 16;
    public int TerrainHandelerY = 16;
    public int TerrainHandelerZ = 16;

    public byte Block(int x, int y, int z)
    {

        if (x >= TerrainHandelerX || x < 0 || y >= TerrainHandelerY || y < 0 || z >= TerrainHandelerZ || z < 0)
        {
            return (byte)1;
        }

        return data[x, y, z];
    }

    void Start()
    {
        data = new byte[TerrainHandelerX, TerrainHandelerY, TerrainHandelerZ];

        for (int x = 0; x < TerrainHandelerX; x++)
        {
            for (int y = 0; y < TerrainHandelerY; y++)
            {
                for (int z = 0; z < TerrainHandelerZ; z++)
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
