using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseClick : MonoBehaviour
{
    private GameManager GM;
    private bool moveAllow;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Ended)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (touchedCollider != null && touchedCollider.CompareTag("Pause"))
                {
                    GM.UIpanel.SetActive(true);
                }
            }
        }
    }
}