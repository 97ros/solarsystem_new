using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Animator glowAnimator;

    private static readonly string GlowExpand = "GlowExpand";
    private static readonly string GlowShrink = "GlowShrink";

    private void OnEnable()
    {
        // Quando il GameObject diventa attivo, forziamo il glow a restringersi
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowShrink, 0, 1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowExpand);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowShrink);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowShrink); // Restringe il glow al clic
        }
    }
}
