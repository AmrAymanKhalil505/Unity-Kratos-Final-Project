using UnityEngine;
using UnityEngine.UI;

using BrickWallEntertainment.Managers;

public class UIManagerProxy : MonoBehaviour
{

    public GameObject pauseMenuCanvas;

    void Update()
    {
        if (GOWManager.Instance.currentGameState != GameState.START_MENU && Input.GetKeyDown(KeyCode.Escape))
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
