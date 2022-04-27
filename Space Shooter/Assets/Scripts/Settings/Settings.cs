using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour{

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currindex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currindex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currindex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }

    public void back(int x){
        if(x == 0){
         SceneManager.LoadScene(0);
        }
    }   

    public void SetVolume(float volume){
        audioMixer.SetFloat("MainVolume", volume);
    }

    public void SetQuality(int quality){
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetScreen(bool screen){
        Screen.fullScreen = screen;
    }
}
