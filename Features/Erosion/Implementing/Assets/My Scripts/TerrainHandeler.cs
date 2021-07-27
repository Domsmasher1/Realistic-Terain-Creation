using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHandeler: MonoBehaviour
{
    public byte[,,] data;
    public int TerrainHandelerX = 16; // The x paramiters for the world size
    public int TerrainHandelerY = 16; // The y paramiters for the world size
    public int TerrainHandelerZ = 16; // The z paramiters for the world size
    public int HightLimit = 8; // The maximum hight that things will be created

    public GameObject chunk;
    public GameObject[,,] chunks;
    public int chunkSize = 16;

    public byte Block(int x, int y, int z)
    {
        if (x >= TerrainHandelerX || x < 0 || y >= TerrainHandelerY || y < 0 || z >= TerrainHandelerZ || z < 0) // If x is biggr than the max (set by TerainHandelerX) or x is smaller than 0, then repeat for Y and Z
        {
            return (byte)1; // Return empty
        }
        return data[x, y, z]; // Make the byte = to the x, y, and z values
    }
    void Start() //Will run when script is called
    {
        data = new byte[TerrainHandelerX, TerrainHandelerY, TerrainHandelerZ]; //Creates a byte that will hold the values to place the block, if the block is valid

        for (int x = 0; x < TerrainHandelerX; x++) //Checks where to build a block, and sees if its valid on the x axis
        {
            for (int y = 0; y < TerrainHandelerY; y++) //Checks where to build a block, and sees if its valid on the y axis
            {
                for (int z = 0; z < TerrainHandelerZ; z++) //Checks where to build a block, and sees if its valid on the z axis
                {
                    if (y <= HightLimit) // Sets the limit where no more meshes will be drawn
                    {
                        data[x, y, z] = 1; //Sets the blocks to air
                    }
                    if (y > HightLimit)// Defines what is placed below the hight limit
                    {
                        data[x, y, z] = 0; // Sets the blocks to stone
                    }
                }
            }
        }
    }
}
