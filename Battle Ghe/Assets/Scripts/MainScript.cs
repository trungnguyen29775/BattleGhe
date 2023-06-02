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

    [SerializeField] private AudioSource theme;


    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.GameIsPaused = true;
        theme.Play();
        startButton.onClick.AddListener(() => StartGame());
        optionButton.onClick.AddListener(() => OptionMenu());
        quitButton.onClick.AddListener(() => Application.Quit());
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("gameplay");
    }

    private void OptionMenu()
    {
        //AudioManager.Instance.musicSource.Pause();
        //Time.timeScale = 0.0f;
        //pauseMenuUI.SetActive(true);
        //GameIsPaused = true;
    }


}
