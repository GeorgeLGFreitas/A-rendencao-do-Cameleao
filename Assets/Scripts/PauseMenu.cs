using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject uiSettings;
    public string menuSceneName = "MainMenu";

    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        if (ui.activeSelf)
        {
            Cursor.visible = true;
            Time.timeScale = 0f;
            uiSettings.SetActive(false);

        }
        else
        {
            Cursor.visible = false;
            Time.timeScale = 1f;
            
        }
    }
    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Retry");
    }
    public void Menu()
    {
        Toggle();
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Settings()
    {
        ToggleSettings();
    }
    public void ToggleSettings()
    {
        ui.SetActive(false);
        uiSettings.SetActive(true);
    }
    public void Back()
    {
        ui.SetActive(true);
        uiSettings.SetActive(false);
    }
}
