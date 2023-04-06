using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSource : MonoBehaviour
{
   
    void Start()
    {
        GetComponent<Health>().onDie.AddListener(SpawnPhysicsTree);
    }
    void SpawnPhysicsTree()
    {
        print("do not panic!");
    }

}
