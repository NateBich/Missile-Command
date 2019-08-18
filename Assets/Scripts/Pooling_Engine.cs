using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand
{
    
    public class Pooling_Engine : MonoBehaviour
    {
        //Creates a list of objects, names them, sets there location and set them to inactive
        public List<GameObject> PoolObjects(GameObject gameObject, Vector3 position, int numOfObjects, string name)
        {
            List<GameObject> objList = new List<GameObject>();

            if (objList != null)
                objList.Clear();

            for (int i = 0; i < numOfObjects; i++)
            {
                objList.Add(Instantiate(gameObject));
                objList[i].transform.position = position;
                objList[i].transform.rotation = Quaternion.identity;
                objList[i].name = name;
                objList[i].SetActive(false);
            }
            return objList;
        }
    }
}