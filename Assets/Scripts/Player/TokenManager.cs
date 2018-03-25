using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour {

    public static TokenManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameStatistics localGameStatistics;

	// Use this for initialization
	void Start () {
        localGameStatistics = GlobalControl.instance.savedGameStatistics;
	}

    public void SaveStatistics()
    {
        GlobalControl.instance.savedGameStatistics = localGameStatistics;
    }

    public void AddTokens()
    {
        int level = GameManager.instance.level;
        localGameStatistics.tokens += level;
    } 
}
