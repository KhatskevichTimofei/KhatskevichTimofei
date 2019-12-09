using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void AddAudio(Transform target, string clip, bool spatialBlend = true, bool one = true)
    {
        if (!(!Main.isAudioCurrentFrame || !one))
            return;
        AudioSource audio = target.gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clip);
        audio.Play();
        if (spatialBlend)
        {
            audio.spatialBlend = 1;
            audio.rolloffMode = AudioRolloffMode.Linear;
        }
        Destroy(audio, audio.clip.length);
        if (one)
            Main.isAudioCurrentFrame = true;
    }

    public static void AddAudio(Vector3 target, string clip, bool spantialBlend = true, bool one = true)
    {
        if (!(!Main.isAudioCurrentFrame || !one))
            return;
        GameObject gameObject = Instantiate(new GameObject(clip));
        gameObject.transform.position = target;
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clip);
        audio.Play();
        if (spantialBlend)
        {
            audio.spatialBlend = 1;
            audio.rolloffMode = AudioRolloffMode.Linear;
        }
        Destroy(gameObject, audio.clip.length);
        if (one)
            Main.isAudioCurrentFrame = true;
    }

    public static void AddAudio(Character parent, string clip, bool spatialBlend = true, bool one = true)
    {
        AddAudio(parent.transform.position, parent.curatorAudio + "/" + clip, spatialBlend, one);
    }


}
