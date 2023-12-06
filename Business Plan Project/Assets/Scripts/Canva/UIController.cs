using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void OpenSettings()
    {
        animator.SetBool(AnimationString.OnSetting, true);
    }
    public void CloseSettings() 
    {
        animator.SetBool(AnimationString.OnSetting, false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
