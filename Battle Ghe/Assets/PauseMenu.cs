using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public static bool GameIsPaused = false;
   [SerializeField] GameObject pauseMenuUI;
   void update() {
        if(Input.GetKeyDown(KeyCode.P)){
            if(GameIsPaused) {
                Resume();
            }
            else{
                Pause();
            }
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
   public void Menu(int sceneID) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
   }
   public void Setting(int sceneID) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
   }
}
