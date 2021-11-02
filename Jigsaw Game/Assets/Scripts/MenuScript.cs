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
