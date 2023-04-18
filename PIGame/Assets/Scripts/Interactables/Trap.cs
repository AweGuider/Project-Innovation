using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviourPunCallbacks
{
    [SerializeField]
    protected int _id;

    [SerializeField]
    protected bool _selected;


    public int GetID()
    {
        return _id;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public void SetSelected(bool s)
    {
        _selected = s;
    }

    public abstract void SelectTrap();
    public abstract void ActivateTrap();
}
