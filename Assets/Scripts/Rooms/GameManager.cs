using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private LevelManager levelManager;

    public int level;
    public int earnedTokens;
    public int roomScaling;
    public int amountOfRoomsIncrease;

    public float enemyHealthScaling;
    public float enemyDamageScaling;
    public float enemySpeedScaling;

    public float totalEnemyHealthIncrease;
    public float totalEnemyDamageIncrease;
    public float totalEnemySpeedIncrease;


    // Use this for initialization
    void Start () {
        levelManager = LevelManager.instance;
        levelManager.Initialize();
        level = 0;
        NewLevel();
        GlobalControl.instance.hasStartedPlaying = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewLevel()
    {
        level++;
        levelManager.Reload();
        totalEnemyHealthIncrease = enemyHealthScaling * (level - 1);
        totalEnemyDamageIncrease = enemyDamageScaling * (level - 1);
        totalEnemySpeedIncrease = enemySpeedScaling * (level - 1);
        amountOfRoomsIncrease = roomScaling * (level - 1);

        levelManager.currentRoomAmount = levelManager.startingRoomAmount + amountOfRoomsIncrease;
        if(levelManager.currentRoomAmount > levelManager.maxRoomAmount)
        {
            levelManager.currentRoomAmount = levelManager.maxRoomAmount;
        }

        foreach(GameObject enemy in levelManager.enemies)
        {
            EnemyManager em = enemy.GetComponent<EnemyManager>();
            em.damage = em.baseDamage + enemyDamageScaling * (level - 1);
            em.health = (int)(em.baseHealth + enemyHealthScaling * (level - 1));
            em.movementSpeed = em.baseMovementSpeed + enemySpeedScaling * (level - 1);

        }
    }

    public void PlayerDied()
    {
        GlobalControl.instance.hasStartedPlaying = true;
        TokenManager.instance.SaveStatistics();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void ExitGame()
    {
        GlobalControl.instance.hasStartedPlaying = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
