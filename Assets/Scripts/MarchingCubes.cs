using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MarchingCubes : MonoBehaviour {
    
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> tris = new List<int>();
    
    public List<Vector3> Vertices
    {
        get => vertices;
        set => vertices = value;
    }

    public List<int> Tris
    {
        get => tris;
        set => tris = value;
    }

    public void ClearMeshData() {
        vertices.Clear();
        tris.Clear();
    }

    public void MarchCube(Vector3 pos, float[] cube, float ts) {
        int index = marchingcubes.Utils.CalculateCubeIndex(cube,ts);
        
        if (index == 0 || index == 255)
            return;

        int e = 0;
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 3; j++) {
                int indice = marchingcubes.Utils.TriangleTable[index, e];

                if (indice == -1) return;

                Vector3 vertice1 = pos + marchingcubes.Utils.EdgeTable[indice, 0];
                Vector3 vertice2 = pos + marchingcubes.Utils.EdgeTable[indice, 1];
                
                vertices.Add(vertice1 + vertice2 /
                             2.0f);
                tris.Add(vertices.Count - 1);
                e++;
            }
        }
    }

    
}