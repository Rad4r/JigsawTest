using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene("Menu");
    }

    public void LevelClick(string level)
    {
        SceneManager.LoadScene(level);
    }
}
