using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public Slider Music;
    public Slider SFX;

    // Start is called before the first frame update
    void Start()
    {
        Music.value = AudioManager.instance.GetVolume("MenuMusic");
        SFX.value = AudioManager.instance.GetVolume("Fly"); //0.238

    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.instance.Regulate("MenuMusic", Music.value);
        AudioManager.instance.Regulate("GameMusic", Music.value);
        if (SFX.value < 0.238f)
        {
            AudioManager.instance.Regulate("Fly", SFX.value);
        }else if (SFX.value < 0.100f)
        {
            AudioManager.instance.Regulate("Disparo", SFX.value);
        }else if (SFX.value < 0.200f)
        {
            AudioManager.instance.Regulate("PowerUp", SFX.value);
        }
        

    }
}
