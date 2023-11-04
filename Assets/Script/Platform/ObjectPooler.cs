using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject poolObject;
    public int amount;
    List<GameObject> poolObjectArray;
    private void Awake() 
    {
        poolObjectArray=new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject x =Instantiate(poolObject);
            x.SetActive(false);
            poolObjectArray.Add(x);
        }
    }

    public GameObject getPoolObject(){
        for (int i = 0; i < poolObjectArray.Count; i++)
        {
            if(!poolObjectArray[i].activeInHierarchy){
                return poolObjectArray[i];
            }
        }
        GameObject x =Instantiate(poolObject);
        x.SetActive(false);
        poolObjectArray.Add(x);
        return x;
    }
}
