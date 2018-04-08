using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager of the game world
/// </summary>
public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //Current level manager
    private LevelManager levelManager;

    // Scaling variables
    #region Scaling Variables
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
    #endregion

    // Use this for initialization
    void Start () {
        levelManager = LevelManager.instance;
        levelManager.Initialize();
        level = 0;
        NewLevel();
        GlobalControl.instance.hasStartedPlaying = true;
	}
	
    /// <summary>
    /// Restart on a new level
    /// </summary>
    public void NewLevel()
    {
        level++;
        levelManager.currentRoomAmount = levelManager.startingRoomAmount + amountOfRoomsIncrease;
        if (levelManager.currentRoomAmount > levelManager.maxRoomAmount)
        {
            levelManager.currentRoomAmount = levelManager.maxRoomAmount;
        }
        totalEnemyHealthIncrease = enemyHealthScaling * (level - 1);
        totalEnemyDamageIncrease = enemyDamageScaling * (level - 1);
        totalEnemySpeedIncrease = enemySpeedScaling * (level - 1);
        amountOfRoomsIncrease = roomScaling * (level - 1);
        levelManager.Reload();

        foreach (GameObject enemy in levelManager.enemies)
        {
            EnemyManager em = enemy.GetComponent<EnemyManager>();
            em.damage = em.baseDamage + enemyDamageScaling * (level - 1);
            em.health = (int)(em.baseHealth + enemyHealthScaling * (level - 1));
            em.movementSpeed = em.baseMovementSpeed + enemySpeedScaling * (level - 1);

        }
    }

    /// <summary>
    /// Player died, kick back to hubworld
    /// </summary>
    public void PlayerDied()
    {
        GlobalControl.instance.hasStartedPlaying = true;
        TokenManager.instance.SaveStatistics();
        SceneManager.LoadScene(0);

    }

    /// <summary>
    /// Exit game to hubworld
    /// </summary>
    public void ExitGame()
    {
        GlobalControl.instance.hasStartedPlaying = false;
        SceneManager.LoadScene(0);
    }
}
