using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void Setup(){
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void RestartButton(){
        Time.timeScale = 1;
        PlayerPrefs.DeleteKey("FirstRun");
        StartCoroutine(LoadAsynchronously("terrain"));
        
        

    }
    public void ExitButton(){
        Time.timeScale = 1;
        StartCoroutine(LoadAsynchronously("MenuScene"));
        
        
    }
    IEnumerator LoadAsynchronously(string sceneName){ // scene name is just the name of the current scene being loaded
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
			while (!operation.isDone){
				if (operation.progress >= 0.9f){
					operation.allowSceneActivation = true;
				yield return null;
			}
		}
    }
}
