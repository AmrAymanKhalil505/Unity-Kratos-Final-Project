using UnityEngine;
using UnityEngine.UI;

using BrickWallEntertainment.Managers;

public class UIManagerProxy : MonoBehaviour
{

    public GameObject deathMenuCanvas;

    public GameObject pauseMenuCanvas;

    void Update()
    {
        if (GOWManager.Instance.currentGameState == GameState.GAME_OVER)
        {
            deathMenuCanvas.SetActive(true);
            return;
        }
        if (GOWManager.Instance.currentGameState != GameState.START_MENU
            && GOWManager.Instance.currentGameState != GameState.GAME_OVER && Input.GetKeyDown(KeyCode.Escape))
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
        UIManager.Instance.StartGame();
    }


    public void ResumeGame()
    {
        UIManager.Instance.ResumeGame();
    }

    public void QuitGame()
    {
        UIManager.Instance.QuitGame();
    }
}
