using UnityEngine;

public class NewAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        // Ottieni il componente Animator associato al pianeta
        animator = GetComponent<Animator>();
    }

    public void PlayNewAnimationAnimation()
    {
        // Avvia l'animazione
        animator.Play("New Animation");
    }
}
