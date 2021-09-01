using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveObject : MonoBehaviour
{
    private GameManager GM;
    private bool moveAllow;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if(col == touchedCollider)
                {
                    moveAllow = true;
                    //GetComponent<SpriteRenderer>().sortingOrder = 1;
                    GetComponent<SortingGroup>().sortingOrder = GM.zIndex++;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllow)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                    //GetComponent<SpriteRenderer>().sortingLayerName = pieceLayer;
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                moveAllow = false;
                //GetComponent<SpriteRenderer>().sortingOrder = 0;
                //GetComponent<SortingGroup>().sortingOrder = 0;
                //GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("open"))
        {
            //Debug.Log("in the open space");
            if (other.name == gameObject.name)
            {
                transform.position = other.transform.position;
                GM.piecesRemaining--;
                GetComponent<MoveObject>().enabled = false;
                GetComponent<SortingGroup>().sortingOrder = 0;
                Destroy(other);
            }
                
        }
    }
}
