using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneTransition
{
    public class SceneManager : MonoBehaviour
    {
        private static SceneManager sceneManager;

        public static int currentSceneIndex = 0;
        
        public void Awake()
        {
            if (sceneManager == null)
            {
                sceneManager = this;
                DontDestroyOnLoad(gameObject);
            }
            
            if (sceneManager != this)
            {
                Destroy(gameObject);
            }
        }

        private static List<SceneNames> scenes = new List<SceneNames>
        {
            SceneNames.SCENE0,
            SceneNames.TRANSITION,
            SceneNames.SCENE1,
            SceneNames.TRANSITION,
            SceneNames.SCENE2
        };

        public static Dictionary<SceneNames, String> sceneMappings = new Dictionary<SceneNames, string>
        {
            {SceneNames.SCENE0, "Scene0"},
            {SceneNames.SCENE1, "Scene1"},
            {SceneNames.SCENE2, "Scene2"},
            {SceneNames.TRANSITION, "Transition"},
        };

        public static void LoadNextScene()
        {
            currentSceneIndex++;
            string sceneName = sceneMappings[scenes[currentSceneIndex]];
            Debug.Log("Loading Scene: " + sceneName);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        
    }
}
