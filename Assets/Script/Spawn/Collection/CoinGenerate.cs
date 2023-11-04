using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerate : MonoBehaviour
{
    public ObjectPooler coinPool;
    public float TanSoSpawnCoin;
    public void spawnCoin(Vector3 pos){
        int top_or_down = Random.Range(0,3);
        switch(top_or_down){
            case 0:
                GameObject coin1Down=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coin1Down.transform.position=new Vector3(pos.x,pos.y + 4f ,pos.z);
                coin1Down.SetActive(true);

                GameObject coin2Down=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coin2Down.transform.position=new Vector3(pos.x - 3.5f,pos.y + 4f ,pos.z);
                coin2Down.SetActive(true);

                GameObject coin3Down=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coin3Down.transform.position=new Vector3(pos.x + 3.5f,pos.y + 4f, pos.z);
                coin3Down.SetActive(true);
                break;

            case 1:
                GameObject coin1Top=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coin1Top.transform.position=new Vector3(pos.x,pos.y + 6.5f ,pos.z);
                coin1Top.SetActive(true);

                GameObject coin2Top=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coin2Top.transform.position=new Vector3(pos.x - 3.5f,pos.y +  6.5f ,pos.z);
                coin2Top.SetActive(true);

                GameObject coin3Top=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coin3Top.transform.position=new Vector3(pos.x + 3.5f,pos.y +  6.5f, pos.z);
                coin3Top.SetActive(true);
                break;
            case 2:
                GameObject coinDown1=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coinDown1.transform.position=new Vector3(pos.x,pos.y + 7.5f ,pos.z);
                coinDown1.SetActive(true);

                GameObject coinTop=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coinTop.transform.position=new Vector3(pos.x - 4.8f,pos.y + 4f ,pos.z);
                coinTop.SetActive(true);

                GameObject coinDown2=coinPool.GetComponent<ObjectPooler>().getPoolObject();
                coinDown2.transform.position=new Vector3(pos.x + 4.8f,pos.y + 4f, pos.z);
                coinDown2.SetActive(true);
                break;
        }

    }
}
