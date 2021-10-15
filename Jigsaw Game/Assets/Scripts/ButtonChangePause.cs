using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ButtonChangePause : MonoBehaviour
{
    public Button[] buttons;
    private GameManager GM;
    private List<Vector3> defaultScales;
    private Player player;
    private int currentButton;
    private bool buttonReset;
    private float buttonChangeDelay;
    private float maxButtonChangeAxisLimit;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        buttonChangeDelay = 0.4f; // could change
        maxButtonChangeAxisLimit = 0.8f;
        player = ReInput.players.GetPlayer(0);
        buttonReset = true;
        defaultScales = new List<Vector3>();

        for (int i = 0; i < buttons.Length; i++)
        {
            defaultScales.Add(buttons[i].transform.localScale);
        }

        UpdateUI();
    }

    private void Update()
    {
        Invoke("ButtonClickCheck",0.5f);

        if (buttonReset)
        {
            if (player.GetAxis("Drag Horizontal") < -maxButtonChangeAxisLimit)
            {
                if (currentButton <= 0)
                    currentButton = buttons.Length - 1;
                else
                    currentButton--;
                buttonReset = false;
                GM.PickUpSoundPlay();
                Invoke("ResetButton", buttonChangeDelay);
                UpdateUI();
            }
            else if (player.GetAxis("Drag Horizontal") > maxButtonChangeAxisLimit)
            {
                if (currentButton >= buttons.Length - 1)
                    currentButton = 0;
                else
                    currentButton++;
                buttonReset = false;
                GM.PickUpSoundPlay();
                Invoke("ResetButton", buttonChangeDelay);
                UpdateUI();
            }
        }
    }

    private void UpdateUI()
    {
        buttons[currentButton].transform.localScale *= 1.2f;
        buttons[currentButton].GetComponent<Animator>().enabled = true;
        buttons[currentButton].image.color = new Color32(27, 129, 255,255);

        for (int i = 0; i < buttons.Length; i++)
        {
            //var color = buttons[i].GetComponent<Button>().colors;
            if (i != currentButton)
            {
                buttons[i].transform.localScale = defaultScales[i];
                buttons[i].image.color = Color.white;
                buttons[i].GetComponent<Animator>().enabled = false; //Animation Reset
            }
        }
    }

    private void ResetButton()
    {
        buttonReset = true;
    }

    private void ButtonClickCheck()
    {
        if (player.GetButton("Touch Click"))
        {
            buttons[currentButton].onClick.Invoke();
        }
    }
}
