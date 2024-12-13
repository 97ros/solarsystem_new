using UnityEngine;
using UnityEngine.UI; // Importa per accedere agli elementi UI
using UnityEngine.Events;

public class ButtonClickHandler : MonoBehaviour
{
    // Riferimento al bottone e al componente da attivare
    public Button button;
    public GameObject componentToActivate; // Oggetto che vuoi attivare
    public GameObject componentProtetto;
    public bool isActive = false; // Variabile che tiene traccia dello stato del componente

    public void Start() // quando premi il bottone
    {
        if (componentToActivate != null)
        {
            isActive = !isActive; // Cambia lo stato del componente
            componentToActivate.SetActive(isActive);
        }
        if (isActive){
            DisattivaOggetti();
        }
    }

    void DisattivaOggetti()
    {
        // Trova tutti gli oggetti nella scena
        GameObject[] oggetti = GameObject.FindObjectsOfType<GameObject>();

        // Cicla attraverso tutti gli oggetti e li disattiva
        foreach (GameObject oggetto in oggetti)
        {
            if (oggetto!=componentProtetto){
                oggetto.SetActive(false);
            }     
        }
    }
}
