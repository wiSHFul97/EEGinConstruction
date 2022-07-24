using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private GameObject character;
    
    [SerializeField]private Transform[] cameras;


    private int camNumber = 1;
    void Awake()
    {
        //turn off all cameras and then turn on the first one
        turnOffAllcams();
        cameras[1].gameObject.SetActive(true);
        Debug.Log(cameras.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeftCam();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRightCam();
        }
        
    }

    private void moveRightCam()
    {
        turnOffAllcams();
        camNumber++;
        if (camNumber >= cameras.Length)
        {
            camNumber = 1;
        }
        cameras[camNumber].gameObject.SetActive(true);
    }

    private void moveLeftCam()
    {
        turnOffAllcams();
        camNumber--;
        if (camNumber == 0)
        {
            camNumber = cameras.Length -1 ;
        }
        cameras[camNumber].gameObject.SetActive(true);;
    }
    
    private void changeCamera(string index)
    {
        Debug.Log(index);
        Debug.Log("----");
        if (index == "")
        {
            Debug.Log("get null");
            return;
        }

        try
        {
            int x = int.Parse(index);
            Debug.Log("i get : " + x);
            if (x == 0)
            {
                turnOffAllcams();
                character.SetActive(true);
                return;
            }

            turnOffAllcams();
            cameras[x].gameObject.SetActive(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void turnOffAllcams()
    {
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        // character.SetActive(false);
    }
}