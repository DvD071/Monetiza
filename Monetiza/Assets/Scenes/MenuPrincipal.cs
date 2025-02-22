using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string levelJogo;
    [SerializeField] private GameObject painelMenuinici;
    [SerializeField] private GameObject painelOpcoes;
    public void Jogar()
    {
        SceneManager.LoadScene(levelJogo);
    }
    public void AbrirOpcoes()
    {
        painelMenuinici.SetActive(false);
        painelOpcoes.SetActive(true);
        
    }
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false );
        painelMenuinici.SetActive(true);
    }
    public void SairJogo()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }

}
