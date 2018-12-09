using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {



    private Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;

    private void Start() {
        this.resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;
        int iter = 0;
        for(int i = 0; i < this.resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if (options.Contains(option.ToLower()))
                continue;

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) {
                currentResolution = iter;
            }

            options.Add(option);
            iter++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;
        if (PlayerPrefs.HasKey("Quality")) {
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(qualityDropdown.value);
            qualityDropdown.RefreshShownValue();
        }
        if (PlayerPrefs.HasKey("Volume")) {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        
    }

    public void setVolume(float value) {
        
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void setQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void setFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }

    public void setResolution(int resolutionIndex) {
        int width = int.Parse(resolutionDropdown.options[resolutionIndex].text.Split(' ')[0]);
        int height = int.Parse(resolutionDropdown.options[resolutionIndex].text.Split(' ')[2]);
        Screen.SetResolution(width, height, Screen.fullScreen);
    }


    public void returnMenu() {
        SceneManager.LoadScene("Menu");
    }
}
