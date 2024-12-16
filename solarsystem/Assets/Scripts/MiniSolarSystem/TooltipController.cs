using UnityEngine;
using System.Collections;

public class TooltipController : MonoBehaviour
{
    public Animator tooltipAnimator;
    public Canvas tooltipCanvas; // Riferimento al Canvas del tooltip

    public void HideTooltipWithDelay()
    {
        StartCoroutine(HideTooltipAfterAnimation());
    }

    private IEnumerator HideTooltipAfterAnimation()
    {
        tooltipAnimator.SetTrigger("HideTooltip");

        // Attendere la fine dell'animazione (o un tempo leggermente superiore)
        yield return new WaitForSeconds(tooltipAnimator.GetCurrentAnimatorStateInfo(0).length); // Aggiunto un piccolo margine

        tooltipCanvas.gameObject.SetActive(false);
    }
}