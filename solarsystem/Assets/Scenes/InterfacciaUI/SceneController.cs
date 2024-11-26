using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        // Carica entrambe le scene additive
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("TabletUI", LoadSceneMode.Additive);
    }
}
