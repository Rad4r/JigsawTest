using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject winPanel;
    public bool buttonSet;

    private void Update()
    {
        if (!buttonSet)
        {
            if (pausePanel.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(pausePanel.GetComponentInChildren<Button>().gameObject);
                buttonSet = true;
            }
            else if (winPanel.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(winPanel.GetComponentInChildren<Button>().gameObject);
                buttonSet = true;
            }
        }
        else
            ButtonDance();
    }

    void ButtonDance()
    {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        Animator[] buttonAnimators = currentButton.transform.parent.GetComponentsInChildren<Animator>();
        currentButton.GetComponent<Animator>().SetBool("CurrentAnimation", true);
        for (int i = 0; i < buttonAnimators.Length; i++)
        {
            if (buttonAnimators[i].gameObject != currentButton)
            {
                //buttonAnimators[i].enabled = false; //Animation Reset
                buttonAnimators[i].GetComponent<Animator>().SetBool("CurrentAnimation", false);
            }
        }
    }
}
