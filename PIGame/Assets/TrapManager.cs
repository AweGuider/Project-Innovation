using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

    [Header("Door Trap Related")]
    [SerializeField]
    private List<GameObject> _doorTraps;

    [Header("Falling Block Trap Related")]
    [SerializeField]
    private List<GameObject> _fallingBlockTraps;

    [Header("Cash Register Trap Related")]
    [SerializeField]
    private List<GameObject> _cashRegisterTraps;
    // Start is called before the first frame update
    void Start()
    {
        _doorTraps = new List<GameObject>();
        _fallingBlockTraps = new List<GameObject>();
        _cashRegisterTraps = new List<GameObject>();

        //foreach (Transform cr in GameObject.FindGameObjectWithTag("CashRegister").transform)
        //{
        //    _cashRegisterTraps.Add(cr.gameObject);
        //}

        _doorTraps = GameObject.FindGameObjectWithTag("Doors")
                                    .transform
                                    .Cast<Transform>()
                                    .Select((transform, index) =>
                                    {
                                        GameObject go = transform.gameObject;
                                        Trap t = go.GetComponent<Trap>();
                                        t.SetID(index);
                                        return go;
                                    })
                                    .ToList();

        _fallingBlockTraps = GameObject.FindGameObjectWithTag("FallingBlocks")
                                    .transform
                                    .Cast<Transform>()
                                    .Select((transform, index) =>
                                    {
                                        GameObject go = transform.gameObject;
                                        Trap t = go.GetComponent<Trap>();
                                        t.SetID(index);
                                        return go;
                                    })
                                    .ToList();
        _cashRegisterTraps = GameObject.FindGameObjectWithTag("CashRegister")
                                    .transform
                                    .Cast<Transform>()
                                    .Select((transform, index) =>
                                    {
                                        GameObject go = transform.gameObject;
                                        Trap t = go.GetComponent<Trap>();
                                        t.SetID(index);
                                        return go;
                                    })
                                    .ToList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void ActivateDoor(int id)
    {
        _doorTraps[id].GetComponent<DoorTrap>().ActivateTrap();
    }

    [PunRPC]
    public void ActivateFB(int id)
    {
        _fallingBlockTraps[id].GetComponent<FBTrap>().ActivateTrap();

    }

    [PunRPC]
    public void ActivateCR(int id)
    {
        _cashRegisterTraps[id].GetComponent<CRTrap>().ActivateTrap();

    }
}
