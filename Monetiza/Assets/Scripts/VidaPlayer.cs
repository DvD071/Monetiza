using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VidaPlayer : MonoBehaviour
{
    [Header("Configura��es de Vida")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Interface de Vida")]
    [Tooltip("Slider que representa a vida do Player.")]
    public Slider healthSlider;
   

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        
    }

    // M�todo para aplicar dano ao Player
    public void TakeDamage(int damage)
    {
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

    // M�todo para curar o Player (opcional)
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

    // M�todo chamado quando a vida do Player chega a zero
    void Die()
    {
        Debug.Log("Player morreu!");
        // Adicione aqui a l�gica de Game Over ou reinicializa��o da cena
    }

}
