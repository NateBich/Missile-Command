using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    public class ExplosionCollider_Engine : MonoBehaviour
    {
        private float timer;
        private bool grow;
        private void OnEnable()
        {
            timer = 0;
            transform.localScale = new Vector3(1f, 1f, 1f);
            grow = true;
        }

        void Update()
        {
            
            timer += Time.deltaTime;
            if (grow)
            {
                transform.localScale.Scale(new Vector3(2f * Time.deltaTime, 2f * Time.deltaTime, 2f * Time.deltaTime));
                if (transform.localScale.x >= 1.25f)
                    grow = false;
            }
            else
            {
                transform.localScale.Scale(new Vector3(-2f * Time.deltaTime, -2f * Time.deltaTime, -2f * Time.deltaTime));
                if (transform.localScale.x <= 0.25f)
                    gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Missile")
            {
                collision.gameObject.SendMessage("DestroyObject");
                gameObject.SendMessageUpwards("DestroyObject");
            }
        }
    }
}