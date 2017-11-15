using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

    public static void LevelFinished()
    {
        LevelGenerationManager.instance.Reload();
    }
}
