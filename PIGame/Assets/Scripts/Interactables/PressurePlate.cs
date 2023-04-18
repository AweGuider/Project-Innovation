using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private int _team;

    [SerializeField]
    private bool _isPressed;

    [SerializeField]
    private Material _material;
    [SerializeField]
    private GameObject _cylinder;

    private void Start()
    {
        _isPressed = false;
        if (_cylinder == null)
        {
            _cylinder = transform.GetChild(0).gameObject;
        }
        //if (_team == 0) _team = int.Parse(transform.parent.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isPressed) return;

        if (_material == null)
        {
            Debug.LogError($"You didn't put a new material!");
            return;
        }

        PlayerData player = other.GetComponent<PlayerData>();
        if (player != null && player.GetTeam() == _team)
        {
            player.IncrementAmountOfPlatesPressed(1);
            _isPressed = true;
            _cylinder.GetComponent<MeshRenderer>().material = _material;
        }
    }
}
