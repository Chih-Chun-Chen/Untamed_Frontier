using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    public Button backBtn;
    public Slider fpsSlider;
    public TMP_Dropdown musicDropdown;
    public Toggle backgroundMusicToggle;
    public Slider volumeSlider;
    public TMP_Dropdown crosshairColorDropdown;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
        backBtn.onClick.AddListener(BackToMain);
    }

    // Update is called once per frame
    void Update()
    {
        MainManager.Instance.fpsSliderValue = fpsSlider.value;
        MainManager.Instance.musicDropdownValue = musicDropdown.value;
        MainManager.Instance.backgroundMusicToggleValue = backgroundMusicToggle.isOn ? 1 : 0;
        MainManager.Instance.volumeSliderValue = volumeSlider.value;
        MainManager.Instance.crosshairColorDropdownValue = crosshairColorDropdown.value;
    }

    public void SaveSettings()
    {
        // Save your settings using PlayerPrefs
        PlayerPrefs.SetFloat("ShowFPS", fpsSlider.value);
        PlayerPrefs.SetInt("MusicDropdownIndex", musicDropdown.value);
        PlayerPrefs.SetInt("BackgroundMusic", backgroundMusicToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("CrosshairColorIndex", crosshairColorDropdown.value);

        PlayerPrefs.Save(); // Writes all modified preferences to disk
    }

    private void LoadSettings()
    {
        // Load your settings, with a default value in case it's the first time
        fpsSlider.value = PlayerPrefs.GetFloat("ShowFPS", 150.0f); // Assuming the slider is for a binary setting
        musicDropdown.value = PlayerPrefs.GetInt("MusicDropdownIndex", 0);
        backgroundMusicToggle.isOn = PlayerPrefs.GetInt("BackgroundMusic", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        crosshairColorDropdown.value = PlayerPrefs.GetInt("CrosshairColorIndex", 0);

        /*
        // After loading, add the listeners to save settings whenever they change
        fpsSlider.onValueChanged.AddListener(value => SaveSettings());
        musicDropdown.onValueChanged.AddListener(value => SaveSettings());
        backgroundMusicToggle.onValueChanged.AddListener(value => SaveSettings());
        volumeSlider.onValueChanged.AddListener(value => SaveSettings());
        crosshairColorDropdown.onValueChanged.AddListener(value => SaveSettings());
        */
    }
        
    void BackToMain()
    {
        SaveSettings();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Splash Screen");
    }
}
