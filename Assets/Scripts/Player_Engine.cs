using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    public class Player_Engine : MonoBehaviour
    {
        private List<GameObject> missiles = new List<GameObject>();
        private readonly int numOfPooledMissiles = 20;
        public int numOfMissilesRemaining;

        private List<GameObject> launchStations = new List<GameObject>();
        public int numOfLaunchStations = 2;

        public string missileName = "Missile";
        public string launchStationName = "LaunchStation";

        public GameObject mousePointerPosition;
        public Camera mainCamera;

        public bool playerReady;

        void Start()
        {
            playerReady = false;
            GameObject pooler = Resources.Load<GameObject>("Prefabs/PoolerPrefab");
            GameObject missile = Resources.Load<GameObject>("Prefabs/PlayerMissilePrefab");
            GameObject launchStation = Resources.Load<GameObject>("Prefabs/LaunchStationPrefab");

            MissilePooler(pooler, missile);
            LaunchStationPooler(pooler, launchStation);

            playerReady = true;

            return;
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

        private void LaunchStationPooler(GameObject pooler, GameObject launchStation)
        {
            if (launchStations != null)
                launchStations.Clear();

            launchStations = pooler.GetComponent<Pooling_Engine>().PoolObjects(launchStation, new Vector3(0f, -20f, 0f), numOfLaunchStations, launchStationName);

            GameObject launchStationParent = new GameObject
            {
                name = "LaunchStations"
            };
            launchStationParent.transform.SetParent(this.transform);
            launchStationParent.transform.position = new Vector3(0f, -20f, 0f);
            for (int i = 0; i < numOfLaunchStations; i++)
            {
                launchStations[i].transform.SetParent(launchStationParent.transform);

                if (i == 0)
                    launchStations[i].transform.position = new Vector3(-5f, 1f, 0);

                else
                    launchStations[i].transform.position = new Vector3(5f, 1f, 0);

                launchStations[i].SetActive(true);
            }
        }

        void Update()
        {
            if (playerReady)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (numOfMissilesRemaining > 0)
                    {
                        FireMissile();
                    }
                }
            }
        }

        void FireMissile()
        {
            Vector3 target;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50))
            {
                Debug.Log("Hit Position = " + hit.point);
                target = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                for (int i = 0; i < numOfPooledMissiles;)
                {
                    if (missiles[i].activeInHierarchy)
                        i++;
                    else
                    {
                        Transform launchStationTarget = launchStations[SetLaunchStation(target)].transform;
                        missiles[i].transform.SetPositionAndRotation(launchStationTarget.position, launchStationTarget.rotation);
                        missiles[i].SetActive(true);
                        numOfMissilesRemaining--;
                        missiles[i].SendMessage("SetTargetForPlayer", target);

                        if (target.y > 3 && target.y < 7)
                        {
                            missiles[i].SendMessage("SetSpeed", 1.5f);
                            missiles[i].SendMessage("SetRotationSpeed", 50f);
                        }
                        else if (target.y <= 3)
                        {
                            missiles[i].SendMessage("SetSpeed", 1f);
                            missiles[i].SendMessage("SetRotationSpeed", 70f);
                        }
                        else
                        {
                            missiles[i].SendMessage("SetSpeed", 2f);
                            missiles[i].SendMessage("SetRotationSpeed", 40f);
                        }

                        return;
                    }
                }
            }
        }

        private int SetLaunchStation(Vector3 tar)
        {
            if (tar.x < 0)
                return 0;

            else if (tar.x > 0)
                return 1;

            else
            {
                int temp = UnityEngine.Random.Range(1, 2);
                return temp;
            }
        }
    }
}
