using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScenesScript : MonoBehaviour
{
    string currentSceneName;
    public bool enableContinueButton;

    private void Start()
    {
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if(currentSceneName != "Main Menu")
        {
            PlayerPrefs.SetString("ContinueScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        
        if (currentSceneName == "Main Menu" && PlayerPrefs.GetString("EnableContinueButton").Equals("Enable"))
        {
            GameObject.FindGameObjectWithTag("TestiZaidima").GetComponent<Button>().interactable = true;
        }
        else if (currentSceneName == "Main Menu" && PlayerPrefs.GetString("EnableContinueButton").Equals("Disable"))
        {
            GameObject.FindGameObjectWithTag("TestiZaidima").GetComponent<Button>().interactable = false;
        }
    }
    public void ChangeScenes(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetString("EnableContinueButton", "Enable");
    }
    public void ContinueLastGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerPrefs.GetString("ContinueScene"));
    }
    public void StartNewGame()
    {
        PlayerPrefs.SetString("ContinueScene", "Level1");
        PlayerPrefs.SetString("EnableContinueButton", "Enable");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void NextLevel()
    {
        GameManager.instance.CloseResultsMenu();
    }
    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
