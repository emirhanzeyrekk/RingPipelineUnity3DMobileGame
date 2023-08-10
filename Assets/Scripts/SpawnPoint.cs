using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject point;

    private void Start()
    {
        if (!this.gameObject.CompareTag("Enemy"))
        {
            CreatePoint();
        }
  
    }

    private void CreatePoint()
    {
        float radius_cyl = transform.localScale.x / 2;
        float radius_cube = point.transform.localScale.x / 2;

        float height = radius_cube + radius_cyl;

        float minRange = transform.position.z - transform.localScale.y;
        float maxRange = transform.position.z + transform.localScale.y;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + height, Random.Range(minRange, maxRange));

        Instantiate(point, pos, Quaternion.identity);
    }
}
