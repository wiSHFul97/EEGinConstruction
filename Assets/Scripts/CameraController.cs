using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // [SerializeField]private GameObject character;

    [SerializeField] private Transform targetCrane;
    [SerializeField] private Transform[] cameras;
    [SerializeField] private float maxAngle;


    [SerializeField] private int camNumber = 0;

    void Awake()
    {
        //turn off all cameras and then turn on the first one
        turnOffAllcams();
        cameras[0].gameObject.SetActive(true);
        Debug.Log(cameras.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("move left");
            moveLeftCam();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("move right");
            moveRightCam();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            checkAllLineOfSights();
        }
    }

    private void moveRightCam()
    {
        turnOffAllcams();
        camNumber++;
        if (camNumber >= cameras.Length)
        {
            camNumber = 0;
        }

        cameras[camNumber].gameObject.SetActive(true);
    }

    private void moveLeftCam()
    {
        turnOffAllcams();
        camNumber--;
        if (camNumber == -1)
        {
            camNumber = cameras.Length - 1;
        }

        cameras[camNumber].gameObject.SetActive(true);
        ;
    }


    private void turnOffAllcams()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        // character.SetActive(false);
    }

    private void checkAllLineOfSights()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            Debug.Log(cameras[i].name + ": line of sight result =>  " + lineOfSight(cameras[i]));
        }
    }

    //return 
    // 0 => out of sight 
    // 1 => there some obstacle in line of sight
    // 2 => clear line of sight
    private int lineOfSight(Transform cameraTransform)
    {
        Vector3 direction = targetCrane.position - cameraTransform.position;
        Vector3 forward = cameraTransform.forward;
        float angle = Vector3.Angle(direction, forward);
        if (angle > maxAngle)
        {
            return 0;
        }

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cameraTransform.position, direction, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(cameraTransform.position, direction,
                Color.yellow);
            Debug.Log("Did Hit");
            if (hit.transform.name.Equals(targetCrane.name))
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }

        Debug.Log("Did not Hit any thing");
        return 404;
    }
}