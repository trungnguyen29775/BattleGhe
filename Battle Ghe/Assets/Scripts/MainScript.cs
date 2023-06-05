using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    [Header("UI")]
    public Button startButton;
    public Button optionButton;
    public Button quitButton;
    public GameObject MainUI;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("theme");
        PauseMenu.GameIsPaused = true;
        startButton.onClick.AddListener(() => StartGame());
        optionButton.onClick.AddListener(() => OptionButtonPressed());
        quitButton.onClick.AddListener(() => QuitGame());
    }

    private void OptionButtonPressed()
    {
        AudioManager.Instance.PlaySFX("clicked");
        SceneManager.LoadScene("option");
    }

    private void StartGame()
    {
        AudioManager.Instance.PlaySFX("clicked");
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("gameplay");
    }

    private void QuitGame()
    {
        AudioManager.Instance.PlaySFX("clicked");
        Application.Quit();
    }

}
