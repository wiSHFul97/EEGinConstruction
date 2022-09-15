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
    
    [SerializeField] private InputField ZScale;

    [SerializeField] private InputField XScaleColl;

    [SerializeField] private InputField YScaleColl;
    
    [SerializeField] private InputField ZScaleColl;


    
    private GameObject lastSelectedObj = null;

    public void makeCube()
    {
        GameObject newObj = Instantiate(cube);
        newObj.transform.localScale = new Vector3(float.Parse(XScale.text == "" ? "1" : XScale.text),
            float.Parse(YScale.text == "" ? "1" : YScale.text),
            float.Parse(ZScale.text == "" ? "1" : ZScale.text));
        newObj.transform.SetParent(targetPos, false);
        newObj.transform.localPosition = Vector3.zero;
        UpdateLastSelectedObj(newObj);
        
        //cube also get check box collider
        BoxCollider collider = newObj.GetComponent<BoxCollider>();
        collider.size = new Vector3(float.Parse(XScale.text == "" ? "1" : XScaleColl.text),
            float.Parse(YScale.text == "" ? "1" : YScaleColl.text),
            float.Parse(ZScale.text == "" ? "1" : ZScaleColl.text));
    }

    public void makeSphere()
    {
        GameObject newObj = Instantiate(sphere);
        newObj.transform.localScale = new Vector3(float.Parse(XScale.text == "" ? "1" : XScale.text),
            float.Parse(YScale.text == "" ? "1" : YScale.text),
            float.Parse(ZScale.text == "" ? "1" : ZScale.text));
        newObj.transform.SetParent(targetPos, false);
        newObj.transform.localPosition = Vector3.zero;
        UpdateLastSelectedObj(newObj);
        Debug.Log("clicked sphere");
    }

    public void makeCapsule()
    {
        GameObject newObj = Instantiate(capsule, targetPos.position, quaternion.identity);
        newObj.transform.localScale = new Vector3(float.Parse(XScale.text == "" ? "1" : XScale.text),
            float.Parse(YScale.text == "" ? "1" : YScale.text),
            float.Parse(ZScale.text == "" ? "1" : ZScale.text));
        newObj.transform.SetParent(targetPos, false);
        newObj.transform.localPosition = Vector3.zero;
        UpdateLastSelectedObj(newObj);
    }

    public void installButton()
    {
        if (lastSelectedObj == null)
        {
            return;
        }

        lastSelectedObj.transform.parent = null;
        lastSelectedObj = null;
    }

    private void UpdateLastSelectedObj(GameObject newObj)
    {
        if (lastSelectedObj != null)
        {
            Destroy(lastSelectedObj);
        }

        lastSelectedObj = newObj;
    }
}