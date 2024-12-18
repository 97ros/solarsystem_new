using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public enum Language { Italian, English }
    public static Language CurrentLanguage = Language.Italian;

    public static void SetLanguage(Language newLanguage)
    {
        CurrentLanguage = newLanguage;
        Debug.Log("Lingua impostata su: " + newLanguage);

        // Trova l'istanza di SceneManagerController e chiama UpdateTabletLanguage()
        SceneManagerController sceneManager = FindObjectOfType<SceneManagerController>();
        if (sceneManager != null)
        {
            sceneManager.UpdateTabletLanguage();
        }
    }
}