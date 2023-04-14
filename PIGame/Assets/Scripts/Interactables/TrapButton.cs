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
    private TrapType _type;
    [SerializeField]
    private int _id;
    public enum TrapType
    {
        Door = 1,
        FB = 2,
        CR = 3
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
        if (!_selected)
        {
            _selected = true;
            _trapController.ActivateTrap(_type, _id);
        }
        else if (_selected && !_activated)
        {
            StartCoroutine(TrapCooldown());
        }
    }

    IEnumerator TrapCooldown()
    {
        _activated = true;
        _trapController.ActivateTrap(_type, _id);
        yield return new WaitForSeconds(_cooldown);
        _selected = false;
        _activated = false;
    }
}
