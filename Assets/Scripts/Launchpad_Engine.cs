using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad_Engine : MonoBehaviour
{
    public int health;
    //private int rotationSpeed = 60;
    //private Vector3 target;

    private void Start()
    {
        //Vector3 target;
    }

    private void OnEnable()
    {
        health = 3;

        return;
    }

    private void FixedUpdate()
    {
        
        transform.rotation = LookAtTarget();
    }

    private Quaternion LookAtTarget()
    {
        //Get the Screen positions of the object
        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        return rotation;
        
    }

    public Quaternion GetRotation()
    {
        return this.transform.rotation;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (health > 0)
        {
            if (collision.gameObject.tag == "Enemy")
                health--;
        }
        else
            gameObject.SetActive(false);

        return;
    }
}
