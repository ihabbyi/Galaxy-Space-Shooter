using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private enum powerup
    {
        Shield,
        Speed,
        Triple_Shot,
        NumberOfPowerups
    }

    private Animator _anim;
    private BoxCollider2D _boxCollider;

    [SerializeField]
    private powerup _powerupSelected;

    public string powerupSelected;

    [SerializeField]
    private float _speed = 3;

    void OnEnable()
    {
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        PickPowerup();
    }

    void Update()
    {
        Movement();
    }

    void PickPowerup()
    {
        _powerupSelected = (powerup)Random.Range(0, (int)powerup.NumberOfPowerups);
        
        string powerupName = _powerupSelected.ToString();
        powerupSelected = powerupName;
        float xSize = 0;
        float ySize = 0;

        foreach(AnimatorControllerParameter parameter in _anim.parameters)
        {
            _anim.SetBool(parameter.name, false);
        }

        if (powerupName == "Triple_Shot")
        {
            xSize = 3.910053f;
            ySize = 3.550057f;
        }
        else if (powerupName == "Shield" || powerupName == "Speed")
        {
            xSize = 2.234706f;
            ySize = 3.550057f;
        }

        _boxCollider.size = new Vector2(xSize, ySize);
        
        _anim.SetBool(_powerupSelected.ToString(), true);
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.7f)
        {

            gameObject.SetActive(false);
        }
    }
}
