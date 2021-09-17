using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.tvOS;

public class ButtonChange : MonoBehaviour
{
    public GameObject arrow;
    public Button[] buttons;
    public Button bottomButton;
    private List<Vector3> defaultScales;
    private Player player;
    private int currentButton;
    private bool buttonReset;
    private bool bottomEnabled;
    private float buttonChangeDelay;
    private float maxButtonChangeAxisLimit;
    private bool buttonCheckable;
    private MenuScript MS;

    private void Start()
    {
        MS = FindObjectOfType<MenuScript>();
        UnityEngine.tvOS.Remote.allowExitToHome = true;
        buttonChangeDelay = 0.2f;
        maxButtonChangeAxisLimit = 0.8f;
        player = ReInput.players.GetPlayer(0);
        buttonReset = true;
        defaultScales = new List<Vector3>();
        Invoke("ButtonCheckable", 1f);
        for (int i = 0; i < buttons.Length; i++)
            defaultScales.Add(buttons[i].transform.localScale);
        UpdateUI();
    }

    private void Update()
    {
        if(buttonCheckable)
            ButtonClickCheck();
        
        if (buttonReset)
        {
            if (bottomEnabled)
            {
                if (player.GetAxis("Drag Vertical") > maxButtonChangeAxisLimit)
                {
                    bottomEnabled = false;
                    currentButton = 1;
                    buttonReset = false;
                    GetComponent<AudioSource>().Play();
                    Invoke("ResetButton", buttonChangeDelay);
                    UpdateUI();
                }
                if (player.GetAxis("Drag Horizontal") < -maxButtonChangeAxisLimit && Remote.reportAbsoluteDpadValues)
                {
                    MS.GetComponent<AudioSource>().Play();
                    buttons[3].image.sprite = MS.touchSprite;
                    Remote.reportAbsoluteDpadValues = false;
                    Remote.touchesEnabled = true;
                    MS.sliderImg.transform.localPosition = new Vector3(-410.5f, 73.77f, 0); // can press more than once so needs invoke
                }
                else if(player.GetAxis("Drag Horizontal") > maxButtonChangeAxisLimit && !Remote.reportAbsoluteDpadValues)
                {
                    MS.GetComponent<AudioSource>().Play();
                    buttons[3].image.sprite = MS.arrowsSprite;
                    Remote.reportAbsoluteDpadValues = true;
                    Remote.touchesEnabled = false;
                    MS.sliderImg.transform.localPosition = new Vector3(410.5f, 73.77f, 0);
                }
            }
            else
            {
                if (player.GetAxis("Drag Vertical") < -maxButtonChangeAxisLimit)
                {
                    currentButton = 3;
                    bottomEnabled = true;
                    buttonReset = false;
                    GetComponent<AudioSource>().Play();
                    Invoke("ResetButton", buttonChangeDelay);
                    UpdateUI();
                }
                else if (player.GetAxis("Drag Horizontal") < -maxButtonChangeAxisLimit)
                {
                    if (currentButton <= 0)
                        currentButton = buttons.Length - 1;
                    else
                        currentButton--;
                    buttonReset = false;
                    GetComponent<AudioSource>().Play();
                    Invoke("ResetButton", buttonChangeDelay);
                    UpdateUI();
                }
                else if(player.GetAxis("Drag Horizontal") > maxButtonChangeAxisLimit)
                {
                    if (currentButton >= buttons.Length - 2)
                        currentButton = 0;
                    else
                        currentButton++;
                    buttonReset = false;
                    GetComponent<AudioSource>().Play();
                    Invoke("ResetButton", buttonChangeDelay);
                    UpdateUI();
                }
            }
            
        }
    }

    private void UpdateUI()
    {
        RectTransform ret = (RectTransform) buttons[currentButton].transform;
        Vector3 newVector = Camera.main.ScreenToWorldPoint(new Vector3(0, ret.rect.height * buttons[currentButton].transform.localScale.y, 0)) + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        arrow.transform.position = buttons[currentButton].transform.position + newVector + new Vector3(0,0.5f, 0);
        
        buttons[currentButton].transform.localScale *= 1.2f;

        for (int i = 0; i < buttons.Length; i++)
        {
            if(i != currentButton)
                buttons[i].transform.localScale = defaultScales[i];
        }
    }

    private void ResetButton()
    {
        buttonReset = true;
    }

    void ButtonClickCheck()
    {
        if (player.GetButtonDown("Touch Click"))
            buttons[currentButton].onClick.Invoke();
    }

    void ButtonCheckable()
    {
        buttonCheckable = true;
    }
}
