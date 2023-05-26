using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] public bool FullScreen = true;
    [SerializeField] public float Volume = 0;
    [SerializeField] public int Resolution;
    [SerializeField] public int Quality = 2;
    [SerializeField] public OptionMenu menu;
    
    // Start is called before the first frame update
    void Start()
    {
       DontDestroyOnLoad(gameObject);
        FullScreen = true;
    }

    public void SaveOption(bool Full, float Vol, int Res,int Qual)
    {
        FullScreen = Full;
        Volume = Vol;
        Resolution = Res;
        Quality = Qual;
    }

    public void SetSetting(bool Full, float Vol, int Res, int Qual)
    {
        menu.SetResolution(Resolution);
        menu.SetVolume(Volume);
        menu.SetQuality(Quality);
        menu.SetFullScreen(FullScreen);

    }


}
