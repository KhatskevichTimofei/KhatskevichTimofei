using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void AddAudio(Transform target, string clip, bool spatialBlend = true)
    {
        AudioSource audio = target.gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clip);
        audio.Play();
        if (spatialBlend)
        {
            audio.spatialBlend = 1;
            audio.rolloffMode = AudioRolloffMode.Linear;
        }
        Destroy(audio, audio.clip.length);
    }

    public static void AddAudio(Vector3 target, string clip, bool spantialBlend = true)
    {
        GameObject gameObject = Instantiate(new GameObject(clip));
        gameObject.transform.position = target;
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clip);
        audio.Play();
        if (spantialBlend)
        {
            audio.spatialBlend = 1;
        }
        Destroy(gameObject, audio.clip.length);
    }



}
