using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public void SetPuzzleImage(Sprite img)
    {
        for (int i = 1; i <= 36; i++)
        {
            GameObject.Find("Piece-" + i).transform.Find("PuzzlePicture").GetComponent<SpriteRenderer>().sprite = img;
            //GameObject.Find("PuzzlePicture").GetComponent<SpriteRenderer>().sprite = img;
        }

        GameObject.Find("PuzzleBackground").GetComponent<SpriteRenderer>().sprite = img;
        //GameObject.Find("PuzzlePicture").GetComponent<SpriteRenderer>().sprite = img;
    }
}
