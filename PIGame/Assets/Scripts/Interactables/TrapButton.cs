using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapButton : MonoBehaviour
{
    [SerializeField]
    private TrapController _trapController;
    [SerializeField]
    private bool _selected;
    [SerializeField]
    private bool _activated;

    [SerializeField]
    private float _cooldown;
    [SerializeField]
    protected static bool _isCooldown;

    [SerializeField]
    private TrapType _type;
    [SerializeField]
    private int _id;
    public enum TrapType
    {
        Door = 1,
        FB = 2,
        CR = 3,
        TT
    }

    // Start is called before the first frame update
    void Start()
    {
        _cooldown = 10f;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetTrapController(TrapController tc)
    {
        _trapController = tc;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    private void OnClick()
    {
        if (_isCooldown) return;
        switch (_type)
        {
            case TrapType.Door:
            case TrapType.FB:
            case TrapType.CR:
                if (!_selected)
                {
                    _selected = true;
                    _trapController.ActivateTrap(_type, _id);
                }
                else if (_selected && !_activated)
                {
                    StartCoroutine(TrapCooldown());
                }
                break;
            case TrapType.TT:
                // TODO: Make a cooldown for train activation/deactivation
                _activated = !_activated;
                _trapController.ActivateTrap(_type, _activated);
                break;
        }
    }

    IEnumerator TrapCooldown()
    {
        _activated = true;
        _trapController.ActivateTrap(_type, _id);
        _isCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        _selected = false;
        _activated = false;
        _isCooldown = false;
    }
}
