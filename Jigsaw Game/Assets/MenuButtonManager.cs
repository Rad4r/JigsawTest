using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonManager : MonoBehaviour
{
    public GameObject arrow;
    private float screenDrivider;
    private GameObject currentButton;

    private void Start()
    {
        //screenDrivider = 4.8f;
        screenDrivider = 15f;
        currentButton = EventSystem.current.currentSelectedGameObject;
    }

    private void Update()
    {
        // }
        UpdateArrowPosition();
    }

    void UpdateArrowPosition()
    {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        arrow.transform.position = currentButton.transform.position + new Vector3(0,currentButton.GetComponent<RectTransform>().rect.height/50,0);
        
        Debug.Log(currentButton.GetComponent<RectTransform>().rect.height);
    }
}
