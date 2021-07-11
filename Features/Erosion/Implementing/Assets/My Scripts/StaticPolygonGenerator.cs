using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPolygonGenerator : MonoBehaviour
{
    // Lists
    public List<Vector3> newVertices = new List<Vector3>(); // Contains every vertex of the mesh
    public List<int> newTriangles = new List<int>(); // Tells Unity how to build the mesh
    public List<Vector2> newUV = new List<Vector2>(); // Tells Unity how the texture is aligned on each polygon
    public List<Vector3> colVertices = new List<Vector3>(); //A list with all the vertices of the collison boxes
    public List<int> colTriangles = new List<int>(); // A list with all the triangles in the collison boxes

    // Mesh
    private Mesh mesh; // Eveything is saved to and built on this variable 

    // Textures
    private float tUnit = 0.25f; // Setting up the texture picker for the texture map 
    private Vector2 tStone = new Vector2(3, 2); // Sets tStone to the texture thats at 0,0 on the texture map
    private Vector2 tGrass = new Vector2(0, 1); // Sets tGrass to the texture thats at 0,1 on the texture map

    // Squares
    private int squareCount;  // Keeps track of what square you are on

    // Colider
    private int colCount;
    private MeshCollider col; // Everything for the colider is built on this variable

    // Bytes
    public byte[,] blocks; // 2d array to store block info

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        squareCount = 0;
        newVertices.Clear();
        newTriangles.Clear();
        newUV.Clear();

        Mesh newMesh = new Mesh();
        newMesh.vertices = colVertices.ToArray();
        newMesh.triangles = colTriangles.ToArray();
        col.sharedMesh = newMesh;

        colVertices.Clear();
        colTriangles.Clear();
        colCount = 0;
    }
    void GenSquare(int x, int y, Vector2 texture) //Creating the square
    {

        newVertices.Add(new Vector3(x, y, 0));
        newVertices.Add(new Vector3(x + 1, y, 0));
        newVertices.Add(new Vector3(x + 1, y - 1, 0));
        newVertices.Add(new Vector3(x, y - 1, 0));

        newTriangles.Add(squareCount * 4);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 3);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 2);
        newTriangles.Add((squareCount * 4) + 3);

        newUV.Add(new Vector2(tUnit * texture.x, tUnit * texture.y + tUnit));
        newUV.Add(new Vector2(tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
        newUV.Add(new Vector2(tUnit * texture.x + tUnit, tUnit * texture.y));
        newUV.Add(new Vector2(tUnit * texture.x, tUnit * texture.y));

        squareCount++;

    }
    void GenTerrain() // Sets up the paramiters and boundries for the created terain
    {
        blocks = new byte[20, 7]; // First value how long the wall is, Seccond value how high the wall is (x,y)

        for (int px = 0; px < blocks.GetLength(0); px++)
        {
            for (int py = 0; py < blocks.GetLength(1); py++)
            {
                if (py > 4) // if any blocks are above the y level of 4
                {
                    blocks[px, py] = 2; // Set the texture of the blocks to 2 (grass)
                }
                else if (py < 5) // if any blocks are below the y level of 5
                {
                    blocks[px, py] = 1; // Set the texture of the blocks to 1 (stone)
                }
                if (px == 10) // if the x value of the blocks is 10
                {
                    blocks[px, py] = 0; // Set the texture of the blocks to 0 (none)
                }
            }
        }
    }
    void BuildMesh()
    {
        for (int px = 0; px < blocks.GetLength(0); px++)
        {
            for (int py = 0; py < blocks.GetLength(1); py++)
            {
                if (blocks[px, py] != 0) // If the block is not air
                {
                    GenCollider(px, py); // This will apply it to every block other than air

                    if (blocks[px, py] == 1) // if the texture value is equal to 1
                    {
                        GenSquare(px, py, tStone); //Set the texture to Stone
                    }
                    else if (blocks[px, py] == 2) // if the texture value is equal to 2
                    {
                        GenSquare(px, py, tGrass); //Set the texture to Grass
                    }
                } //End air block check
            }
        }
    }

    byte Block(int x, int y)
    {
        if (x == -1 || x == blocks.GetLength(0) || y == -1 || y == blocks.GetLength(1))
        {
            return (byte)1;
        }
        return blocks[x, y];
    }
    void GenCollider(int x, int y)
    {

        //Creating collider for the top of the cube
        if (Block(x, y + 1) == 0) // Checking if cube is already next to another collider on its given side
        {
            colVertices.Add(new Vector3(x, y, 1));
            colVertices.Add(new Vector3(x + 1, y, 1));
            colVertices.Add(new Vector3(x + 1, y, 0));
            colVertices.Add(new Vector3(x, y, 0));
            ColliderTriangles();
            colCount++;
        }

        //Creating collider for the bottem of the cube
        if (Block(x, y - 1) == 0) // Checking if cube is already next to another collider on its given side
        {
            colVertices.Add(new Vector3(x, y - 1, 0));
            colVertices.Add(new Vector3(x + 1, y - 1, 0));
            colVertices.Add(new Vector3(x + 1, y - 1, 1));
            colVertices.Add(new Vector3(x, y - 1, 1));
            ColliderTriangles();
            colCount++;
        }

        //Creating collider for the left side of the cube
        if (Block(x - 1, y) == 0) // Checking if cube is already next to another collider on its given side
        {
            colVertices.Add(new Vector3(x, y - 1, 1));
            colVertices.Add(new Vector3(x, y, 1));
            colVertices.Add(new Vector3(x, y, 0));
            colVertices.Add(new Vector3(x, y - 1, 0));
            ColliderTriangles();
            colCount++;
        }

        //Creating collider for the right side of the cube
        if (Block(x + 1, y) == 0) // Checking if cube is already next to another collider on its given side
        {
            colVertices.Add(new Vector3(x + 1, y, 1));
            colVertices.Add(new Vector3(x + 1, y - 1, 1));
            colVertices.Add(new Vector3(x + 1, y - 1, 0));
            colVertices.Add(new Vector3(x + 1, y, 0));
            ColliderTriangles();
            colCount++;
        }
    }

    void ColliderTriangles()
    {
        colTriangles.Add(colCount * 4);
        colTriangles.Add((colCount * 4) + 1);
        colTriangles.Add((colCount * 4) + 3);
        colTriangles.Add((colCount * 4) + 1);
        colTriangles.Add((colCount * 4) + 2);
        colTriangles.Add((colCount * 4) + 3);
    }

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<MeshCollider>();
        GenTerrain();
        BuildMesh();
        UpdateMesh();
    }
}