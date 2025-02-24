using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    [SerializeField] public string nomeCena;
    private bool isPaused = false;
    
    public void CarregarCena()
    {
        SceneManager.LoadScene(nomeCena);
    }
   void Update()
   {
   if (Input.GetKeyDown(KeyCode.Escape))
   {
      TogglePause();
   }
   }
  void TogglePause()
  {
   isPaused = !isPaused;
   pauseMenuCanvas.SetActive(isPaused);
    Time.timeScale = isPaused ? 0 : 1;
  }
}
