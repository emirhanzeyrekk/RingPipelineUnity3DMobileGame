using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = new Vector3 (0, 0, 0);

    [SerializeField]
    private LayerMask player_layer;

    [SerializeField]
    private Color Collectable_color, nonCollectable_color;

    [SerializeField]
    private AudioClip pickup_sound;

    private PlayerManager pm;

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        transform.Rotate(axis * Time.deltaTime);

        if (pm.can_collect)
        {
            //Color and rotation speed
            axis.y = 270;
            GetComponent<MeshRenderer>().material.color = Collectable_color;

            //Collect Point
            bool touchingToPlayer = Physics.CheckSphere(transform.position, 0.2f, player_layer);
            if (touchingToPlayer)
            {
                pm.IncreaseHealth(2.0f);

                Camera.main.GetComponent<AudioSource>().PlayOneShot(pickup_sound);

                Destroy(this.gameObject);
            }
        }
        else
        {
            //Color and rotation speed
            axis.y = 45;
            GetComponent<MeshRenderer>().material.color = nonCollectable_color;
        }
    }
}
