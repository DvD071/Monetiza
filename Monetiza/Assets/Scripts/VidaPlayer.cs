using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VidaPlayer : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int maxHealth = 100;
    public float currentHealth;

    [Header("Interface de Vida")]
    [Tooltip("Slider que representa a vida do Player.")]
    public Slider healthSlider;
    public GameObject gameOver;
    public LayerMask Cura;


    float timer = 0;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        gameOver.SetActive(false);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0 && currentHealth < maxHealth) 
        {
            currentHealth += 0.01f;
        }
        Debug.Log(timer + " " + currentHealth);
        healthSlider.value = currentHealth;
       
       
    }

    // Método para aplicar dano ao Player
    public void TakeDamage(int damage)
    {
        timer = 10;
        Movement.animator.SetTrigger("Hurt");
        currentHealth -= damage;
        Debug.Log("Player recebeu " + damage + " de dano. Vida atual: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para curar o Player (opcional)
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
       
        Debug.Log("Player curado em " + amount + ". Vida atual: " + currentHealth);
    }

    // Método chamado quando a vida do Player chega a zero
    void Die()
    { 
        gameOver.SetActive(true);
        Debug.Log("Player morreu!");
        Time.timeScale = 0f;

        // Adicione aqui a lógica de Game Over ou reinicialização da cena
         
    }
    public void Derrota()
    {
        SceneManager.LoadScene("Jogo");
        Time.timeScale = 1f;
    }
   
    

}
