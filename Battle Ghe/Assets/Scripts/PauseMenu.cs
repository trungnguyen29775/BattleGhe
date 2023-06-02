using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
  
    public static bool GameIsPaused = false;
    
    [SerializeField] GameObject pauseMenuUI;
    
    public GameManager gameManager;

    [Header("UI")]
    public Button resumeBtn;
    public Button returnMenuBtn;
    public Button restartBtn;
    public Button settingBtn;
    public Text text;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        resumeBtn.onClick.AddListener(() => Resume());
        returnMenuBtn.onClick.AddListener(() => ReturnMain());
        restartBtn.onClick.AddListener(() => ReplayGamePressed());
    }


   private void Resume() {
        Time.timeScale = 1.0f;
        pauseMenuUI.SetActive(false);
        AudioManager.Instance.musicSource.UnPause();
        GameIsPaused = false;
   }
   public void Pause(){
        AudioManager.Instance.musicSource.Pause();
        Time.timeScale = 0.0f;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
   }
   private void ReturnMain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   private void Setting(int sceneID) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
   }

    private void ReplayGamePressed()
    {
        AudioManager.Instance.musicSource.Stop();
        SceneManager.LoadScene("gameplay");
        GameIsPaused = false;
    }
}
