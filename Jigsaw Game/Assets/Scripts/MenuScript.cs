using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void EasyLevelClick()
    {
        LoadLevel("EasyPuzzleLevels");
    }
    public void MediumLevelClick()
    {
        LoadLevel("MediumPuzzleLevels");
    }
    public void HardLevelClick()
    {
        LoadLevel("HardPuzzleLevels");
    }

    void LoadLevel(string level)
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(level);
    }
}
