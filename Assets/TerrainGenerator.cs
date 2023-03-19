using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;


public class TerrainGenerator : MonoBehaviour
{
    public int size = 256; //map size
    public int maxHeight = 64;
    public bool autoGenerate = true;
    private Terrain terrain;
    public float mountainSize = 50;

    private void Start()
    {
        if(autoGenerate)Generate();
        
    }

    private void Generate()
    {
        terrain = GetComponent<Terrain>();

        var terrainData = terrain.terrainData;

        terrainData.heightmapResolution = size;
        terrainData.size = new Vector3(size, maxHeight, size);

        var heightMap = new float[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var pos = new float2(x, y) / mountainSize;
                var height = noise.pnoise(pos, size);
                heightMap[x, y] = height;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);

    }
}
