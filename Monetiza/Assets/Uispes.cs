using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Uispes : MonoBehaviour
{
    public GameObject Creds, WinScreen;

    private void Update()
    {
        if (VidaEnemy.Win) 
        {
            WinScreen.SetActive(true);
        }
    }
    public void BackMenu() 
    {
        SceneManager.LoadScene("Menu");
    }
    public void Creditos(bool bass) 
    {
        Creds.SetActive(bass);
    }
}
