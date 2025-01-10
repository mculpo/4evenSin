using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadingHandle : MonoBehaviour
{
    public void LoadScene(string nameScene)
    {
        LoadSceneManager.instance.LoadScene(nameScene);
    }
}
