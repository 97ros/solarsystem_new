using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Assegna il tuo Panel dal Canvas
    private Animator panelAnimator;

    void Start()
    {
        // Ottieni il componente Animator dal pannello
        panelAnimator = panel.GetComponent<Animator>();
        ShowPanel();
    }

    void ShowPanel()
    {
        panel.SetActive(true); // Assicurati che il pannello sia attivo
        // Attiva l'animazione di apertura
        panelAnimator.SetTrigger("OpenPanel");
        // Dopo 8 secondi, attiva la chiusura
        Invoke("StartCloseAnimation", 8f);
    }

    void StartCloseAnimation()
    {
        // Attiva il parametro booleano per la transizione a CloseWarning
        panelAnimator.SetBool("ClosePanel", true);
        // Disattiva il pannello dopo la fine dell'animazione di chiusura
        StartCoroutine(DisablePanelAfterAnimation());
    }

    System.Collections.IEnumerator DisablePanelAfterAnimation()
    {
        // Ottieni la lunghezza dell'animazione CloseWarning
        float closeAnimationLength = 0f;
        AnimationClip[] clips = panelAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "CloseWarning")
            {
                closeAnimationLength = clip.length;
                break;
            }
        }

        // Aspetta la fine dell'animazione CloseWarning
        yield return new WaitForSeconds(closeAnimationLength);

        // Disattiva il pannello
        panel.SetActive(false);
        // Resetta il parametro booleano per la prossima apertura
        panelAnimator.SetBool("ClosePanel", false);
    }
}