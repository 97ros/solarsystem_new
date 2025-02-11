using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    // Riferimenti agli oggetti nel Canvas
    public GameObject logo;
    public GameObject intro;
    public Button inizioButton;

    void Start()
    {
        // Disattivare gli oggetti all'inizio
        logo.SetActive(false);
        intro.SetActive(false);
        inizioButton.gameObject.SetActive(false);

        // Iniziare la coroutines
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
        // Attendere un secondo prima di visualizzare il logo e intro
        yield return new WaitForSeconds(2f);

        // Mostrare il logo e l'intro
        logo.SetActive(true);
        intro.SetActive(true);

        // Attendere 4 secondi
        yield return new WaitForSeconds(6f);

        // Disattivare l'intro
        intro.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        logo.SetActive(false);

        //yield return new WaitForSeconds(0.5f);
        // Attivare il bottone "Inizio"
        inizioButton.gameObject.SetActive(true);
    }
}