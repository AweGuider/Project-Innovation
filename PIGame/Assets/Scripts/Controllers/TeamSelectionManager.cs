using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject chosenButton;

    internal GameObject GetChosenButton()
    {
        return chosenButton;
    }
    internal void SetChosenButton(GameObject button)
    {
        if (chosenButton != button)
        {
            chosenButton = button;
        }
    }

    public string GetRole()
    {
        return chosenButton.GetComponent<RoleButton>().GetRole();
    }

    public int GetTeam()
    {
        return chosenButton.GetComponent<RoleButton>().GetTeam();
    }
}
