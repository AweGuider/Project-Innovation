using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private string _role;
    [SerializeField]
    private int _team;

    [Header("Animation Related")]
    [SerializeField]
    private Animator animator;


    [SerializeField]
    private int _plateCount;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        _plateCount = 0;
    }

    public string GetRole()
    {
        return _role;
    }
    public int GetTeam()
    {
        return _team;
    }
    public PhotonView GetPhotonView()
    {
        return GetComponent<PhotonView>();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SetRole(string r)
    {
        _role = r;
    }
    public void SetTeam(int t)
    {
        _team = t;
    }

    public void SetWalking(bool b)
    {
        animator.SetBool("isWalking", b);
    }

    public int GetPlatesPressed()
    {
        return _plateCount;
    }

    public void IncrementAmountOfPlatesPressed(int n)
    {
        _plateCount += n;
    }
}
