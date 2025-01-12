using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that lets objects be openable.
/// </summary>
public class Openable : MonoBehaviour
{
    public float yRotation = 120f;
    private bool hasBeenClicked;
    private bool isOpened;
    private void Update()
    {
        if (hasBeenClicked)
        {
            if (!isOpened)
            { 
                transform.Rotate(new Vector3(0f, yRotation, 0f));
                isOpened = true;
            }
            else
            {
                transform.Rotate(new Vector3(0f, -yRotation, 0f));
                isOpened = false;
            }

            hasBeenClicked = false;
        }
    }

    public void Open()
    {
        hasBeenClicked = true;
    }
    /*
    public float rotationPerSecond = 1f;
    public float yRotation = 120f;
    private bool hasOpened;

    private float rotationDirection;
    // Start is called before the first frame update
    private float time;
    private float currentRotation;
    void Start()
    {
        rotationDirection = yRotation;
    }

    // Update is called once per frame
    void Update()
    {  
        if (hasOpened && time < 1)
        {
            Vector3 newAngle = new Vector3(0, Mathf.LerpAngle(currentRotation, rotationDirection, time), 0);
            time += Time.deltaTime; //*rotationPerSecond;
            Debug.Log( " Angle "+ newAngle);
            transform.Rotate(newAngle);
        }
        else
        {
            hasOpened = false;
        }
    }

    public void Open()
    {
        hasOpened = true;
        time = 0f;
        if (Mathf.Abs(Mathf.Round(transform.localRotation.eulerAngles.y))< 10f)
        {
            rotationDirection = yRotation;
            transform.Rotate(Vector3.zero);
        }
        else
        {
            rotationDirection = 0f;
            transform.Rotate(new Vector3(0f, yRotation, 0f));
        }

        currentRotation = transform.localRotation.eulerAngles.y;
    }*/
    
    
}
