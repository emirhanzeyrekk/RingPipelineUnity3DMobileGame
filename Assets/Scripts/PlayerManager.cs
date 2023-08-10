using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Constants
    
    private const float size_scalel = 0.28f;
    private const float checker_radius = 0.18f;
    private const float offset = 0.05f;

    #endregion

    #region SerializeFields
    
    [SerializeField]
    private Vector3 default_size = new Vector3 (1, 1, 1);

    [SerializeField]
    private LayerMask cylinder_layer;

    [SerializeField]
    private AudioClip click_sound, death_sound;

    #endregion

    [HideInInspector]
    public bool can_collect = false;

    public float health = 10.0f;

    #region Unity

    private void Update()
    {
        //Define cylinder and radius
        Transform cyl = Physics.OverlapSphere(transform.position, checker_radius, cylinder_layer)[0].transform;
        float cyl_radius = cyl.localScale.x * size_scalel;

        //Check For Player Death

        if(health <= 0)
        {
            Death();
        }

        if(cyl_radius > transform.localScale.y)
        {
            Death();
        }

        if (cyl.CompareTag("Enemy"))
        {
            if(cyl_radius + offset > transform.localScale.y)
            {
                Death();
            }
        }

        //Check can_collect
        if (cyl_radius + offset > transform.localScale.y)
        {
            can_collect = true;
        }
        else
        {
            can_collect = false;
        }

        ChangeRingRadius(cyl_radius);

        HealthCounter();

    }

    #endregion

    #region Functions

    private void Death()
    {
        //Stop Camera Controller
        if(Camera.main != null)
        {
            Camera.main.GetComponent<CameraController>().enabled = false;
        }

        //Open GameOver UI
        UIManager.ui_m.OpenGameOverUI();

        //Player Alive Boolean
        GameManager.gm.isPlayerAlive = false;

        //Play Death Sound Effect
        Camera.main.GetComponent<AudioSource>().PlayOneShot(death_sound);

        //Save High Score
        if(GameManager.gm.distance > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", GameManager.gm.distance);
        }

        //Set High Score Text
        UIManager.ui_m.SetHighScoreText();

        //!!!!!!!!!!!!! Destroy player GameObject !!!!!!!!!
        Destroy(this.gameObject);
    }

    private void ChangeRingRadius(float cyl_radius)
    {
        if(Input.touchCount > 0) //!!!!!!!!
        {
            Touch touch = Input.GetTouch(0);

            //Play sound effect
            if (touch.phase == TouchPhase.Began) //Input.GetMouseButtonDown(0)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(click_sound, 0.3f);
            }

            //When touched to screen
            if (touch.phase == TouchPhase.Stationary) //Input.GetMouseButton(0)
            {
                //Set size of the ring
                Vector3 target_vector = new Vector3(default_size.x, cyl_radius, cyl_radius);
                transform.localScale = Vector3.Slerp(transform.localScale, target_vector, 0.125f);

            }
            /*else
            {
                transform.localScale = Vector3.Slerp(transform.localScale, default_size, 0.125f);
            }*/
        }//!!!!!!!!!
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, default_size, 0.125f);
        }

    }

    private void HealthCounter()
    {
        health = Mathf.Clamp(health,-1, 10.0f);

        if(health >= 0)
        {
            health -= Time.deltaTime;
            UIManager.ui_m.SetPlayerHealth(health);
        }
    }

    public void IncreaseHealth(float value)
    {
        health += value;
    }


    #endregion
}
