using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField]
    private MapManager mm;

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
    void Start()
    {
        if (mm == null) mm = GetComponent<MapManager>();

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
    private void KidSelected(int team)
    {
        if (team == 1)
        {
            mm.boyKidAnimator.SetBool("Think", true);
        }
        else
        {
            mm.girlKidAnimator.SetBool("Think", true);
        }
    }
    private void KidActivated(int team)
    {
        int randomVoice = 0;
        if (team == 1)
        {
            mm.boyKidAnimator.SetBool("Activate", true);
            randomVoice = Random.Range(2, 4);
        }
        else
        {
            mm.girlKidAnimator.SetBool("Activate", true);
            randomVoice = Random.Range(0, 2);
        }
        AudioManager.instance.PlaySound(AudioManager.AudioType.Voice, randomVoice);
    }
    [PunRPC]
    public void SelectDoor(int id, int team)
    {
        DoorTrap trap = _doorTraps[id].GetComponent<DoorTrap>();
        trap.SelectTrap();
        KidSelected(team);
    }
    [PunRPC]
    public void SelectFB(int id, int team)
    {
        FBTrap trap = _fallingBlockTraps[id].GetComponent<FBTrap>();
        trap.SelectTrap();
        KidSelected(team);
    }
    [PunRPC]
    public void SelectCR(int id, int team)
    {
        CRTrap trap = _cashRegisterTraps[id].GetComponent<CRTrap>();
        trap.SelectTrap();
        KidSelected(team);
    }
    [PunRPC]
    public void SelectTT(bool stopping, int team)
    {
        foreach (GameObject t in _trainTraps)
        {
            Train train = t.GetComponent<Train>();
            train.SelectTrain();
        }
        KidSelected(team);
    }
    [PunRPC]
    public void ActivateDoor(int id, int team)
    {
        DoorTrap trap = _doorTraps[id].GetComponent<DoorTrap>();
        trap.ActivateTrap();
        KidActivated(team);
    }
    [PunRPC]
    public void ActivateFB(int id, int team)
    {
        FBTrap trap = _fallingBlockTraps[id].GetComponent<FBTrap>();
        trap.ActivateTrap();
        KidActivated(team);
    }
    [PunRPC]
    public void ActivateCR(int id, int team)
    {
        CRTrap trap = _cashRegisterTraps[id].GetComponent<CRTrap>();
        trap.ActivateTrap();
        KidActivated(team);
    }
    [PunRPC]
    public void ActivateTT(bool stopping, int team)
    {
        foreach (GameObject t in _trainTraps)
        {
            Train train = t.GetComponent<Train>();
            train.SetStopped(stopping);
        }
        KidActivated(team);
    }
}
