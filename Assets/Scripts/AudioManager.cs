using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void AddAudio(Transform target, string clip)
    {
        AudioSource audio = target.gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clip);
        audio.Play();
        Destroy(audio, audio.clip.length);
    }

    public static void AddAudio(Vector3 target, string clip)
    {
        GameObject gameObject = Instantiate(new GameObject(clip));
        gameObject.transform.position = target;
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clip);
        audio.Play();
        Destroy(gameObject, audio.clip.length);
    }



}
