using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Boats")]
    public GameObject[] boats;
    public List<TileScript> allTileScript;
    public EnemyScript enemyScript;
    private BoatScript boatScript;
    private GameObject pressedTile;
    private List<int[]> enemyBoats;
    private int boatIndex = 0;

    [Header("HUD")]
    public Button nextBoatButton;
    public Button rotateBoatButton;

    [Header("Texts")]
    public TextMeshProUGUI headText;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI enemyText;

    [Header("Objects")]
    public GameObject missilePrefab;
    public GameObject enemyMissilePrefab;
    public GameObject deloyDeck;
    public GameObject invisibleSquare;
    public GameObject firePrefab;

    [Header("GameOver")]
    public Light componentlight;
    public TextMeshProUGUI gameOverUI;
    public TextMeshProUGUI gameOverText;
    public Button replayGameButton;
    public Button quitGameButton;

    [Header("PauseMenu")]
    public Button pauseGameButton;
    public PauseMenu pauseMenu;

    private bool setupComplete = false;
    private bool playerTurn = true;

    private int enemyBoatCounter = 5;
    private int playerBoatCounter = 5;

    private List<GameObject> playerFires = new List<GameObject>();
    private List<GameObject> enemyFires = new List<GameObject>();

    //Music and Sound Effect
    private AudioManager musicManager;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        if (!PauseMenu.GameIsPaused)
        {
            boatScript = boats[boatIndex].GetComponent<BoatScript>();
            nextBoatButton.onClick.AddListener(() => NextBoat());
            rotateBoatButton.onClick.AddListener(() => RotateBoat());
            replayGameButton.onClick.AddListener(() => ReplayGamePressed());
            quitGameButton.onClick.AddListener(() => QuitGamePressed());
            pauseGameButton.onClick.AddListener(() => PauseGamePressed());
        }
        enemyBoats = enemyScript.EnemyDeploy();
    }

    

    public void TilePressed(GameObject tile)
    {
        if (setupComplete && playerTurn)
        {
            Vector3 tilePos = tile.transform.position;
            tilePos.y = 5;
            playerTurn = false;
            Instantiate(missilePrefab, tilePos, missilePrefab.transform.rotation);
        } else if (!setupComplete)
        {
            PlaceBoat(tile);
            boatScript.SetPressedTile(tile);
        }
    }

    private void PlaceBoat(GameObject tile)
    {
        boatScript = boats[boatIndex].GetComponent<BoatScript>();
        boatScript.ClearTileList();
        Vector3 newVec = boatScript.GetOffsetVec(tile.transform.position);
        boats[boatIndex].transform.localPosition = newVec;
    }

    private void NextBoat()
    {
        if(!boatScript.OnGameBoard())
        {
            boatScript.FlashColor(Color.red);
            Debug.Log("Boat Index: " + boatIndex);
        } 
        else
        {
            if (boatIndex <= boats.Length - 2)
            {
                
                boatIndex++;
                boatScript = boats[boatIndex].GetComponent<BoatScript>();
                boatScript.FlashColor(Color.green);
            }
            else
            {
                rotateBoatButton.gameObject.SetActive(false);
                nextBoatButton.gameObject.SetActive(false);
                deloyDeck.gameObject.SetActive(false);
                invisibleSquare.gameObject.SetActive(false);
                headText.text = "Your turn.\nFind enemy boats location!!";
                setupComplete = true;

                for (int i = 0; i < boats.Length; i++)
                {
                    boats[i].gameObject.SetActive(false);
                }
            }
        }
        
    }

    private void RotateBoat()
    {
        boatScript.RotateBoat();
    }

    private void PauseGamePressed()
    {
        Time.timeScale = 0.0f;
        pauseMenu.Pause();
    }

    private void setBoatPressedTile(GameObject tile)
    {
        pressedTile = tile;
    } 

    public void CheckHit(GameObject tile)
    {
        int tileNum = Int32.Parse(Regex.Match(tile.name, @"\d+").Value);
        int hitCount = 0;
        foreach (int[] tileNumArray in enemyBoats)
        {
            if (tileNumArray.Contains(tileNum))
            {
                for (int i = 0; i < tileNumArray.Length; i++)
                {
                    if (tileNumArray[i] == tileNum)
                    {
                        tileNumArray[i] = -5;
                        hitCount++;
                    }
                    else if (tileNumArray[i] == -5)
                    {
                        hitCount++;
                    }
                }
                if (hitCount == tileNumArray.Length)
                {
                    enemyBoatCounter--;
                    headText.text = "SUNKKK!!";
                    //  Sunk
                    enemyFires.Add(Instantiate(firePrefab, tile.transform.position + new Vector3(0f, 0.5f, 0.3f), Quaternion.Euler(90f, 0f, 0)));
                    //  Color
                    tile.GetComponent<TileScript>().SetTileColor(1, new Color32(68, 0, 0, 255));
                    tile.GetComponent<TileScript>().SwitchColors(1);
                }
                else
                {
                    // Hit
                    // Color
                    enemyFires.Add(Instantiate(firePrefab, tile.transform.position + new Vector3(0f, 0.5f,0.3f), Quaternion.Euler(90f, 0f, 0)));
                    tile.GetComponent<TileScript>().SetTileColor(1, new Color32(255, 0, 0, 255));
                    tile.GetComponent<TileScript>().SwitchColors(1);
                    headText.text = "HIT!!";
                }
                break;
            }
        }
        if (hitCount == 0)
        {
            // Missed
            // Color
            tile.GetComponent<TileScript>().SetTileColor(1, new Color32(38, 57, 76, 255));
            tile.GetComponent<TileScript>().SwitchColors(1);
            headText.text = "You Missed!!";
        }
        Invoke("EndPlayerTurn",1.0f);
    }

    public void EnemyHitPlayer(Vector3 tile, int tileNum, GameObject hitObj)
    {

        enemyScript.MissileHit(tileNum);
        tile.y += 0.5f;
        tile.z += 0.3f;
        playerFires.Add(Instantiate(firePrefab, tile, Quaternion.Euler(90.0f, 0f, 0f)));
        if(hitObj.GetComponent<BoatScript>().HitCheckSank())
        {
            playerBoatCounter--;
            //playerText.text = playerBoatCounter.ToString();
            enemyScript.SunkPlayer();
        }
        Invoke("EndEnemyTurn", 2.0f);
    }

    public void EndPlayerTurn()
    {
        for (int i = 0; i < boats.Length;  i++)
        {
            boats[i].SetActive(true);
        }

        foreach (GameObject fire in playerFires) fire.SetActive(true);
        foreach (GameObject fire in enemyFires) fire.SetActive(false);
        //enemyText.text = enemyBoatCounter.ToString();
        headText.text = "Enemy's turn";
        enemyScript.EnemyTurn();
        ColorAllTiles(0);
        if (playerBoatCounter < 1) GameOver("You are defeated\nYou Lose"); 

    }

    public void EndEnemyTurn()
    {
        for (int i = 0; i < boats.Length; i++)
        {
            boats[i].SetActive(false);
        }

        foreach (GameObject fire in playerFires) fire.SetActive(false);
        foreach (GameObject fire in enemyFires) fire.SetActive(true);
        //playerText.text = playerBoatCounter.ToString();
        headText.text = "Your's turn";
        playerTurn = true;
        ColorAllTiles(1);
        if (enemyBoatCounter < 1) GameOver("Enemy are defeated\nYou Win");

    }

    private void ColorAllTiles(int colorIndex)
    {
        foreach (TileScript tileScript in allTileScript)
        {
            tileScript.SwitchColors(colorIndex);
        }
    }

    void GameOver(string winner)
    {

        componentlight.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true) ;
        gameOverUI.gameObject.SetActive(true);
        replayGameButton.gameObject.SetActive(true) ;
        quitGameButton.gameObject.SetActive(true) ;
        gameOverText.text = winner;
        playerTurn = false;
    }


    void ReplayGamePressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitGamePressed()
    {
        Application.Quit();
    }
}
