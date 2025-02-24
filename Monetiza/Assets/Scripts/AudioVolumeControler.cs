using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioVolumeControler : MonoBehaviour
{
    [Header("Refer�ncias")]
    [Tooltip("Componente AudioSource que ser� controlado.")]
    public AudioSource audioSource;
    [Tooltip("Slider que controla o volume.")]
    public Slider volumeSlider;

    void Start()
    {
        // Verifica se as refer�ncias foram atribu�das
        if (audioSource == null)
        {
            Debug.LogError("AudioSource n�o foi atribu�do!");
        }
        if (volumeSlider == null)
        {
            Debug.LogError("Volume Slider n�o foi atribu�do!");
        }
        else
        {
            // Inicializa o slider com o volume atual do AudioSource
            volumeSlider.value = audioSource.volume;
            // Adiciona um listener para mudar o volume quando o valor do slider for alterado
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    // M�todo chamado sempre que o slider � modificado
    public void ChangeVolume(float newVolume)
    {
        if (audioSource != null)
        {
            audioSource.volume = newVolume;
        }
    }
}
