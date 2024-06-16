using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Canvas MainMenuUI;
    [SerializeField] private Canvas SettingsMenuUI;
    [SerializeField] private Dropdown QualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensivitySlider;
    [SerializeField] private Text sensivityText;
    [SerializeField] private Text volumeText;
    public void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
            volumeText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
        }
        else
        {
            volumeSlider.value = 0.5f;
            AudioListener.volume = 0.5f;
            volumeText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
        }
        if (PlayerPrefs.HasKey("Sensivity"))
        {
            sensivitySlider.value = PlayerPrefs.GetFloat("Sensivity");
            sensivityText.text = (sensivitySlider.value / 100f).ToString("0.0");
        }
        else
        {
            sensivitySlider.value = 100f;
            sensivityText.text = (sensivitySlider.value / 100f).ToString("0.0");
            PlayerPrefs.SetFloat("Sensivity", sensivitySlider.value);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("Quality"))
        {
            QualityDropdown.value = PlayerPrefs.GetInt("Quality");
        }
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            if (PlayerPrefs.GetInt("Fullscreen") == 1)
            {
                fullscreenToggle.isOn = true;
            }
            else
            {
                fullscreenToggle.isOn = false;
            }
        }
    }
    public void SetVolume()
    {
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
        AudioListener.volume = volume;
        volumeText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
    }
    public void SetSensivity()
    {
        PlayerPrefs.SetFloat("Sensivity", sensivitySlider.value);
        PlayerPrefs.Save();
        sensivityText.text = (sensivitySlider.value / 100f).ToString("0.0");
    }

    public void SetQuality(int qualityIndex)
    {
        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen()
    {
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void BackToMainMenu()
    {
        SettingsMenuUI.gameObject.SetActive(false);
        MainMenuUI.gameObject.SetActive(true);
    }
}