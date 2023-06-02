using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
  
    public static bool GameIsPaused = false;
    
    [SerializeField] public GameObject pauseMenuUI;

    [Header("UI")]
    public Button resumeBtn;
    public Button returnMenuBtn;
    public Button restartBtn;
    public Text text;

    void Start()
    {

        resumeBtn.onClick.AddListener(() => Resume());
        returnMenuBtn.onClick.AddListener(() => ReturnMain());
        restartBtn.onClick.AddListener(() => ReplayGamePressed());
    }

   private void Resume() {
        AudioManager.Instance.PlaySFX("clicked");
        Time.timeScale = 1.0f;
        pauseMenuUI.SetActive(false);
        AudioManager.Instance.musicSource.UnPause();
        GameIsPaused = false;
   }
   public void Pause(){
        AudioManager.Instance.PlaySFX("clicked");
        AudioManager.Instance.musicSource.Pause();
        Time.timeScale = 0.0f;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
   }
   private void ReturnMain() {
        AudioManager.Instance.PlaySFX("clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("main");
        GameIsPaused = false;
    }

    private void ReplayGamePressed()
    {
        AudioManager.Instance.PlaySFX("clicked");
        Time.timeScale = 1f;
        AudioManager.Instance.musicSource.Stop();
        SceneManager.LoadScene("gameplay");
        GameIsPaused = false;
    }
}
