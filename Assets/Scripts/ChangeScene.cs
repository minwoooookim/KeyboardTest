using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneToMain()
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void ChangeSceneToBeforeNK()
    {
        SceneManager.LoadScene("Before 1");
    }
    public void ChangeSceneToBeforeWK()
    {
        SceneManager.LoadScene("Before 2");
    }
    public void ChangeSceneToNK()
    {
        SceneManager.LoadScene("Test 1");
    }
    public void ChangeSceneToWK()
    {
        SceneManager.LoadScene("Test 2");
    }
    public void ChangeSceneToResult()
    {
        SceneManager.LoadScene("Result Scene");
    }

    public void ChangeSceneToNKPractice()
    {
        SceneManager.LoadScene("Test 1 Practice");
    }
    public void ChangeSceneToWKPractice()
    {
        SceneManager.LoadScene("Test 2 Practice");
    }
}