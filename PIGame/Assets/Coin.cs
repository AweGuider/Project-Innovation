using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool collected;
    public static float amountOfCoins;
    public static float maxAmount = 10;

    private void Start()
    {
        maxAmount = 10;
        amountOfCoins = maxAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddCoin();
            amountOfCoins--;
            Destroy(gameObject);
            Debug.Log($"Coin collected IN COIN");
        }
    }
}
