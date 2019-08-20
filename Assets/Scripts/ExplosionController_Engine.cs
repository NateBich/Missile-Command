using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    public class ExplosionController_Engine : MonoBehaviour
    {
        public List<GameObject> explosionsTypeA;
        private readonly int numOfPooledExplosionsTypeA = 20;

        public string explosionTypeAName = "ExplosionTypeA";

        void Start()
        {
            GameObject pooler = Resources.Load<GameObject>("Prefabs/PoolerPrefab");
            GameObject explosion = Resources.Load<GameObject>("Prefabs/ExplosionPrefab");

            explosionsTypeA = ExplosionPooler(explosionsTypeA, pooler, explosion, numOfPooledExplosionsTypeA, explosionTypeAName);

            gameObject.SetActive(false);
        }

        List<GameObject> ExplosionPooler(List<GameObject> explosions, GameObject pooler, GameObject explosion, int numOfPooledExplosions, string explosionName)
        {
            if (explosions != null)
                explosions.Clear();

            explosions = pooler.GetComponent<Pooling_Engine>().PoolObjects(explosion, new Vector3(0f, -20f, 9f), numOfPooledExplosions, explosionName);

            GameObject explosionParent = new GameObject
            {
                name = "Explosions"
            };
            explosionParent.transform.SetParent(this.transform);
            explosionParent.transform.position = new Vector3(0f, -20f, 0f);
            for (int i = 0; i < numOfPooledExplosions; i++)
                explosions[i].transform.SetParent(explosionParent.transform);

            return explosions;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
