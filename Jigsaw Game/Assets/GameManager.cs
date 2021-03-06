using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text remainingText;
    public int zIndex;
    public int piecesRemaining;
    public GameObject UIpanel;
    public GameObject winPanel;
    public GameObject[] unwantedObjects;
    public GameObject completedPuzzle;
    public GameObject starEffect;
    public AudioClip pickupSound;
    public AudioClip correctDropOffSound;
    public AudioClip winSound;
    public AudioClip whooshSound;
    private bool gameWon;
    
    
    private AudioSource audi;
    void Start()
    {
        audi = GetComponent<AudioSource>();
        zIndex = 1;
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
        starEffect.SetActive(true);
        if (!gameWon)
        {
            audi.PlayOneShot(winSound);
            Invoke("WhooshSoundPlay", 2f);
            Invoke("CorrectPieceSoundPlay", 2.5f);
            gameWon = true;
        }
        

        if (starEffect.GetComponent<ParticleSystem>().isStopped)
        {
            for (int i = 0; i < unwantedObjects.Length; i++)
                unwantedObjects[i].SetActive(false);
            completedPuzzle.SetActive(true);
            Invoke("OpenWinUI", 0.5f);
            
        }
    }

    void OpenWinUI()
    {
        remainingText.color = new Color(250, 170, 0); // could change y to -15 for ipmax
        winPanel.SetActive(true);
        winPanel.transform.localScale = Vector3.Lerp(winPanel.transform.localScale, new Vector3(1, 1, 1), 0.015f);
    }

    public void OnPauseClick()
    {
        UIpanel.SetActive(true);
        PickUpSoundPlay();
    }

    public void OnRetryClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnContinueClick()
    {
        UIpanel.SetActive(false);
        PickUpSoundPlay();
    }
    
    public void OnBackClick()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void OnNextClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CorrectPieceSoundPlay()
    {
        audi.PlayOneShot(correctDropOffSound);
    }
    public void PickUpSoundPlay()
    {
        audi.PlayOneShot(pickupSound);
    }

    void WhooshSoundPlay()
    {
        audi.PlayOneShot(whooshSound);
    }
}
