using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlsClicked()
    {
        controlMenu.SetActive(true);
    }

    public void CloseClicked()
    {
        controlMenu.SetActive(false);
    }

    public void QuitClicked()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #elif UNITY_STANDALONE
        Application.Quit();
        #endif
        
    }
}
