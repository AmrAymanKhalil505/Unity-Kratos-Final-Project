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
            Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
            deathMenuCanvas.SetActive(true);
            return;
        }
        if (GOWManager.Instance.currentGameState == GameState.GAME_WIN)
        {
            Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
            winMenuCanvas.SetActive(true);
            return;
        }
        if (GOWManager.Instance.currentGameState != GameState.START_MENU
            && GOWManager.Instance.currentGameState != GameState.GAME_OVER
                && GOWManager.Instance.currentGameState != GameState.GAME_WIN && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GOWManager.Instance.currentGameState != GameState.PAUSE_MENU)
            {
                Cursor.lockState = CursorLockMode.None;
			    Cursor.visible = true;
                UIManager.Instance.previousGameState = GOWManager.Instance.currentGameState;
                EventManager.emitGameState(GameState.PAUSE_MENU);
                pauseMenuCanvas.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
			    Cursor.visible = false;
                pauseMenuCanvas.SetActive(false);
                EventManager.emitGameState(UIManager.Instance.previousGameState);
            }
        }
    }

    public void StartGame()
    {
        UIManager.Instance.StartGame();
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

    public void MusicVolumeChange(float volume)
    {
        UIManager.Instance.MusicVolumeChange(volume);
    }

    public void SpeechVolumeChange(float volume)
    {
        UIManager.Instance.SpeechVolumeChange(volume);
    }

    public void EffectsVolumeChange(float volume)
    {
        UIManager.Instance.EffectsVolumeChange(volume);
    }
}
