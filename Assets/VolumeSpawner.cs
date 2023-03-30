using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSpawner : MonoBehaviour
{
    public Collider collider;
    public GameObject prefab;
    public bool spawnAtStart = true;
    public Vector2 count;
    public float height = 100;
    public float gapSize = 50;
    public float offset = 20;
    public bool randomRotation = true;
    public float spawnChance= 0.8f;
    public bool rayCastDown = false;

    public void Start()
    {
        if (spawnAtStart)
        {
            Spawn();
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
                var rot = Quaternion.Euler(0, 0, 0);
                if (rayCastDown && Physics.Raycast(pos, Vector3.down, out var hit))
                {
                    if (hit.collider.gameObject.CompareTag("Ground"))
                    {
                        Debug.Log("RayCasted");
                        Instantiate(prefab, pos, rot);
                        Debug.DrawLine(transform.position, hit.point, Color.green);
                    }

                    
                }
                if (randomRotation)
                {
                    rot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

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
