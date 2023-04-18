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

    [Header("Train Trap Related")]
    [SerializeField]
    private List<GameObject> _trainTraps;
    // Start is called before the first frame update
    void Start()
    {
        _doorTraps = new List<GameObject>();
        _fallingBlockTraps = new List<GameObject>();
        _cashRegisterTraps = new List<GameObject>();
        _trainTraps = new List<GameObject>();

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

        _trainTraps = GameObject.FindGameObjectsWithTag("Train")
                                    .Select((transform, index) =>
                                    {
                                        GameObject go = transform.gameObject;
                                        //Trap t = go.GetComponent<Trap>();
                                        //t.SetID(index);
                                        return go;
                                    })
                                    .ToList();

    }



    [PunRPC]
    public void SelectDoor(int id)
    {
        Debug.LogError($"ACTIVATING Door: ID {id}");
        DoorTrap trap = _doorTraps[id].GetComponent<DoorTrap>();
        trap.SelectTrap();
    }

    [PunRPC]
    public void SelectFB(int id)
    {
        Debug.LogError($"ACTIVATING Falling: ID {id}");
        FBTrap trap = _fallingBlockTraps[id].GetComponent<FBTrap>();
        trap.SelectTrap();
    }

    [PunRPC]
    public void SelectCR(int id)
    {
        Debug.LogError($"ACTIVATING Cash: ID {id}");
        CRTrap trap = _cashRegisterTraps[id].GetComponent<CRTrap>();
        trap.SelectTrap();
    }

    [PunRPC]
    public void SelectTT(bool stopping)
    {
        Debug.LogError($"ACTIVATING Train: Bool {stopping}");
        foreach (GameObject t in _trainTraps)
        {
            Train train = t.GetComponent<Train>();
            train.SelectTrain();
        }
    }


    [PunRPC]
    public void ActivateDoor(int id)
    {
        Debug.LogError($"ACTIVATING Door: ID {id}");
        DoorTrap trap = _doorTraps[id].GetComponent<DoorTrap>();
        trap.ActivateTrap();
    }

    [PunRPC]
    public void ActivateFB(int id)
    {
        Debug.LogError($"ACTIVATING Falling: ID {id}");
        FBTrap trap = _fallingBlockTraps[id].GetComponent<FBTrap>();
        trap.ActivateTrap();
    }

    [PunRPC]
    public void ActivateCR(int id)
    {
        Debug.LogError($"ACTIVATING Cash: ID {id}");
        CRTrap trap = _cashRegisterTraps[id].GetComponent<CRTrap>();
        trap.ActivateTrap();
    }

    [PunRPC]
    public void ActivateTT(bool stopping)
    {
        Debug.LogError($"ACTIVATING Train: Bool {stopping}");
        foreach (GameObject t in _trainTraps)
        {
            Train train = t.GetComponent<Train>();
            train.SetStopped(stopping);
        }
    }
}
