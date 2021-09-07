using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
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
