using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    public class ExplosionController_Engine : MonoBehaviour
    {
        private Pooling_Engine pooler;
        public List<GameObject> explosionsTypeA;
        private readonly int numOfPooledExplosionsTypeA = 20;

        public string explosionTypeAName = "ExplosionTypeA";

        void Start()
        {
            pooler = GetComponent<Pooling_Engine>();
            GameObject explosion = Resources.Load<GameObject>("Prefabs/ExplosionPrefab");

            explosionsTypeA = pooler.PoolObjects(explosionsTypeA, explosion, this.gameObject, numOfPooledExplosionsTypeA, "ExplosionTypeA", "Explosions");
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
