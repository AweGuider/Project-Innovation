using Photon.Pun;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private MapManager mm;

    [SerializeField]
    private string _role;
    [SerializeField]
    private int _team;

    [Header("Animation Related")]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int _plateCount;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        _plateCount = 0;
        mm = GameObject.Find("MapManager").GetComponent<MapManager>();
    }
    public string GetRole()
    {
        return _role;
    }
    public int GetTeam()
    {
        return _team;
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
        if (_plateCount == 5)
        {
            mm.OpenDoors();
        }
    }
}
