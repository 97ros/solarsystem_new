using UnityEngine;

public class ButtonGlowEffectHome : MonoBehaviour
{
    public Animator glowAnimator; // Riferimento all'Animator di GlowEffect

    public void OnMouseEnter()
    {
        if (glowAnimator != null)
        {
            //glowAnimator.ResetTrigger("Shrink"); // Resetta il trigger precedente
            //glowAnimator.Se(tTrigger("Expand"); // Attiva l'espansione
            glowAnimator.Play("GlowExpand_home");
        }
    }

    public void OnMouseExit()
    {
        if (glowAnimator != null)
        {
            //glowAnimator.ResetTrigger("Expand"); // Resetta il trigger precedente
            //glowAnimator.SetTrigger("Shrink"); // Attiva la contrazione
            glowAnimator.Play("GlowShrink_home");

        }
    }
}
