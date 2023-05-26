using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public DontDestroy infoOption;
    public GameObject info;
    
    
    private void Start()
    {
        
        // on cherche l'objet qui ne se détruit pas au chargement d'une autre scene (celle qui contient les sauvegardes des options)
        info = GameObject.FindGameObjectsWithTag("DontDestroy")[0];
        infoOption = info.GetComponent<DontDestroy>();

        resolutions = Screen.resolutions;

        float Len = resolutions.Length;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < Len; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
           
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        SetResolution(infoOption.Resolution);
        SetQuality(infoOption.Quality);
        SetFullScreen(infoOption.FullScreen);
        SetVolume(infoOption.Volume);
        
    }

    public void SetResolution(int resolutionID)
    {
        Resolution resolution = resolutions[resolutionID];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        infoOption.Resolution = resolutionID;
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume",volume);
        infoOption.Volume = volume;
    }

    public void SetQuality (int qualityID)
    {
        QualitySettings.SetQualityLevel(qualityID);
        infoOption.Quality = qualityID;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        infoOption.FullScreen = isFullScreen;
    }


    
}
