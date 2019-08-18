using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    public class Enemy_Engine : MonoBehaviour
    {

        public List<GameObject> missiles = new List<GameObject>();
        private readonly int numOfPooledMissiles = 20;
        private int numOfMissilesRemaining;//, health;
        private float waitTimer, spawningTimer;
        private bool isWaitingToSpawn, ammoOut;

        public string missileName = "Missile";

        void Start()
        {
            GameObject pooler = Resources.Load<GameObject>("Prefabs/PoolerPrefab");
            GameObject missile = Resources.Load<GameObject>("Prefabs/EnemyMissilePrefab");
            MissilePooler(pooler, missile);

            gameObject.SetActive(false);
        }

        private void MissilePooler(GameObject pooler, GameObject missile)
        {
            if (missiles != null)
                missiles.Clear();

            missiles = pooler.GetComponent<Pooling_Engine>().PoolObjects(missile, new Vector3(0f, -20f, 0f), numOfPooledMissiles, missileName);

            GameObject missileParent = new GameObject
            {
                name = "Missiles"
            };
            missileParent.transform.SetParent(this.transform);
            missileParent.transform.position = new Vector3(0f, -20f, 0f);
            for (int i = 0; i < numOfPooledMissiles; i++)
                missiles[i].transform.SetParent(missileParent.transform);
        }

        private void OnEnable()
        {
            waitTimer = 3;
            spawningTimer = 0;
            numOfMissilesRemaining = 10;
            isWaitingToSpawn = true;
            ammoOut = false;
        }

        void Update()
        {
            if (!ammoOut)
            {
                if (isWaitingToSpawn)
                {
                    waitTimer -= Time.deltaTime;

                    if (waitTimer <= 0)
                        isWaitingToSpawn = false;
                }
                else
                {
                    spawningTimer += Time.deltaTime;

                    if (spawningTimer >= 2)
                    {
                        SpawnMissile();
                        spawningTimer = 0;
                    }
                }
            }
        }

        private void SpawnMissile()
        {
            //Vector3 spawxnPosition = FindTargetPosition(-12f, 12f, 12);
            
            for (int i = 0; i < numOfPooledMissiles;)
            {
                if (missiles[i].activeInHierarchy)
                    i++;
                else
                {
                    Vector3 target = FindTargetPosition(-12f, 12f, 12);
                    missiles[i].transform.SetPositionAndRotation(target, SetRotation(missiles[i].transform.position, target));
                    //missiles[i].transform.position = FindTargetPosition(-12f, 12f, 12);
                    //missiles[i].transform.eulerAngles = new Vector3(-180f, 0f, 0f);
            //Quaternion targetRotation;


                    missiles[i].SetActive(true);
                    missiles[i].SendMessage("SetTargetForEnemy", FindTargetPosition(-10f, 10f, 0f));

                    missiles[i].SendMessage("SetSpeed", 12f);

                    return;
                }
            }

            numOfMissilesRemaining--;
            if (numOfMissilesRemaining <= 0)
            {
                ammoOut = true;
                isWaitingToSpawn = true;
                Debug.Log("Spawning Complete");
            }
        }

        private Quaternion SetRotation(Vector3 missilePosition, Vector3 targetPosition)
        {
            //Get the Screen positions of the object
            //Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            //Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(missilePosition, targetPosition);
            //float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            Debug.Log("Enemy Missile Angle" + angle);
            //Ta Daaa
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
            return rotation;
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        private Vector3 FindTargetPosition(float minRandomNum, float maxRandomNum, float yPosition)
        {
            float x = Random.Range(minRandomNum, maxRandomNum);
            Vector3 target = new Vector3(x, yPosition, 0f);
            Debug.Log(target);
            return target;
        }
    }
}