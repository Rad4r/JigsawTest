using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] pieces;
    public Transform sideBar;
    public Text remainingText;
    public int zIndex;
    public int piecesRemaining;
    public GameObject UIpanel;
    void Start()
    {
        zIndex = 1;
        piecesRemaining = 15;
        foreach (var piece in pieces)
        {
            piece.position = sideBar.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-3.0f, 3.0f), 0);
            //piece.position = Vector3.Lerp(piece.position,sideBar.position + new Vector3(0, Random.Range(-5.0f, 5.0f), 0), 0.125f);
        }
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
