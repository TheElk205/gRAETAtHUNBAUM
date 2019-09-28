using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTransition
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager sceneManager { private set; get; }

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

        public void Start()
        {
            FindObjectOfType<AudioManager>().Play("StartScreen");
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

        public static Dictionary<SceneNames, String> sceneBackgroundMusic = new Dictionary<SceneNames, string>
        {
            {SceneNames.SCENE0, "Level1Start"},
            {SceneNames.SCENE1, "Level2"},
            {SceneNames.SCENE2, "Level3"},
            {SceneNames.TRANSITION, "Shop"},
        };

        public void LoadScenePositionPlayer(Vector2 position)
        {
            currentSceneIndex++;
            string sceneName = sceneMappings[scenes[currentSceneIndex]];
            StartCoroutine(LoadLevelWaitAndSetPosition(sceneName, position));

            string music = sceneBackgroundMusic[scenes[currentSceneIndex]];
            FindObjectOfType<AudioManager>().Play(music);
        }
        
        public static void LoadNextScene()
        {
            
        }
        
        private IEnumerator LoadLevelWaitAndSetPosition (String sceneName, Vector2 position)
        {
            
            Debug.Log("Loading Scene: " + sceneName);
            var asyncLoadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone){
                print("Loading the Scene"); 
                yield return null;
            }

            GameObject.FindWithTag("Player").transform.position = new Vector3(position.x, position.y, -40);
        }
        
    }
}
