using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
        optionButton.onClick.AddListener(() => OptionMenu());
        quitButton.onClick.AddListener(() => Application.Quit());
    }

    private void StartGame()
    {
        AudioManager.Instance.PlaySFX("clicked");
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("gameplay");
    }

    private void OptionMenu()
    {
        AudioManager.Instance.PlaySFX("clicked");
        //AudioManager.Instance.musicSource.Pause();
        //Time.timeScale = 0.0f;
        //pauseMenuUI.SetActive(true);
        //GameIsPaused = true;
    }


}
