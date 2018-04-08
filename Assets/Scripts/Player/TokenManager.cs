using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour {

    #region Singleton
    public static TokenManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameStatistics localGameStatistics;

	// Use this for initialization
	void Start () {
        localGameStatistics = GlobalControl.instance.savedGameStatistics;
	}

    /// <summary>
    /// Save the tokens
    /// </summary>
    public void SaveStatistics()
    {
        GlobalControl.instance.savedGameStatistics = localGameStatistics;
    }

    /// <summary>
    /// Add the tokens
    /// </summary>
    public void AddTokens()
    {
        int level = GameManager.instance.level;
        localGameStatistics.tokens += level;
    } 
}
