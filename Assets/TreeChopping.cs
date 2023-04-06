using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopping : MonoBehaviour
{

    public string resourceTag;
    public int range;
    public int damage;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            if (Physics.Raycast(ray, out var hit, range))
            {
                if (hit.collider.CompareTag(resourceTag))
                {
                    hit.collider.GetComponent<Health>()?.Damage(damage);
                }
            }
        }
        
    }
}
