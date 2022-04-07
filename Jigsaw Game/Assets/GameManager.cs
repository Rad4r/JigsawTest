using UnityEngine;
using UnityEngine.EventSystems;
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
    public bool gameWon;
    private bool testBool;
    
    
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
        unwantedObjects[2].SetActive(false);
        remainingText.text = "You won!";
        starEffect.SetActive(true);
        if (!gameWon)
        {
            OnContinueClick();
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
            Invoke("OpenWinUI", 0.2f);
            
        }
    }

    void OpenWinUI()
    {
        //remainingText.color = new Color(250, 170, 0); // could change y to -15 for ipmax
        winPanel.SetActive(true);
        winPanel.transform.localScale = Vector3.Lerp(winPanel.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 2);
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
        if (!audi.isPlaying)
            PickUpSoundPlay();
        EventSystem.current.SetSelectedGameObject(null);
        // FindObjectOfType<PointerRemote>().transform.position = Vector3.zero;
    }
    
    public void OnBackClick()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void OnLevelBackClick()
    {
        SceneManager.LoadScene("EasyPuzzleLevels");
    }
    
    public void OnLevelBackMediumClick()
    {
        SceneManager.LoadScene("MediumPuzzleLevels");
    }
    
    public void OnLevelBackHardClick()
    {
        SceneManager.LoadScene("HardPuzzleLevels");
    }

    public void OnNextClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // might need to update
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
