using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    public bool enemyShot;

    // Update is called once per frame
    void Update()
    {
        Movement();  
    }

    void Movement()
    {
        if (enemyShot)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }   
        
        if (transform.position.y >= 5.27f)
        {
            gameObject.SetActive(false);
        }

        if (transform.position.y <= -5.32f)
        {
            gameObject.SetActive(false);
        }
    }
}
