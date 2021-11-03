using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LevelClick(string level)
    {
        SceneManager.LoadScene(level);
    }
}
