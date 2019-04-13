using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level : MonoBehaviour
{
    //config
    [SerializeField] int blocksRemaining;

    //Cached
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        blocksRemaining++;
    }

    public void BlockDestroyed()
    {
        blocksRemaining--;

        if (blocksRemaining == 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

}
