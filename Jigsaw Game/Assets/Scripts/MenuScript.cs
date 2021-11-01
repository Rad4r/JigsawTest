using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.tvOS;

public class MenuScript : MonoBehaviour
{
    private void Start()
    {
        Remote.allowExitToHome = true;
    }

    public void EasyLevelClick()
    {
        LoadLevel("EasyPuzzleLevels");
    }
    public void MediumLevelClick()
    {
        LoadLevel("Puzzle2");
    }
    public void HardLevelClick()
    {
        LoadLevel("Puzzle3");
    }

    void LoadLevel(string level)
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(level);
    }
}
