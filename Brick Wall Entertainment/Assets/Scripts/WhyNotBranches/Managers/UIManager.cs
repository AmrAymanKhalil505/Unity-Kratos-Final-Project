using UnityEngine;
using UnityEngine.Audio;
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

        public AudioMixer audioMixer;

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
            //SceneManager.LoadScene("Scenes/WhyNotBranches/GameScene");
            SceneManager.LoadScene("Integration/Normal Level/Normal Level");
            EventManager.emitGameState(GameState.LEVEL_1);
        }

        public void RestartGame()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
            if (sceneName.Equals("Normal Level"))
            {
                EventManager.emitGameState(GameState.LEVEL_1);
            }
            else if (sceneName.Equals("Boss Level"))
            {
                EventManager.emitGameState(GameState.BOSS_LEVEL);
            }
        }

        public void ResumeGame()
        {
            EventManager.emitGameState(this.previousGameState);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void MusicVolumeChange(float volume)
        {
            audioMixer.SetFloat("musicVolume", volume);
        }

        public void SpeechVolumeChange(float volume)
        {
            audioMixer.SetFloat("speechVolume", volume);
        }

        public void EffectsVolumeChange(float volume)
        {
            audioMixer.SetFloat("effectsVolume", volume);
        }
    }
}