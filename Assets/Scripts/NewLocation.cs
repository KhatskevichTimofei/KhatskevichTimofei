using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLocation : MonoBehaviour
{
    public void OnMouseDown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("living room");
    }
}
