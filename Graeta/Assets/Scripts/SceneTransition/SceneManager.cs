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
            {SceneNames.SCENE2, "Level2"},
            {SceneNames.TRANSITION, "Shop"},
        };

        public void LoadScenePositionPlayer(Vector2 position)
        {
            string sceneName = sceneMappings[scenes[currentSceneIndex]];
            string music = sceneBackgroundMusic[scenes[currentSceneIndex]];

            currentSceneIndex++;
            
            StartCoroutine(LoadLevelWaitAndSetPosition(sceneName, position));

            FindObjectOfType<AudioManager>().Play(music);
            
        }

        private IEnumerator LoadLevelWaitAndSetPosition (String sceneName, Vector2 position)
        {
            
            Debug.Log("Loading Scene: " + sceneName);
            var asyncLoadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone){
                print("Loading the Scene"); 
                yield return null;
            }

            yield return null;
            GameObject.FindWithTag("Player").transform.position = new Vector3(position.x, position.y, -40);
        }

        public void Restart()
        {
            currentSceneIndex = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneMappings[SceneNames.TRANSITION], LoadSceneMode.Single);

            Destroy(GameObject.FindWithTag("Player"));
        }
        
    }
}
