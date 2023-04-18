using Photon.Pun;
using UnityEngine;

public abstract class Trap : MonoBehaviourPunCallbacks
{
    [SerializeField]
    protected int _id;

    public int GetID()
    {
        return _id;
    }

    public void SetID(int id)
    {
        _id = id;
    }
    public abstract void SelectTrap();
    public abstract void ActivateTrap();
}
