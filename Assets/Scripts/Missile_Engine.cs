using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    public class Missile_Engine : MonoBehaviour
    {
        private float timer, delayTimer, speed, rotationSpeed = 60;
        private Vector3 startPosition, turnPosition, controlPoint, target;
        private bool canMove = false, alive = false, canTurn = false;

        //public static Missile_Engine missileEngine;

        private void Start()
        {
            //missileEngine = this;

            if (canMove)
                canMove = false;
            if (alive)
                alive = false;
        }

        private void OnEnable()
        {
            delayTimer = 0;
            canMove = false;
            canTurn = false;
            alive = true;
        }

        private void Update()
        {

            switch (canMove)
            {
                case false:
                    delayTimer += Time.deltaTime;
                    if (delayTimer >= 0.25f)
                    {
                        canMove = true;
                        timer = 0;
                    }
                    break;

                case true:
                    if (timer >= 1)
                        canTurn = true;
                    float dist = Vector3.Distance(target, transform.position);
                    if (dist < 0.25f)
                        DeactiveMissile();
                    break;
            }

        }

        void FixedUpdate()
        {
            switch (canMove)
            {
                case false:

                    break;

                case true:
                    transform.position = MoveMissile(timer / speed, startPosition, target, controlPoint);
                    //if (canTurn)
                        transform.rotation = LookAtTarget();

                    timer += Time.deltaTime;
                    break;
            }
        }

        private Vector3 MoveMissile(float t, Vector3 start, Vector3 end, Vector3 p)
        {
            float u = 1 - t;
            float uu = u * u;

            Vector3 point = uu * start;
            point += 2 * u * t * p;
            point += t * t * end;
            return point;
        }

        private Quaternion LookAtTarget()
        {
            //Quaternion q = Quaternion.LookRotation(target - transform.position);
            //Quaternion rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(transform.position, target);

            //Ta Daaa
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
            return rotation;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Missile")
            {
                collision.gameObject.SendMessage("DestroyObject");
                DestroyObject();
            }
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        public void SetTargetForPlayer(Vector3 tar)
        {
            target = tar;
            startPosition = transform.position;
            controlPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //controlPoint = new Vector3(transform.position.x, 0.05f + tar.y, transform.position.z);
            return;
        }

        public void SetTargetForEnemy(Vector3 tar)
        {
            target = tar;
            startPosition = transform.position;
            controlPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            return;
        }

        public void DestroyObject()
        {

            DeactiveMissile();
        }

        public void SetSpeed(float s)
        {
            speed = s;
            return;
        }

        public void SetRotationSpeed(float s)
        {
            rotationSpeed = s;
            return;
        }

        private void DeactiveMissile()
        {
            //explosionCollider.SetActive(false);
            this.gameObject.SetActive(false);
            return;
        }
    }
}