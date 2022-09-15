using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GollabAllert : MonoBehaviour
{
    [SerializeField] private Image alert;

    [SerializeField]private int numberOfAlertedElements = 0;

    // Start is called before the first frame update
    void Start()
    {
        turnOffTheAlarm();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void turnOnTheAlarm()
    {
        alert.color = Color.red;
    }

    private void turnOffTheAlarm()
    {
        alert.color = Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Allert find something");
        if (other.gameObject.layer == LayerMask.NameToLayer("building"))
        {
            turnOnTheAlarm();
            numberOfAlertedElements++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("building"))
        {
            numberOfAlertedElements--;
        }

        if ( numberOfAlertedElements ==0)
        {
            turnOffTheAlarm();
        }
        
    }
}