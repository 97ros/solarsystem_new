using UnityEngine;

public class OpenLayersAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        // Ottieni il componente Animator associato al pianeta
        animator = GetComponent<Animator>();
    }

    public void PlayOpenLayersAnimation()
    {
        // Avvia l'animazione
        animator.Play("OpenLayers");
    }
}
