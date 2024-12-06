using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonClip;  // Il suono da riprodurre
    private AudioSource audioSource;

    void Start()
    {
        // Trova il componente AudioSource nella scena
        audioSource = FindObjectOfType<AudioSource>();
    }

    public void PlaySound()
    {
        // Riproduce il suono
        if (audioSource != null && buttonClip != null)
        {
            audioSource.PlayOneShot(buttonClip);
        }
    }
}
