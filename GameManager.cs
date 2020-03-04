using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    menu,
    inGame,
    gameOver
}


public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;



    private void Awake()
    {
        sharedInstance = this;
    }


    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start") && this.currentGameState != GameState.inGame)
        {
            StartGame();
        }
    }


    public void StartGame()
    {
        // Inicia e Juego
        SetGameState(GameState.inGame);
        PlayerController.sharedInstance.StartGame();
        //Se reinicia la camara
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CamFollow camFollow = camera.GetComponent<CamFollow>();
        camFollow.ResetCameraPosition();

        //Cuando reinicia el Juego
        LevelGenerator.sharedInstance.RemoveAllLevelBlocks();
        LevelGenerator.sharedInstance.GenerateInitialLevelBlock();
    }



    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }


    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }   else if(newGameState == GameState.inGame)
            {
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }   else if(newGameState == GameState.gameOver)
            {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
            
        }
       
        this.currentGameState = newGameState;
    }

 
}


