using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{ 
    public class Game_Engine : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;

        private int enemyCount;

        void Start()
        {
            player = Instantiate(Resources.Load<GameObject>("Prefabs/PlayerPrefab"));
            player.name = "PlayerObject";

            enemy = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyPrefab"));
            enemy.name = "EnemyObject";

            return;
        }

        void Update()
        {
        
        }
    }
}
