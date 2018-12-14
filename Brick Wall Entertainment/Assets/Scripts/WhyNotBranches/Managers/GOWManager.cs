using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickWallEntertainment.Managers
{
    public enum GameState
    {
        PAUSE_MENU,
        START_MENU,
        LEVEL_1,
        BOSS_LEVEL,
        GAME_OVER,
        GAME_RESTART,
        GAME_WIN,
    }

    public class GOWManager : MonoBehaviour
    {
        private static GOWManager gowManager;

        public static GOWManager Instance
        {
            get
            {
                return gowManager;
            }
        }

        [HideInInspector]
        public GameState currentGameState;

        public int numberOfWaves = 3;

        // This should match the number of Transform Children in an element in Spawn Groups.
        public int numberOfEnemiesPerWave = 4;

        public string spawnGroupsTag = "EnemySpawn";

        public Transform[] SpawnGroups;

        // This should match the number of ratios of which to spawn enemies.
        public GameObject[] prefabs;

        public int[] ratios;

        public float delayBeforeSpawn = 1;

        private GameObject[] wavePrefab;

        private GameObject[] wave;

        private int spawnGroup;

        private int currentWave;

        private bool waveStarted;

        private bool foundEnemySpawn;

        private PlayerController kratosController;

        private bossTakeDamage bossController;

        private bool bossStarted;

        void Awake()
        {
            if (gowManager == null)
                gowManager = this;
            else if (gowManager != this)
                Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
            EventManager.GameStateEventBus += OnGameStateChange;
        }

        void Start()
        {
            EventManager.emitGameState(GameState.START_MENU);

            int sumRatios = 0;
            for (int i = 0; i < ratios.Length; i++) sumRatios += ratios[i];

            wavePrefab = new GameObject[numberOfEnemiesPerWave];
            for (int i = 0, j = 0; i < prefabs.Length; i++)
            {
                GameObject prefab = prefabs[i];
                int numberOfInstances = (ratios[i] * numberOfEnemiesPerWave) / sumRatios;
                while (j < wavePrefab.Length && numberOfInstances > 0)
                {
                    wavePrefab[j] = prefab;
                    j++;
                    numberOfInstances--;
                }
            }
            wave = new GameObject[numberOfEnemiesPerWave];

            spawnGroup = 0;
            currentWave = 0;
            waveStarted = false;
            foundEnemySpawn = false;
            bossStarted = false;
        }

        private IEnumerator GameLoop()
        {
            yield return new WaitForSeconds(delayBeforeSpawn);

            if (!foundEnemySpawn)
            {
                FindEnemySpawnPoints();
                GameObject kratosGameObject = GameObject.FindGameObjectWithTag("Kratos");
                kratosController = kratosGameObject.GetComponent<PlayerController>();
                foundEnemySpawn = true;
            }

            yield return SpawnEnemies();

            yield return KilledEnemiesAndAlive();

            if (kratosController.currentHealth <= 0)
            {
                EventManager.emitGameState(GameState.GAME_OVER);
            }
            else if (currentWave < numberOfWaves)
            {
                StartCoroutine(GameLoop());
            }
            else
            {
                GameObject gate = GameObject.FindGameObjectWithTag("Gate");
                Gate gateScript = gate.GetComponent<Gate>();
                gateScript.OpenGate();
            }
        }

        private IEnumerator SpawnEnemies()
        {
            int enemyIndex = 0;
            foreach (Transform child in SpawnGroups[spawnGroup])
            {
                wave[enemyIndex] = SpawnManager.Spawn(wavePrefab[enemyIndex], child.position, Quaternion.identity);
                enemyIndex++;
            }
            spawnGroup = (spawnGroup + 1) % SpawnGroups.Length;
            currentWave++;
            yield return null;
        }

        private IEnumerator KilledEnemiesAndAlive()
        {
            while (!EnemiesDead() && kratosController.currentHealth > 0)
            {
                yield return null;
            }
        }

        private bool EnemiesDead()
        {
            foreach (GameObject enemy in wave)
            {
                if (enemy == null) continue;
                LightEnemyBehaviour lebs = enemy.GetComponent<LightEnemyBehaviour>();
                if (lebs != null && lebs.Anim.GetInteger("HP") > 0) return false;

                RangedBehaviour rb = enemy.GetComponent<RangedBehaviour>();
                if (rb != null && rb.Anim.GetInteger("HP") > 0) return false;
            }
            return true;
        }

        private void FindEnemySpawnPoints()
        {
            GameObject[] gameObjectSpawnGroups = GameObject.FindGameObjectsWithTag("EnemySpawn");
            this.SpawnGroups = new Transform[gameObjectSpawnGroups.Length];
            for (int i = 0; i < gameObjectSpawnGroups.Length; i++)
            {
                SpawnGroups[i] = gameObjectSpawnGroups[i].transform;
            }
        }

        private IEnumerator BossLoop()
        {
            yield return new WaitForSeconds(0.5f);

            if (!foundEnemySpawn)
            {
                GameObject kratosGameObject = GameObject.FindGameObjectWithTag("Kratos");
                kratosController = kratosGameObject.GetComponent<PlayerController>();

                GameObject bossGameObject = GameObject.FindGameObjectWithTag("Boss");
                bossController = bossGameObject.GetComponent<bossTakeDamage>();
                foundEnemySpawn = true;
            }

            while (kratosController.currentHealth > 0 && bossController.health > 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(3.25f);

            if (kratosController.currentHealth <= 0)
            {
                print("DIED");
                EventManager.emitGameState(GameState.GAME_OVER);
            }
            else if (bossController.health <= 0)
            {
                EventManager.emitGameState(GameState.GAME_WIN);
            }
            bossStarted = false;
        }

        private void OnGameStateChange(GameState gameState)
        {
            this.currentGameState = gameState;
            print(Time.timeScale);
            if (gameState == GameState.START_MENU)
            {
                StopAllCoroutines();
                AudioManager.Instance.StopAll();
                spawnGroup = 0;
                currentWave = 0;
                waveStarted = false;
                foundEnemySpawn = false;
                bossStarted = false;
                AudioManager.Instance.Play("MainMenuTheme");
            }
            else if (gameState == GameState.PAUSE_MENU
                || gameState == GameState.GAME_OVER || gameState == GameState.GAME_WIN)
            {
                Time.timeScale = 0;
                AudioManager.Instance.PauseAll();
                AudioManager.Instance.UnPause("MainMenuTheme");
            }
            else if (gameState == GameState.GAME_RESTART)
            {
                print("Restarting");
                StopAllCoroutines();
                spawnGroup = 0;
                currentWave = 0;
                waveStarted = false;
                foundEnemySpawn = false;
                bossStarted = false;
            }
            else if (gameState == GameState.LEVEL_1)
            {
                Time.timeScale = 1;
                AudioManager.Instance.UnPauseAll();
                AudioManager.Instance.Pause("MainMenuTheme");
                AudioManager.Instance.Play("NormalLevelTheme");
                AudioManager.Instance.Play("BirdAmbient");
                if (!waveStarted)
                {
                    waveStarted = true;
                    StartCoroutine(GameLoop());
                }
            }
            else if (gameState == GameState.BOSS_LEVEL)
            {
                StopAllCoroutines();
                Time.timeScale = 1;
                AudioManager.Instance.Stop("NormalLevelTheme");
                AudioManager.Instance.Stop("BirdAmbient");
                AudioManager.Instance.UnPauseAll();
                AudioManager.Instance.Pause("MainMenuTheme");
                AudioManager.Instance.Play("BossBattleTheme");
                if (!bossStarted)
                {
                    print("BOSS STARTED");
                    bossStarted = true;
                    foundEnemySpawn = false;
                    StartCoroutine(BossLoop());
                }
            }
        }
    }
}
