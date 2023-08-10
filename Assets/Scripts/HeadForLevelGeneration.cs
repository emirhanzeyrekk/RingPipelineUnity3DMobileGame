using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadForLevelGeneration : MonoBehaviour
{
    [SerializeField]
    private LevelGenerator lg;

    [SerializeField]
    private LayerMask cyl_layer;

    private void Update()
    {
        Collider [] cyl = Physics.OverlapSphere(transform.position, 1f, cyl_layer);
        if(cyl.Length <= 0)
        {
            lg.SpawnCylinder();
        }
    }
}
