using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaEnemy : MonoBehaviour
{
    [Header("Configurações de Vida do Inimigo")]
    public int maxHealth = 50;
    public int currentHealth;

    [Header("Interface de Vida (opcional)")]
    [Tooltip("Slider que representa a vida do Inimigo (se houver).")]
    public Slider healthSlider;

    Animator anime;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        anime = GetComponent<Animator>();
    }

    // Método para aplicar dano ao inimigo
    public void TakeDamage(int damage)
    {

        anime.SetTrigger("Hurt");

        currentHealth -= damage;
        Debug.Log("Inimigo recebeu " + damage + " de dano. Vida atual: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método chamado quando o inimigo morre
    void Die()
    {
        Debug.Log("Inimigo morreu!");
        // Aqui você pode adicionar efeitos de morte, animações, etc.
        Destroy(gameObject);
    }
}
