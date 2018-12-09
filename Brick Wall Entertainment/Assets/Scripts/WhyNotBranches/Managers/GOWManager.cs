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
                while (j < wavePrefab.Length)
                {
                    wavePrefab[j] = prefab;
                    j++;
                }
            }
            wave = new GameObject[numberOfEnemiesPerWave];

            spawnGroup = 0;
            currentWave = 0;
            waveStarted = false;
            foundEnemySpawn = false;
        }

        void Update() { }

        private IEnumerator GameLoop()
        {
            yield return new WaitForSeconds(delayBeforeSpawn);

            if (!foundEnemySpawn)
            {
                FindEnemySpawnPoints();
                foundEnemySpawn = true;
            }

            yield return SpawnEnemies();

            yield return KilledEnemiesAndAlive();

            // if (kratosDead)
            // {
                // DISPLAY YOU DIED UI.
                // spawnGroup = 0;
                // currentWave = 0;
                // waveStarted = false;
                // return;
            // } else

            if (currentWave < numberOfWaves)
            {
                StartCoroutine(GameLoop());
            }
            else
            {
                // OPEN GATE
                
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
            while (!EnemiesDead()) // && KratosAlive
            {
                yield return null;
            }
        }

        // CHANGE THIS WITH `EnemyBehavior` SCRIPT.
        private bool EnemiesDead()
        {
            foreach (GameObject enemy in wave)
            {
                Killable killableScrpt = enemy.GetComponent<Killable>();
                if (!killableScrpt.isDead) return false;
            }
            return true;
        }

        private void FindEnemySpawnPoints()
        {
            GameObject[] gameObjectSpawnGroups = GameObject.FindGameObjectsWithTag("EnemySpawn");
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
            }
            else if (gameState == GameState.LEVEL_1)
            {
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
