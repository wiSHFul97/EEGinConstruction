using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GollabAllert : MonoBehaviour
{
    [SerializeField] private Image alert;
	[SerializeField] private AudioSource alarmSound;

    // [SerializeField]private int numberOfAlertedElements = 0;

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
		if (!alarmSound.isPlaying)
			alarmSound.Play();

    }

    private void turnOffTheAlarm()
    {
        alert.color = Color.black;
		if (alarmSound.isPlaying)
			alarmSound.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Allert find something");
        // if (other.gameObject.layer == LayerMask.NameToLayer("building"))
        if (other.gameObject.tag == "Player")
        {
            turnOnTheAlarm();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
			turnOffTheAlarm();
        }
        
    }
}