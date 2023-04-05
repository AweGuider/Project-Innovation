using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleButton : MonoBehaviour
{
    [SerializeField]
    private bool _used;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private string _role;
    [SerializeField]
    private int _team;

    [SerializeField] TeamSelectionManager manager;

    // Start is called before the first frame update
    void Start()
    {
        if (manager == null) manager = transform.parent.transform.parent.GetComponent<TeamSelectionManager>();
        GetComponent<Button>().onClick.AddListener(OnClick);
        _role = gameObject.name;
        _team = int.Parse(transform.parent.name);
        _text.text = _role;
    }

    private void OnClick()
    {
        if (!IsUsed())
        {
            if (manager.GetChosenButton() != null)
            {
                manager.GetChosenButton().GetComponent<RoleButton>().ResetButton();
            }
            manager.SetChosenButton(gameObject);
            SetText(PhotonNetwork.NickName);
            SetUsed(true);
        }
        else
        {
            if (PhotonNetwork.LocalPlayer.IsLocal)
            {
                manager.SetChosenButton(null);
                ResetButton();
            }
        }
    }

    public void ResetButton()
    {
        SetText(GetRole());
        SetUsed(false);
    }

    public bool IsUsed()
    {
        return _used;
    }

    public string GetRole()
    {
        return _role;
    }

    public int GetTeam()
    {
        return _team;
    }

    public void SetUsed(bool u)
    {
        _used = u;
    }

    public void SetText(string t)
    {
        _text.text = t;
    }
}
