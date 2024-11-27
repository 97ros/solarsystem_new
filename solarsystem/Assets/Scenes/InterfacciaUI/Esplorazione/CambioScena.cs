using UnityEngine;
using UnityEngine.SceneManagement;  // Necessario per gestire le scene
using UnityEngine.UI;  // Necessario per lavorare con UI

public class CambioScena : MonoBehaviour
{
    // Funzione per caricare la scena "TabletUI"
    public void GoToSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
