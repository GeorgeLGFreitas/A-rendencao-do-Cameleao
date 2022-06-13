using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    CheckpointController loader;
    public GameObject uiMenu, uiSettings;

    private void Start()
    {
        Cursor.visible = true;
        loader = FindObjectOfType<CheckpointController>();
        Destroy(loader.gameObject);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
    public void Settings()
    {
        uiMenu.SetActive(false);
        uiSettings.SetActive(true);
    }
    public void Back()
    {
        uiMenu.SetActive(true);
        uiSettings.SetActive(false);
    }
}

