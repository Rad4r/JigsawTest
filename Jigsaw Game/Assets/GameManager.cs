using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text remainingText;
    public int zIndex;
    public int piecesRemaining;
    public GameObject UIpanel;
    void Start()
    {
        zIndex = 1;
        piecesRemaining = 36;
    }

    private void Update()
    {
        if (piecesRemaining <= 0)
            GameWon();
        else
            UIUpdate();
    }

    void UIUpdate()
    {
        remainingText.text = "Remaining pieces: " + piecesRemaining;
    }

    void GameWon()
    {
        remainingText.text = "Congrats you won!";
    }

    public void OnRetryClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnContinueClick()
    {
        UIpanel.SetActive(false);
    }
}
