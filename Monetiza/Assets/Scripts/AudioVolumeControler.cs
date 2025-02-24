using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioVolumeControler : MonoBehaviour
{
    [Header("Referências")]
    [Tooltip("Componente AudioSource que será controlado.")]
    public AudioSource audioSource;
    [Tooltip("Slider que controla o volume.")]
    public Slider volumeSlider;

    void Start()
    {
        // Verifica se as referências foram atribuídas
        if (audioSource == null)
        {
            Debug.LogError("AudioSource não foi atribuído!");
        }
        if (volumeSlider == null)
        {
            Debug.LogError("Volume Slider não foi atribuído!");
        }
        else
        {
            // Inicializa o slider com o volume atual do AudioSource
            volumeSlider.value = audioSource.volume;
            // Adiciona um listener para mudar o volume quando o valor do slider for alterado
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    // Método chamado sempre que o slider é modificado
    public void ChangeVolume(float newVolume)
    {
        if (audioSource != null)
        {
            audioSource.volume = newVolume;
        }
    }
}
