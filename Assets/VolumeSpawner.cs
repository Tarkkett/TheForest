using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trees
{
    public float probability;
    public GameObject treePrefab;
}

public class VolumeSpawner : MonoBehaviour
{
    public int seed;

    public Collider collider;
    [Header("Clouds")]
    public GameObject cloudPrefab;
    [Header("Trees")]
    public List<GameObject> gameObjectPrefabs;
    public bool spawnAtStart = true;
    public Vector2 count;
    public float height = 100;
    public float gapSize = 50;
    public float offset = 20;
    public bool randomRotation = true;
    public float spawnChance= 0.8f;
    public bool rayCastDown = false;
    public bool placingClouds = false;
    public bool placingTrees = false;
    public LayerMask mask;
    [SerializeField]List<Trees> treeClass = new List<Trees>();
    

    public void Start()
    {
        if (spawnAtStart)
        {
            Spawn();
            Random.InitState(seed);
        }
    }
    public void Spawn()
    {
        var bounds = collider.bounds;
        for (var z = bounds.min.z; z < bounds.max.z; z +=gapSize)
        {
            for (var x = bounds.min.x; x < bounds.max.x; x+=gapSize)
            {
                var chance = Random.Range(0, 1f);
                if (chance > spawnChance)
                {
                    continue;
                }
                
                var pos = new Vector3(x, height, z);
                pos += Random.insideUnitSphere * offset;
                
                if (rayCastDown && Physics.Raycast(pos, Vector3.down, out var hit))
                {
                    var rot = Quaternion.Euler(0, 0, 0);
                    if (randomRotation)
                    {
                        rot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                    }
                    if (placingClouds == true)
                    {

                        Instantiate(cloudPrefab, pos, rot);
                        
                    }
                    if (hit.collider.gameObject.CompareTag("Ground"))
                    {
                        
                        if (placingTrees)
                        {
                            
                            Instantiate(gameObjectPrefabs[Random.Range(0, gameObjectPrefabs.Count)], hit.point, rot);
                            Debug.DrawLine(transform.position, hit.point, Color.red);
                        }

                    }
                    
                }
                
                
                
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        if (!collider)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(collider.bounds.center + Vector3.up * height, collider.bounds.size);
    }
}
