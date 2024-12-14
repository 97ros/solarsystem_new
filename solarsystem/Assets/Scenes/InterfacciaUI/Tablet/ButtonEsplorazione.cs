using UnityEngine;
using UnityEngine.UI;

public class ButtonEsplorazione : MonoBehaviour
{
    public GameObject canvaTablet;
    public GameObject eventSystemTablet;
    public GameObject cameraTablet;
    public GameObject eventSystem;
    public GameObject spaceShip;
    public Camera mainCamera;

    private CursorLockMode cursorLockMode;
    private bool cursorVisible;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnEsplorazioneClicked);
        
        // Salviamo la configurazione attuale del cursore
        cursorLockMode = Cursor.lockState;
        cursorVisible = Cursor.visible;
    }

    private void OnEsplorazioneClicked()
    {
        // Disattiviamo gli oggetti relativi al tablet
        canvaTablet.SetActive(false);
        eventSystemTablet.SetActive(false);
        cameraTablet.SetActive(false);
        
        // Attiviamo gli oggetti per l'esplorazione
        eventSystem.SetActive(true);
        spaceShip.SetActive(true);
        
        // Ripristiniamo lo stato del cursore
        Cursor.lockState = cursorLockMode;
        Cursor.visible = cursorVisible;
    }
}
