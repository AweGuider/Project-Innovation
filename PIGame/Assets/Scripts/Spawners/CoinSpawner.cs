using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> coins;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject walls;
    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectWithTag("Walls");
        coins = new();
        for (int i = 0; i < Coin.maxAmount; i++)
        {
            while (true)
            {
                Vector3 tempPos = new(Random.Range(-1.5f, 55), coinPrefab.transform.position.y, Random.Range(-27, 9.5f));

                // Player is not yet connected when its trying to create coins
                // Maybe make spawners scriptable objects and create them when OnJoinedRoom()
                //GameObject coin = PhotonNetwork.Instantiate(coinPrefab.name, tempPos, Quaternion.identity);
                //coins.Add(coin);

                break;
                //bool isColliding = Physics.CheckBox(tempPos, coinPrefab.GetComponent<Renderer>().bounds.extents, Quaternion.identity, LayerMask.GetMask("Walls"));
                //if (!isColliding)
                //{
                //    coins.Add(coin);
                //    break;
                //}
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Coin.amountOfCoins <= 0)
        {
            Destroy(door);
        }

        //List<GameObject> deleteList = new();
        //foreach (GameObject coin in coins)
        //{
        //    if (coin.GetComponent<Coin>().collected)
        //    {
        //        deleteList.Add(coin);
        //        Debug.Log($"Coin collected in SPAWNER");
        //    }
        //}
        //foreach (GameObject coin in deleteList)
        //{
        //    coins.Remove(coin);
        //    Destroy(coin);
        //}
    }
}
