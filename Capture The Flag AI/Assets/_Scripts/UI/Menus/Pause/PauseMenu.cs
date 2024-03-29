using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;

    private void Awake()
    {
        menuObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseScreenState(PauseManager.Instance.IsPaused);
        }
    }
    private void ChangePauseScreenState(bool pauseState)
    {
        if (pauseState)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        menuObject.SetActive(true);
        PauseManager.Instance.SetPauseState(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        menuObject.SetActive(false);
        PauseManager.Instance.SetPauseState(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoToMainMenu()
    {
        PauseManager.Instance.SetPauseState(false);
        SceneManagerScript.Instance.LoadScene(Scenes.MainMenu);
        GameManager.Instance.EndMatch();
    }
    
    public void RestartLevel()
    {
        PauseManager.Instance.SetPauseState(false);
        SceneManagerScript.Instance.ReloadScene();
        GameManager.Instance.EndMatch();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
