using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureAttach : MonoBehaviour
{
    public List<GameObject> pieces;
    public GameObject puzzleImage;
    // Start is called before the first frame update
    void Start()
    {
        //pieces = new List<GameObject>();
        foreach (var piece in pieces)
        {
            GameObject imgPuz = Instantiate(puzzleImage, puzzleImage.transform.position, Quaternion.identity);
            imgPuz.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            imgPuz.transform.parent = piece.transform;
        }

        puzzleImage.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 80);
        
    }
}
