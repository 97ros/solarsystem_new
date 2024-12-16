using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class PlanetMenu : MonoBehaviour
{
    public TMP_Dropdown planetDropdown;
    public GameObject planetInfoPanel;
    public TMP_Text nameText, distanceText, rotationTimeText, descriptionText;
    public Button closeButton;
    public Animator planetPanelAnimator;
    public TMP_Text funFactText;
    public Image planetImage;
    public RawImage videoDisplay;
    public VideoPlayer planetVideoPlayer;
    public RenderTexture renderTexture;
    public CanvasGroup infoPanelCanvasGroup; // Aggiunto Canvas Group

    [System.Serializable]
    public struct PlanetInfo
    {
        public string name;
        public float distanceFromSunValue;
        public string distanceFromSunUnit;
        public float rotationTimeValue;
        public string rotationTimeUnit;
        public string funFact;
        public Sprite planetImage;
        public VideoClip planetVideo;
        public string description;
    }

    public PlanetInfo[] planets;

    void Start()
    {
        planetDropdown.ClearOptions();
        planetDropdown.options.Add(new TMP_Dropdown.OptionData("Select a planet"));

        foreach (var planet in planets)
        {
            planetDropdown.options.Add(new TMP_Dropdown.OptionData(planet.name));
        }

        planetDropdown.onValueChanged.AddListener(OnPlanetSelected);
        planetDropdown.value = 0;
        planetDropdown.RefreshShownValue();

        planetInfoPanel.SetActive(false);
        closeButton.onClick.AddListener(ClosePanel);
    }

    void OnPlanetSelected(int index)
    {
        if (index > 0)
        {
            UpdatePlanetInfoPanel(index);
        }
        else
        {
            planetInfoPanel.SetActive(false);
        }
    }

    void UpdatePlanetInfoPanel(int index)
    {
        if (planetInfoPanel.activeSelf)
        {
            StartCoroutine(FadeOutInInfoPanel(index));
        }
        else
        {
            planetInfoPanel.SetActive(true);
            UpdatePlanetInfo(index);
            planetPanelAnimator.SetTrigger("OpenPanel");
        }
    }


    private IEnumerator FadeOutInInfoPanel(int index)
    {
        float fadeOutDuration = 0.5f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeOutDuration)
        {
            infoPanelCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
        infoPanelCanvasGroup.alpha = 0f;

        UpdatePlanetInfo(index);


        float fadeInDuration = 0.5f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInDuration)
        {
            infoPanelCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        infoPanelCanvasGroup.alpha = 1f;

    }


     void UpdatePlanetInfo(int index) {

        PlanetInfo selectedPlanet = planets[index - 1];
        nameText.text = selectedPlanet.name;
        distanceText.text = "- Distanza media dal Sole: " + selectedPlanet.distanceFromSunValue + " " + selectedPlanet.distanceFromSunUnit;
        rotationTimeText.text = "- Durata dell'orbita: " + selectedPlanet.rotationTimeValue + " " + selectedPlanet.rotationTimeUnit + " terrestri";
        funFactText.text = "Lo sapevi?\n" + selectedPlanet.funFact;
        descriptionText.text = selectedPlanet.description;
        planetImage.sprite = selectedPlanet.planetImage;

        if (selectedPlanet.name == "Nettuno" && selectedPlanet.planetVideo != null)
        {
            videoDisplay.gameObject.SetActive(true);
            planetImage.gameObject.SetActive(false);
            planetVideoPlayer.renderMode = VideoRenderMode.RenderTexture;
            planetVideoPlayer.targetTexture = renderTexture;
            planetVideoPlayer.clip = selectedPlanet.planetVideo;
            planetVideoPlayer.Play();
        }
        else
        {
            videoDisplay.gameObject.SetActive(false);
            planetImage.gameObject.SetActive(true);
        }
    }

    void ClosePanel()
    {
        planetPanelAnimator.SetTrigger("ClosePanel");
        planetVideoPlayer.Stop();
    }

    void ClosePanelAnimationComplete()
    {
        planetInfoPanel.SetActive(false);
        planetDropdown.onValueChanged.RemoveListener(OnPlanetSelected);
        planetDropdown.value = 0;
        planetDropdown.RefreshShownValue();
        planetDropdown.onValueChanged.AddListener(OnPlanetSelected);
    }

    void LateUpdate()
    {
        if (planetPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("PanelClose") &&
            planetPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            ClosePanelAnimationComplete();
        }
    }
}