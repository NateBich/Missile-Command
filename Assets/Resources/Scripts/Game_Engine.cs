using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{ 
    public class Game_Engine : MonoBehaviour
    {
        public GameObject pooler, player, enemy, explosionController;
        

        //private int enemyCount;

        void Start()
        {
            pooler = Instantiate(Resources.Load<GameObject>("Prefabs/PoolerPrefab"));
            pooler.name = "Pooler";

            player = Instantiate(Resources.Load<GameObject>("Prefabs/PlayerPrefab"));
            player.name = "Player";

            enemy = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyPrefab"));
            enemy.name = "Enemy";

            explosionController = Instantiate(Resources.Load<GameObject>("Prefabs/ExplosionsControllerPrefab"));
            explosionController.name = "ExplosionController";

            return;
        }

        void Update()
        {
        
        }
    }
}
