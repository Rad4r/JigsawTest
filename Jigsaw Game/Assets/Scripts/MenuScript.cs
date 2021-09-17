using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.tvOS;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Sprite touchSprite;
    public Sprite arrowsSprite;
    public GameObject sliderImg;
    public Image controlChangeImg;

    private void Start()
    {
        if (Remote.reportAbsoluteDpadValues)
        {
            controlChangeImg.sprite = arrowsSprite;
            sliderImg.transform.localPosition = new Vector3(410.5f, 73.77f, 0);
        }
        else
        {
            controlChangeImg.sprite = touchSprite;
            sliderImg.transform.localPosition = new Vector3(-410.5f, 73.77f, 0);
        }
    }

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
    
    public void ControlChange()
    {
        GetComponent<AudioSource>().Play();
        if (Remote.reportAbsoluteDpadValues)
        {
            controlChangeImg.sprite = touchSprite;
            Remote.reportAbsoluteDpadValues = false;
            Remote.touchesEnabled = true;
            sliderImg.transform.localPosition = new Vector3(-410.5f, 73.77f, 0);
        }
        else
        {
            controlChangeImg.sprite = arrowsSprite;
            Remote.reportAbsoluteDpadValues = true;
            Remote.touchesEnabled = false;
            sliderImg.transform.localPosition = new Vector3(410.5f, 73.77f, 0);
        }
    }
    
    // public void ControlChange(GameObject img)
    // {
    //     Rect ret = sliderImg.GetComponent<RectTransform>().rect;
    //     if (Remote.reportAbsoluteDpadValues)
    //     {
    //         img.GetComponent<Image>().sprite = touchSprite;
    //         Remote.reportAbsoluteDpadValues = false;
    //         Remote.touchesEnabled = true;
    //         ret.position = new Vector2(-410.5f, 0);
    //         //sliderImg.transform.position = new Vector3(-410.5f, 0, 0);
    //     }
    //     else
    //     {
    //         img.GetComponent<Image>().sprite = arrowsSprite;
    //         Remote.reportAbsoluteDpadValues = true;
    //         Remote.touchesEnabled = false;
    //         ret.position = new Vector2(410.5f, 0);
    //         //sliderImg.transform.position = new Vector3(410.5f, 0, 0);
    //     }
    // }
}
