using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private MeshFilter _meshFilter;
        
    private void OnDrawGizmos()
    {
        _meshFilter = GetComponent<MeshFilter>();
        MeshUtils.CreateSquareGrid(20, 20, -Vector3.one, 1, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

        MeshUtils.ChangeSquareGridUvs(0, 4, 1, 1, 1, ref uvs);
        
        Mesh mesh = new Mesh();
        
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        _meshFilter.mesh = mesh;
    }
}
