using UnityEngine;
using UnityEngine.UI;

public class ControlloGioco : MonoBehaviour
{
    public GameObject tabletPrefab;    // Prefab del Tablet
    public GameObject tPanel;         // Oggetto TPanel nella scena
    public GameObject spaceShip;      // Oggetto SpaceShip nella scena
    public Button explorationButton;  // Bottone Esplorazione nel Tablet
    public KeyCode toggleTabletKey = KeyCode.T; // Tasto per attivare/disattivare il tablet

    private GameObject instantiatedTablet; // Riferimento al tablet istanziato

    void Start()
    {
        // Trova il canvas nel tablet prefab e attiva UIiniziale
        instantiatedTablet = Instantiate(tabletPrefab, Vector3.zero, Quaternion.identity);
        GameObject can = instantiatedTablet.transform.Find("Can").gameObject;
        GameObject uiIniziale = can.transform.Find("UIiniziale").gameObject;
        uiIniziale.SetActive(true);

        // Disattiva gli altri oggetti
        tPanel.SetActive(false);
        spaceShip.SetActive(false);

        // Configura il bottone Esplorazione
        explorationButton.onClick.AddListener(StartExploration);
    }

    void Update()
    {
        // Riattiva il tablet quando si preme il tasto T
        if (Input.GetKeyDown(toggleTabletKey))
        {
            instantiatedTablet.SetActive(true);
            tPanel.SetActive(false);
            spaceShip.SetActive(false);

            GameObject can = instantiatedTablet.transform.Find("Can").gameObject;
            GameObject uiIniziale = can.transform.Find("UIiniziale").gameObject;
            uiIniziale.SetActive(true);
        }
    }

    void StartExploration()
    {
        // Disattiva il tablet e attiva SpaceShip e TPanel
        instantiatedTablet.SetActive(false);
        spaceShip.SetActive(true);
        tPanel.SetActive(true);
    }
}