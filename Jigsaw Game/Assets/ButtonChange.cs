using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEditor.Rendering.LookDev;

public class ButtonChange : MonoBehaviour
{
    public GameObject arrow;
    public Button[] buttons;
    private List<Vector3> defaultScales;
    private Player player;
    private int currentButton;
    private bool buttonReset;
    private float buttonChangeDelay;
    private float maxButtonChangeAxisLimit;

    private void Start()
    {
        buttonChangeDelay = 0.2f; // could change
        maxButtonChangeAxisLimit = 0.9f;
        player = ReInput.players.GetPlayer(0);
        Debug.Log(buttons[currentButton].name);
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
        if (player.GetButton("Touch Click"))
        {
            buttons[currentButton].onClick.Invoke();
        }
        
        if (buttonReset)
        {
            if (player.GetAxis("Drag Horizontal") < -maxButtonChangeAxisLimit)
            {
                if (currentButton <= 0)
                    currentButton = buttons.Length - 1;
                else
                    currentButton--;
                buttonReset = false;
                Invoke("ResetButton", buttonChangeDelay);
                UpdateUI();
            }
            else if(player.GetAxis("Drag Horizontal") > maxButtonChangeAxisLimit)
            {
                if (currentButton >= buttons.Length - 1)
                    currentButton = 0;
                else
                    currentButton++;
                buttonReset = false;
                Invoke("ResetButton", buttonChangeDelay);
                UpdateUI();
            }
        }
        //Debug.Log("Axis Speed: " + player.GetAxis("Drag Horizontal"));
        Debug.Log("Current Button: " + currentButton);
    }

    private void UpdateUI()
    {
        RectTransform ret = (RectTransform) buttons[currentButton].transform;
        Vector3 newVector = Camera.main.ScreenToWorldPoint(new Vector3(0, ret.rect.height, 0)) + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
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
}
