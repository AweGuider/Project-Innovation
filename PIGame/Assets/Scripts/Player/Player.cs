using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] private float numberOfCoinsCollected;
    [SerializeField] private GameObject door;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

        }
    }

    public void AddCoin()
    {
        numberOfCoinsCollected++;
    }
}
