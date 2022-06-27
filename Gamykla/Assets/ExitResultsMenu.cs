using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitResultsMenu : MonoBehaviour
{
    LevelLoaded levelLoaded;

    public void LoadNextLevel()
    {
        levelLoaded = GameObject.FindGameObjectWithTag("Finish").GetComponent<LevelLoaded>();
        levelLoaded.CollectChildrenAndExit();
    }
}
