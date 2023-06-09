using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;


public class TerrainGenerator : MonoBehaviour
{
    public int size = 256; //map size
    public float islandSize = 90;
    public int maxHeight = 64;
    
    private Terrain terrain;

    [Header("Sizes")]
    public float mountainSize = 50;
    public float hillSize = 10;
    public float bumpSize = 6;

    [Header("Heights")]
    public float mountainHeight = 1;
    public float hillHeight = 0.3f;
    public float bumpHeight = 0.2f;

    public bool autoGenerate = true;

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
                //distance from center
                var distance = Vector2.Distance(new Vector2(size / 2, size / 2), new Vector2(x, y));
                distance /= islandSize;
                distance = math.remap(0, size, 1, 0, distance);


                //generate height
                float Noise(Vector2 pos) => math.remap(-1, 1, 0, 1, noise.pnoise(pos, size));
                var pos = new float2(x, y) ;
                var height = Noise(pos / mountainSize) * mountainHeight;
                height -= Noise(pos / hillSize) * hillHeight;
                height += Noise(pos / bumpSize) * bumpHeight;

                

                height *= distance;

                //set height
                heightMap[x, y] = height;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);

    }
}
