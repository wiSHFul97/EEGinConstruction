using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ObjSelection : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject capsule;

    [SerializeField] private Transform targetPos;

    [SerializeField] private InputField XScale;

    [SerializeField] private InputField YScale;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void makeCube()
    {
        GameObject newObj = Instantiate(cube);
        newObj.transform.localScale = new Vector3(float.Parse(XScale.text=="" ? "1" : XScale.text), float.Parse(YScale.text=="" ? "1" : YScale.text),
            newObj.transform.localScale.z);
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.SetParent(targetPos,false);
    }

    public void makeSphere()
    {
        GameObject newObj = Instantiate(sphere);
        newObj.transform.localScale = new Vector3(float.Parse(XScale.text=="" ? "1" : XScale.text), float.Parse(YScale.text=="" ? "1" : YScale.text),
            newObj.transform.localScale.z);
        newObj.transform.SetParent(targetPos,false);
        newObj.transform.localPosition = Vector3.zero;
        Debug.Log("clicked sphere");
    }

    public void makeCapsule()
    {
        GameObject newObj = Instantiate(capsule, targetPos.position, quaternion.identity);
        newObj.transform.localScale = new Vector3(float.Parse(XScale.text=="" ? "1" : XScale.text), float.Parse(YScale.text=="" ? "1" : YScale.text),
            newObj.transform.localScale.z);
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.SetParent(targetPos,false);
    }
}