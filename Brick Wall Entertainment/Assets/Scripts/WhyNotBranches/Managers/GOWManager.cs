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
        GAME_OVER,
        GAME_RESTART,
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

                // YIELD BOSS LEVEL
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

        private void OnGameStateChange(GameState gameState)
        {
            this.currentGameState = gameState;
            if (gameState == GameState.START_MENU || gameState == GameState.PAUSE_MENU)
            {
                AudioManager.Instance.PauseAll();
                // PLAY THEME
                AudioManager.Instance.Play("");
                Time.timeScale = 0;
            }
            else if (gameState == GameState.GAME_RESTART)
            {
                StopAllCoroutines();
                spawnGroup = 0;
                currentWave = 0;
                waveStarted = false;
                foundEnemySpawn = false;
            }
            else if (gameState == GameState.LEVEL_1)
            {
                Time.timeScale = 1;
                AudioManager.Instance.Play("BirdAmbient");
                if (!waveStarted)
                {
                    waveStarted = true;
                    StartCoroutine(GameLoop());
                }
            }
        }
    }
}
