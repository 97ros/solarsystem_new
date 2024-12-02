using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        // Controlla se il tasto T è stato premuto
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Cambia scena
            SceneManager.LoadScene("TabletUI");
        }
    }
}
