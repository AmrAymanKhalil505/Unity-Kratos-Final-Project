using UnityEngine;
using UnityEngine.UI;

using BrickWallEntertainment.Managers;

public class UIManagerProxy : MonoBehaviour
{

    public GameObject deathMenuCanvas;

    public GameObject pauseMenuCanvas;

    public GameObject winMenuCanvas;

    void Update()
    {
        if (GOWManager.Instance.currentGameState == GameState.GAME_OVER)
        {
            deathMenuCanvas.SetActive(true);
            return;
        }
        if (GOWManager.Instance.currentGameState == GameState.GAME_WIN)
        {
            winMenuCanvas.SetActive(true);
            return;
        }
        if (GOWManager.Instance.currentGameState != GameState.START_MENU
            && GOWManager.Instance.currentGameState != GameState.GAME_OVER
                && GOWManager.Instance.currentGameState != GameState.GAME_WIN && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GOWManager.Instance.currentGameState != GameState.PAUSE_MENU)
            {
                UIManager.Instance.previousGameState = GOWManager.Instance.currentGameState;
                EventManager.emitGameState(GameState.PAUSE_MENU);
                pauseMenuCanvas.SetActive(true);
            }
            else
            {
                pauseMenuCanvas.SetActive(false);
                EventManager.emitGameState(UIManager.Instance.previousGameState);
            }
        }
    }

    public void RestartGame()
    {
        EventManager.emitGameState(GameState.GAME_RESTART);
        // UIManager.Instance.StartGame();
        UIManager.Instance.RestartGame();
    }


    public void ResumeGame()
    {
        UIManager.Instance.ResumeGame();
    }

    public void QuitGame()
    {
        UIManager.Instance.QuitGame();
    }

    public void QuitToMainMenu()
    {
        UIManager.Instance.QuitToMainMenu();
    }
}
