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

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        resumeBtn.onClick.AddListener(() => Resume());
        returnMenuBtn.onClick.AddListener(() => ReturnMain());
        restartBtn.onClick.AddListener(() => gameManager.ReplayGamePressed());
    }

    public void PauseGame()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else 
        {
            Pause();
        }
    }

   public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
   }
   public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
   }
   public void ReturnMain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   public void Setting(int sceneID) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
   }
}
