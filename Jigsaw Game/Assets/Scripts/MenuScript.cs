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
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Puzzle1");
    }
    public void MediumLevelClick()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Puzzle2");
    }
    public void HardLevelClick()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Puzzle3");
    }
}
