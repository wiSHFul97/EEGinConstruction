using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private GameObject character;
    [SerializeField]private GameObject characterCamera;

    
    private Transform[] cameras;
    
    void Awake()
    {
        //turn off all cameras and then turn on the first one
        cameras = this.gameObject.GetComponentsInChildren<Transform>();
        turnOffAllcams();
        cameras[1].gameObject.SetActive(true);
        Debug.Log(cameras.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.inputString != "")
        {
            changeCamera(Input.inputString);
        }
    }

    private void changeCamera(string index)
    {
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
                characterCamera.SetActive(true);
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
    }
}