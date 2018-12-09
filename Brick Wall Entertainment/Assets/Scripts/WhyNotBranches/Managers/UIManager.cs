using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickWallEntertainment.Managers
{
    public class UIManager : MonoBehaviour
    {

        private static UIManager uiManager;

        public static UIManager Instance
        {
            get
            {
                return uiManager;
            }
        }

        [HideInInspector]
        public GameState previousGameState;

        void Awake()
        {
            if (uiManager == null)
                uiManager = this;
            else if (uiManager != this)
                Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
        }


        public void StartGame()
        {
            SceneManager.LoadScene("Scenes/WhyNotBranches/GameScene");
            EventManager.emitGameState(GameState.LEVEL_1);
        }

        public void ResumeGame()
        {
            EventManager.emitGameState(this.previousGameState);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}