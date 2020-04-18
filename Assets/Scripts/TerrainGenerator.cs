using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

 [RequireComponent(typeof(MarchingCubes))]
 public class TerrainGenerator : MonoBehaviour {
     private MarchingCubes mc;
     
     [SerializeField] private int width = 32;
     [SerializeField] private int height = 8;
     [SerializeField] private float terrainSurface = 0.5f;
     [SerializeField] private float[,,] tmap;
     
     private MeshFilter meshFilter;
     private void Start() {
         mc = GetComponent<MarchingCubes>();
         meshFilter = GetComponent<MeshFilter>();
         mc.ClearMeshData();
         
         tmap = GenerateTerrain();
         CreateMeshData();
         meshFilter.mesh = marchingcubes.Utils.BuildMesh(mc.Vertices,mc.Tris);
     }

     private float[,,] GenerateTerrain() {
         float[,,] map = new float[width + 1,height + 1, width + 1];
         for (int x = 0; x < width; x++) {
             for (int y = 0; y < height; y++) {
                 for (int z = 0; z < width; z++) {
                     float h = (float) height *
                               Mathf.PerlinNoise((float) x / 16f * 1.5f + 0.001f, (float) z / 16f * 1.5f + 0.001f);
                     float point = 0;
 
                     if (y <= (h - 0.5f)) point = 0f;
                     else if (y > h + 0.5f) point = 1f;
                     else if (y > h) point = (float) y - h;
                     else point = h - (float) y;
 
                     map[x, y, z] = point;
                 }
             }
         }
         return map;
     }
     
     private void CreateMeshData() {
         for (int i = 0; i < width; i++) {
             for (int j = 0; j < height; j++) {
                 for (int k = 0; k < width; k++) {
                     float[] cube = new float[8];

                     for (int l = 0; l < 8; l++) {
                         Vector3Int corner = new Vector3Int(i, j, k) + marchingcubes.Utils.CornerTable[l];
                         cube[l] = tmap[corner.x, corner.y, corner.z];
                     }
                     mc.MarchCube(new Vector3(i, j, k), cube,terrainSurface);
                 }
             }
         }
     }
 }