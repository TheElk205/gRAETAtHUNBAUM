﻿using SceneTransition;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void NextScene()
    {
        FindObjectOfType<AudioManager>().Play("Level1Start");
        SceneManager.sceneManager.LoadScenePositionPlayer(new Vector2(-52.8f, 60.7f));
    }
}