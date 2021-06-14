using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneManagerSECRIOUS : MonoBehaviour
{

    [Tooltip("optional")]
    public GameObject loadingScreen;
    [Tooltip("optional")]
    public UnityEvent changingSceneFX;
    [Tooltip("optional")]
    public UnityEvent quitSceneFX;
    [Tooltip("optional")]
    public float delaySeconds;


    public void ChangeSceneWithLoadingScreen(int sceneIndex)
    {
        StartCoroutine(AsyncSceneLoading(sceneIndex));

        IEnumerator AsyncSceneLoading(int index)
        {
          

            //if you have a loading screen;
            if (loadingScreen != null)
                loadingScreen.SetActive(true);

            //start any scene transition effects like fadein/out HERE
            changingSceneFX?.Invoke();

            //if you want a fixed delay 
            yield return new WaitForSeconds(delaySeconds);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }

    public void GameQuit()
    {
        StartCoroutine(QuitRoutine());
        IEnumerator QuitRoutine()
        {
            //start any scene transition effects like fadein/out HERE
            quitSceneFX?.Invoke();
            yield return new WaitForSeconds(delaySeconds);
        }

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }




}


public static class SceneManagerExtension
{
    public static void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadNScene(int n)
    {
        if (ErrorCheckIndex(n))
        {
            SceneManager.LoadScene(n);
        }
        else
        {
            Debug.Log("Invalid index");
        }
    }

    public static void LoadLastScene()
    {
        int highestIndex = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(highestIndex);
    }

    public static void ReloadScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
    public static void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("currentIndex: " + currentIndex);
        int nextIndex = currentIndex + 1;
        Debug.Log("nextIndex: " + nextIndex);
        if (ErrorCheckIndex(nextIndex))
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("Invalid index");
        }
    }

    public static bool ErrorCheckIndex(float index)
    {
        if (index < SceneManager.sceneCountInBuildSettings)
            return true;
        else
            return false;
    }

}

