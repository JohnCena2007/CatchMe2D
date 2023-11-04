using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAirSupportWood : MonoBehaviour
{
    [SerializeField]private SupportWoodAir[] dadWoods;
    private void OnEnable() {
        for (int i = 0; i < dadWoods.Length; i++)
        {
            dadWoods[i].resetWoodInChild();
        }
    }


}
