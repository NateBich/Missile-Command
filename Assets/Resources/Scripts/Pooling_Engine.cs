using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    
    public class Pooling_Engine : MonoBehaviour
    {
        //Creates a list of objects, names them, sets there location and set them to inactive
        public List<GameObject> ObjectsPool(GameObject gameObject, Vector3 position, int numOfObjects, string name)
        {
            List<GameObject> objList = new List<GameObject>();

            if (objList != null)
                objList.Clear();

            for (int i = 0; i < numOfObjects; i++)
            {
                objList.Add(Instantiate(gameObject));
                objList[i].SetActive(false);
                objList[i].transform.position = position;
                objList[i].transform.rotation = Quaternion.identity;
                objList[i].name = name;
            }
            return objList;
        }

        public List<GameObject> PoolObjects(List<GameObject> gameObjects, GameObject gameObject, GameObject senderGameObject, int numOfObjects, string name, string parentName)
        {
            if (gameObjects != null)
                gameObjects.Clear();

            gameObjects = ObjectsPool(gameObject, new Vector3(0f, -20f, 9f), numOfObjects, name);

            GameObject parent = new GameObject
            {
                name = parentName
            };
            parent.transform.SetParent(senderGameObject.transform);
            parent.transform.position = new Vector3(0f, -20f, 0f);
            for (int i = 0; i < numOfObjects; i++)
                gameObjects[i].transform.SetParent(parent.transform);

            return gameObjects;
        }
    }
}