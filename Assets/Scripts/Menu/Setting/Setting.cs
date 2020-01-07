using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : UnityEngine.MonoBehaviour
{
    public static float music = 1;
    public static float sound = 1;
    public static float voice = 1;
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
