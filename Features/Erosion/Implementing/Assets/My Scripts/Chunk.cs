using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private List<Vector3> newVertices = new List<Vector3>();
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();

    private float tUnit = 0.25f;
    private Vector2 tStone = new Vector2(3, 2);
    private Vector2 tGrass = new Vector2(0, 3);

    private Mesh mesh;
    private MeshCollider col;

    private int faceCount;

    public GameObject TerrainHandelerGO;
    private TerrainHandeler TerrainHandeler;

    public int chunkSize = 16;

    void Start()
    {
        TerrainHandeler = TerrainHandelerGO.GetComponent("TerrainHandeler") as TerrainHandeler;

        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<MeshCollider>();
        CubeTop(0, 0, 0, 0);
        CubeNorth(0, 0, 0, 0);
        CubeSouth(0, 0, 0, 0);
        CubeEast(0, 0, 0, 0);
        CubeWest(0, 0, 0, 0);
        CubeBot(0, 0, 0, 0);
        CubeTop(0, 0, 0, 0);
        GenerateMesh();
    }

    void GenerateMesh()
    {
        for (int x = 0; x < TerrainHandeler.TerrainHandelerX; x++) //If x is smaller then the chunk size, then add one to x (x++)
        {
            for (int y = 0; y < TerrainHandeler.TerrainHandelerY; y++) //If y is smaller then the chunk size, then add one to y (y++)
            {
                for (int z = 0; z < TerrainHandeler.TerrainHandelerZ; z++) //If z is smaller then the chunk size, then add one to z (z++)
                {
                    //This code will run for every block in the chunk
                    if (TerrainHandeler.Block(x, y, z) != 0)
                    {
                        if (TerrainHandeler.Block(x, y + 1, z) == 0) //If the block is solid
                        {
                            CubeTop(x, y, z, TerrainHandeler.Block(x, y, z)); //Block above is air, so create mesh face
                        }
                        if (TerrainHandeler.Block(x, y - 1, z) == 0) //If the block is solid 
                        {
                            CubeBot(x, y, z, TerrainHandeler.Block(x, y, z)); //Block below is air, so create mesh face
                        }
                        if (TerrainHandeler.Block(x + 1, y, z) == 0 || (x == TerrainHandeler.TerrainHandelerX-1)) //If the block is solid or is the last block
                        {
                            CubeEast(x, y, z, TerrainHandeler.Block(x, y, z)); //Block east is air, so create mesh face
                        }
                        if (TerrainHandeler.Block(x - 1, y, z) == 0 || (x == 0)) //If the block is solid or is the first block
                        {
                            CubeWest(x, y, z, TerrainHandeler.Block(x, y, z)); //Block west is air, so create mesh face
                        }
                        if (TerrainHandeler.Block(x, y, z + 1) == 0 || (z == TerrainHandeler.TerrainHandelerZ-1)) //If the block is solid or is the last block
                        {
                            CubeNorth(x, y, z, TerrainHandeler.Block(x, y, z)); //Block north is air, so create mesh face
                        }
                        if (TerrainHandeler.Block(x, y, z - 1) == 0 || (z == 0)) //If the block is solid or is the first block
                        {
                            CubeSouth(x, y, z, TerrainHandeler.Block(x, y, z)); //Block south is air, so create mesh face
                        }
                    }
                }
            }
        }
    UpdateMesh();
    }

    void CubeTop(int x, int y, int z, byte block)  //Creates the top face of the cube out of triangles
    {
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));

        Vector2 texturePos;
        print("CubeTop xyz" + x + " " + y + " " + z);
        texturePos = tStone;

        Cube(texturePos);
    }

    void CubeNorth(int x, int y, int z, byte block) //Creates the north face of the cube out of triangles
    {
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);
    }

    void CubeEast(int x, int y, int z, byte block) //Creates the east face of the cube out of triangles
    {
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);
    }
    void CubeSouth(int x, int y, int z, byte block) //Creates the south face of the cube out of triangles
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);
    }
    void CubeWest(int x, int y, int z, byte block) //Creates the west face of the cube out of triangles
    {
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - 1, z));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);
    }
    void CubeBot(int x, int y, int z, byte block) //Creates the bottom face of the cube out of triangles
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);
    }


    void Cube(Vector2 texturePos)
    {
        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 1); //2
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4 + 3); //4

        newUV.Add(new Vector2(tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
        newUV.Add(new Vector2(tUnit * texturePos.x + tUnit, tUnit * texturePos.y + tUnit));
        newUV.Add(new Vector2(tUnit * texturePos.x, tUnit * texturePos.y + tUnit));
        newUV.Add(new Vector2(tUnit * texturePos.x, tUnit * texturePos.y));

        faceCount++;
    }

        void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        col.sharedMesh=null;
        col.sharedMesh=mesh;

        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();

        faceCount = 0;
    }

    void Update()
    {

    }
}
