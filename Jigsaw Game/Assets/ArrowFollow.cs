using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollow : MonoBehaviour
{
    public Transform arrowExtrude;
    void Update()
    {
        float scaleFactor = arrowExtrude.localScale.y * 0.01f;
        //Debug.Log(arrowExtrude.up);
        transform.position = arrowExtrude.position + arrowExtrude.up * scaleFactor;
        transform.rotation = arrowExtrude.rotation;
    }
}
