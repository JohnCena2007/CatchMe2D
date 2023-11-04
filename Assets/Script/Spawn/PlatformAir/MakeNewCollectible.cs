using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNewCollectible : MonoBehaviour
{
    [SerializeField]private GameObject[] collections;
    private void OnEnable() {
        for (int i = 0; i < collections.Length; i++)
        {
            collections[i].gameObject.SetActive(true);
        }
    }
}
