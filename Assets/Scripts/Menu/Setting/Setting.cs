using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public static float music;
    public static float sound;
    public static float voice;
    public bool curator;
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider voiceSlider;
    public Toggle curatorToggle;


    public void SetMusic()
    {
        music = musicSlider.value;
    }

    public void SetSound()
    {
        sound = soundSlider.value;
    }

    public void SetVoice()
    {
        voice = voiceSlider.value;
    }

    public void SetCurator()
    {
        curator = curatorToggle.isOn;
    }
}
