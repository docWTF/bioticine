using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;

    public void CreditsOff()
    {
        CreditsMenu.SetActive(false);
    }

    public void CreditsOn()
    {
        CreditsMenu.SetActive(true);
    }
    public void SettingsOff()
    {
        SettingsMenu.SetActive(false);
    }

    public void SettingsOn()
    {
        SettingsMenu.SetActive(true);
    }

}