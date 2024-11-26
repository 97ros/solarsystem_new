using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        // Carica entrambe le scene additive
        SceneManager.LoadScene("SistemaSolare", LoadSceneMode.Additive);
        SceneManager.LoadScene("Tablet", LoadSceneMode.Additive);
    }
}
