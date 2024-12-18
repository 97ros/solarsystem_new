using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGlowEffecrHome : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Animator glowAnimator;

    private static readonly string GlowExpandHome = "GlowExpand_home";
    private static readonly string GlowShrinkHome = "GlowShrink_home";

    private void OnEnable()
    {
        // Quando il GameObject diventa attivo, forziamo il glow a restringersi
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowShrinkHome, 0, 1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowExpandHome);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowShrinkHome);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (glowAnimator != null)
        {
            glowAnimator.Play(GlowShrinkHome); // Restringe il glow al clic
        }
    }
}
