using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
/* This script controls the enemy wave system of the level. 
 * It can be placed into an empty game object in the level scene. 
 * The object should be fairly close to the first enemy waypoint as
 * the enemy spawns to this objects location and then starts following the waypoints.
 */
{
    public void NameWaves()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            int waveNumber = i + 1;
            waves[i].name = "Wave " + waveNumber;
        }
    }

    public void NameEnemies(Wave wave)
    {
        int j = 1;
        foreach (var enemy in wave.enemies)
        {
            switch (enemy.enemyType)
            {
                case EnemyTypes.Basic:
                    enemy.name = "Basic " + j;
                    break;
                case EnemyTypes.Sturdy:
                    enemy.name = "Sturdy " + j;
                    break;
                case EnemyTypes.Armored:
                    enemy.name = "Armored " + j;
                    break;
                case EnemyTypes.Wizard:
                    enemy.name = "Wizard " + j;
                    break;
                case EnemyTypes.WoodWagon:
                    enemy.name = "Wood wagon " + j;
                    break;
                case EnemyTypes.IronWagon:
                    enemy.name = "Iron wagon " + j;
                    break;
                case EnemyTypes.Exploding:
                    enemy.name = "Exploding " + j;
                    break;
                case EnemyTypes.Fast:
                    enemy.name = "Fast " + j;
                    break;
                case EnemyTypes.Medic:
                    enemy.name = "Medic " + j;

                    break;
            }
            j++;
        }
    }
    [System.Serializable]
    public class Wave
    {
        //[Tooltip("Wave name or number")]
        [HideInInspector]
        public string name;
        //Dont change, just for visual
        //[Multiline]
        //public string header = " \n ------------ \n ------------";
        [HideInInspector]
        public List<EnemyInWave> enemies = new List<EnemyInWave>();
        [Space]
        [Tooltip("Spawn delay between enemies in seconds")]
        [HideInInspector]
        public float delay;

        [Tooltip("Give specific path between 0 and 3, 4 if random"), HideInInspector]
        [Range(0, 4)]
        public int dedicatedPath;
        [HideInInspector]
        public bool isRandomized = true;
        [HideInInspector]
        public bool show = false;
        [HideInInspector]
        public bool showEnemies = false;
        [HideInInspector]
        public bool showLongList = false;
        [HideInInspector]
        public bool showWaveInfo = false;

        [System.Serializable]
        public class EnemyInWave
        {
            [HideInInspector]
            public string name;
            [ContextMenuItem("Add enemy", "AddEnemyToWave")]
            [HideInInspector]
            public EnemyTypes enemyType = EnemyTypes.Basic;
            [HideInInspector]
            public float delay = 0;
            [HideInInspector]
            public bool isEnhanced = false;
            [HideInInspector]
            public bool showInfo = false;
            [HideInInspector]
            public GameObject activeGameObject = null;

            public EnemyInWave(EnemyTypes enemyType)
            {
                this.enemyType = enemyType;
            }
            public EnemyInWave Spawn(Vector3 pos)
            {

                GameObject enemyPrefab = null;
                switch (enemyType)
                {
                    case EnemyTypes.Basic:
                        enemyPrefab = Enemies.Instance.basicEnemyPrefab;
                        break;
                    case EnemyTypes.Sturdy:
                        enemyPrefab = Enemies.Instance.sturdyEnemyPrefab;
                        break;
                    case EnemyTypes.Armored:
                        enemyPrefab = Enemies.Instance.armoredEnemyPrefab;
                        break;
                    case EnemyTypes.Wizard:
                        enemyPrefab = Enemies.Instance.wizardEnemyPrefab;
                        break;
                    case EnemyTypes.WoodWagon:
                        enemyPrefab = Enemies.Instance.woodWagonEnemyPrefab;
                        break;
                    case EnemyTypes.IronWagon:
                        enemyPrefab = Enemies.Instance.ironWagonEnemyPrefab;
                        break;
                    case EnemyTypes.Exploding:
                        enemyPrefab = Enemies.Instance.explodingEnemyPrefab;
                        break;
                    case EnemyTypes.Fast:
                        enemyPrefab = Enemies.Instance.fastEnemyPrefab;
                        break;
                    case EnemyTypes.Medic:
                        enemyPrefab = Enemies.Instance.medicEnemyPrefab;
                        break;
                }
                activeGameObject = Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
                return this;
            }
            public Attacker GetAttacker()
            {
                GameObject enemyPrefab = null;
                switch (enemyType)
                {
                    case EnemyTypes.Basic:
                        enemyPrefab = Enemies.Instance.basicEnemyPrefab;
                        break;
                    case EnemyTypes.Sturdy:
                        enemyPrefab = Enemies.Instance.sturdyEnemyPrefab;
                        break;
                    case EnemyTypes.Armored:
                        enemyPrefab = Enemies.Instance.armoredEnemyPrefab;
                        break;
                    case EnemyTypes.Wizard:
                        enemyPrefab = Enemies.Instance.wizardEnemyPrefab;
                        break;
                    case EnemyTypes.WoodWagon:
                        enemyPrefab = Enemies.Instance.woodWagonEnemyPrefab;
                        break;
                    case EnemyTypes.IronWagon:
                        enemyPrefab = Enemies.Instance.ironWagonEnemyPrefab;
                        break;
                    case EnemyTypes.Exploding:
                        enemyPrefab = Enemies.Instance.explodingEnemyPrefab;
                        break;
                    case EnemyTypes.Fast:
                        enemyPrefab = Enemies.Instance.fastEnemyPrefab;
                        break;
                    case EnemyTypes.Medic:
                        enemyPrefab = Enemies.Instance.medicEnemyPrefab;
                        break;
                }
                Debug.Log("EnemyPrefab:" + enemyPrefab);
                return enemyPrefab.GetComponent<Attacker>();
            }
        }
    }

    #region Variables
    public static Spawner instance;

    public enum State { SPAWNING, WAITING, EMPTY }; // SPAWNING = enemies are being spawned, WAITING = enemies still alive, EMPTY = clear from enemies (at the beginning or after clearing a wave)

    public State state = State.EMPTY; // Declaring the state to be EMPTY as the level opens.
    [HideInInspector]
    public bool showDefaults = true;
    [HideInInspector]
    public bool showWaves = false;
    [Header("Amount of waves"), HideInInspector]
    public List<Wave> waves = new List<Wave>();

    [Header("Possible paths")]
    public GameObject[] paths;
    [Space]
    public int nextWave = 0; // Zero at first, because when pressing start, it increments by one. Has to be public since it's needed for the UI.
    private float searchTimer = 2f; // Timer keeps track the wave clearing time / searchTimer does the enemy tag search from the hierarchy only once every second, not every frame (to make it less heavy for the system)
                                    //private float timer; not used atm

    public GameHandler gameHandler;

    public Image leaderGuardianHealth;

    [HideInInspector]
    public GameObject[] spawnedEnemies = new GameObject[30];

    public bool enemiesDead;

    // Using this to pause spawning... during timestorm etc.
    [HideInInspector]
    public bool paused = false;

    //private GameObject shuffledGO; // This is needed for enemy spawn order shuffling

    // Wave clear sound alternatives
    private string[] waveClearSounds = { "Wave_clear", "Wave_clear_var1", "Wave_clear_var2" };

    #endregion

    #region Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Update()
    {
        // THESE TIMERS MIGHT BE USED LATER ON TO AWARD PLAYER FOR FAST CLEARING
        //timer += Time.deltaTime; //Timer is added just in case we want to award the player for how fast they cleared the wave
        //timeText.text = timer.ToString("F1"); // Giving the time text object the time as a string

        if (state == State.WAITING)
        // While the spawner is in waiting state, it calls for EnemiesAlive function. If there are still enemies it doesn't execute the rest of the update function.
        // However, if no enemies were found (enemiesAlive function returned false), the state is changed into EMPTY
        {
            // WAVE CLEARED!
            if (!EnemiesAlive())
            {
                //Debug.Log("No enemies found.");
                GameStateManager.Instance.currentGameState = GameStateManager.GameStates.Inactive;
                // Play random wave clear sound from alternatives
                int i = Random.Range(0, 3); // returns a number between 0-2
                AudioManager.PlaySound(waveClearSounds[i]);
                if (nextWave == 1)
                {
                    if (Level1Tutorial.instance != null)
                    {
                        if (!Level1Tutorial.instance.lastEnemyTutorialComplete)
                        {
                            Level1Tutorial.instance.FinalEnemyTutorial();
                        }
                    }
                }
                // Wave clear message etc.
                gameHandler.WaveClearUI();

                state = State.EMPTY;
            }
            else if (leaderGuardianHealth.fillAmount <= 0)
            {
                state = State.EMPTY;
            }
            else
            {
                return;
            }
        }

        if (state == State.EMPTY)
        {
            gameHandler.GameUpdate(); // Calling the function from gameHandler that is attached to the GameManager in scene
            if (nextWave < waves.Count) {
                if (waves[nextWave].dedicatedPath == 0)
                {
                    gameHandler.waveIndicators[0].SetActive(true);
                    gameHandler.waveIndicators[1].SetActive(false);
                    gameHandler.waveIndicators[2].SetActive(false);
                }
                else if (waves[nextWave].dedicatedPath == 1)
                {
                    gameHandler.waveIndicators[1].SetActive(true);
                    gameHandler.waveIndicators[0].SetActive(false);
                    gameHandler.waveIndicators[2].SetActive(false);
                }
                else if (waves[nextWave].dedicatedPath == 2)
                {
                    gameHandler.waveIndicators[2].SetActive(true);
                    gameHandler.waveIndicators[0].SetActive(false);
                    gameHandler.waveIndicators[1].SetActive(false);
                }
                else if (waves[nextWave].dedicatedPath == 3)
                {
                    gameHandler.waveIndicators[2].SetActive(true); // The 4th possible path in level 6 begins from same place as 3th path, hence the same indicator
                    gameHandler.waveIndicators[0].SetActive(false);
                    gameHandler.waveIndicators[1].SetActive(false);
                }
                else // so if chosen random dedicated path (4th), activate all indicators
                {
                    gameHandler.waveIndicators[0].SetActive(true);
                    gameHandler.waveIndicators[1].SetActive(true);
                    gameHandler.waveIndicators[2].SetActive(true);
                }
            }
        }
    }
    public void Shuffle(Wave _wave) // This shuffles the enemies dedicated to this wave, making the spawn order randomized
    {
        for (int i = 0; i < _wave.enemies.Count - 1; i++)
        {
            int rnd = Random.Range(i, _wave.enemies.Count);
            Wave.EnemyInWave shuffledEnemy = _wave.enemies[rnd];
            _wave.enemies[rnd] = _wave.enemies[i];
            _wave.enemies[i] = shuffledEnemy;
        }
    }


    public void StartWave() // This is set to the Start button in the gamePanel. It sets the game speed to 1, deactivated the gamePanel and starts the timer and starts spawning, unless it's already spawning. 
    {
        // Play wave start sound
        AudioManager.PlaySound("Wave_start");
        GameStateManager.Instance.currentGameState = GameStateManager.GameStates.Active;
        if (waves[nextWave].isRandomized)
        {
            Shuffle(waves[nextWave]);
        }

        gameHandler.GetComponent<GameHandler>().startAndSpeedButton.GetComponent<GameStartAndSpeed>().gameON = true;
        Time.timeScale = 1f;
        gameHandler.GetComponent<GameHandler>().startAndSpeedButton.GetComponent<Animator>().Play("Idle");
        //timer = 0f; not in use atm

        if (state != State.SPAWNING)
        {
            WaveIndicatorAlphaChange(gameHandler.waveIndicators[0], 40f);
            WaveIndicatorAlphaChange(gameHandler.waveIndicators[1], 40f);
            WaveIndicatorAlphaChange(gameHandler.waveIndicators[2], 40f);
            StartCoroutine(SpawnWave(waves[nextWave])); // Starting a coroutine "SpawnWave" that spawns the enemies at the set delay from each other. It checks which of the waves it is and sends the information to the coroutine.
        }
        nextWave++; // After spawning is done, the wave number is incremented, so that next time the StartWave is called, it's the next wave.
        gameHandler.WaveTextUpdate();

        // The music during waves
        if (!AudioManager.IsPlaying("TFG_Battle_2.1") && !AudioManager.IsPlaying("TFG_Battle_1.2"))
        {
            AudioManager.StopMusic("TD_Demo");

            int rand = Random.Range(0, 2);
            if (rand == 1) AudioManager.PlayOnLoop("TFG_Battle_2.1");
            else AudioManager.PlayOnLoop("TFG_Battle_1.2");
        }
    }

    bool EnemiesAlive() // Function that checks if there's enemies alive
    {
        for (int i = 0; i < spawnedEnemies.Length; i++) // Does not take test spawns into account (dragging an enemy prefab in the game for testing)
        {
            if (spawnedEnemies[i] != null)
            {
                return true;
            }
        }
        enemiesDead = true;
        WaveIndicatorAlphaChange(gameHandler.waveIndicators[0], 190f);
        WaveIndicatorAlphaChange(gameHandler.waveIndicators[1], 190f);
        WaveIndicatorAlphaChange(gameHandler.waveIndicators[2], 190f);
        return false;
    }

    IEnumerator SpawnWave(Wave _wave) // Spawning coroutine
    {
        state = State.SPAWNING; // state is changed
        for (int i = 0; i < _wave.enemies.Count; i++) // Goes through the loop as many times as you have set the amount of enemies to be in the wave
        {
            Wave.EnemyInWave enemy = null;
            spawnedEnemies[i] = SpawnEnemy(_wave.enemies[i], _wave, out enemy); // Spawn function called and as parameters it gives a random number from 0 to the amount of different enemies you assigned for this wave
            if (enemy.delay == 0)
            {
                yield return new WaitForSeconds(_wave.delay); // waits the time set as delay until next spawn function is called
            }
            //Adding the option for individual enemies have less or more delay between spawns
            else
            {
                yield return new WaitForSeconds(enemy.delay);
            }
            // Stops spawning if paused (needed for timestorm etc.) The coroutine does nothing while paused.
            while (paused) yield return null;
        }

        state = State.WAITING; // when spawning loop is over, the state is changed into waiting 
        yield break; // leaves the coroutine
    }

    GameObject SpawnEnemy(Wave.EnemyInWave _enemy, Wave _wave, out Wave.EnemyInWave enemyInWave) // Function that instantiates the enemies at the position where the Spawner script is attached to. Enemy starts then moving towards it's first waypoint (0 by default)
    {
        Wave.EnemyInWave tempEnemy = _enemy;
        Wave.EnemyInWave spawnedEnemy = null;
        if (_wave.dedicatedPath == 4) // Number 4 means the path is chosen to be random between the possible paths
        {
            int randPath = Random.Range(0, paths.Length);
            tempEnemy.GetAttacker().path = randPath; // giving the attacker a new path value
                                                     //spawnedEnemy = Instantiate(tempEnemy, paths[randPath].transform.position, Quaternion.identity); // spawning it to a position according to the path
            spawnedEnemy = tempEnemy.Spawn(paths[randPath].transform.position);

        }
        else
        {
            tempEnemy.GetAttacker().path = _wave.dedicatedPath; // giving the attacker a new path value
            spawnedEnemy = tempEnemy.Spawn(paths[_wave.dedicatedPath].transform.position); // spawning it to a position according to the path
        }
        enemyInWave = spawnedEnemy;
        return spawnedEnemy.activeGameObject;
    }

    public void WaveIndicatorAlphaChange(GameObject indicator, float alpha) // WIP! Doesn't work for some reason.
    {
        Debug.Log("vaihtaa alphaa: " + alpha);
        Color alphaColor = indicator.GetComponent<Image>().color;
        alphaColor.a = 40f;
        indicator.GetComponent<Image>().color = alphaColor;
    }
    #endregion
}