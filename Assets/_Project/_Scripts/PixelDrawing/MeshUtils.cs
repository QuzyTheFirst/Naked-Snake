using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUtils
{

    public static Mesh CreateEmptyMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[0];
        mesh.uv = new Vector2[0];
        mesh.triangles = new int[0];
        return mesh;
    }

    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uvs = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }

    public static void CreateSquare(out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4];
        uvs = new Vector2[4];
        triangles = new int[6];

        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 1, 0);
        vertices[2] = new Vector3(1, 1, 0);
        vertices[3] = new Vector3(1, 0, 0);

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(0, 1);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
    }

    public static void CreateSquareGrid(int width, int height, Vector3 originPosition, float tilesize, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * width * height];
        uvs = new Vector2[4 * width * height];
        triangles = new int[6 * width * height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int index = x * height + y;

                vertices[index * 4 + 0] = new Vector2(originPosition.x + tilesize * x, originPosition.y + tilesize * y);
                vertices[index * 4 + 1] = new Vector2(originPosition.x + tilesize * x, originPosition.y + tilesize * (y + 1));
                vertices[index * 4 + 2] = new Vector2(originPosition.x + tilesize * (x + 1), originPosition.y + tilesize * (y + 1));
                vertices[index * 4 + 3] = new Vector2(originPosition.x + tilesize * (x + 1), originPosition.y + tilesize * y);

                uvs[index * 4 + 0] = new Vector2(0f, 0);
                uvs[index * 4 + 1] = new Vector2(0f, 1);
                uvs[index * 4 + 2] = new Vector2(1, 1);
                uvs[index * 4 + 3] = new Vector2(1, 0);

                triangles[index * 6 + 0] = index * 4 + 0;
                triangles[index * 6 + 1] = index * 4 + 1;
                triangles[index * 6 + 2] = index * 4 + 2;
                triangles[index * 6 + 3] = index * 4 + 0;
                triangles[index * 6 + 4] = index * 4 + 2;
                triangles[index * 6 + 5] = index * 4 + 3;
            }
        }
    }

    public static void ChangeSquareGridUvs(int index, int tilesNumberX, int tilesNumberY, int tileX, int tileY,  ref Vector2[] uvs)
    {
        float tileWidth = 1f / tilesNumberX;
        float tileHeight = 1f / tilesNumberY;
        
        float widthTileBeginning = tileX * tileWidth - tileWidth;
        float widthTileEnding = tileX * tileWidth;
        
        float heightTileBeginning = tileY * tileHeight - tileHeight;
        float heightTileEnding = tileY * tileHeight;

        uvs[index * 4 + 0] = new Vector2(widthTileBeginning, heightTileBeginning);
        uvs[index * 4 + 1] = new Vector2(widthTileBeginning, heightTileEnding);
        uvs[index * 4 + 2] = new Vector2(widthTileEnding, heightTileEnding);
        uvs[index * 4 + 3] = new Vector2(widthTileEnding, heightTileBeginning);
    }
}
