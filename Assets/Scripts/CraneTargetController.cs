using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CraneTargetController : MonoBehaviour
{
    [SerializeField] private string baseUrl;
    [SerializeField] private double latPivot;
    [SerializeField] private double lonPivot;
    [SerializeField] private float hPivot;

    private string url;
    
    
    void Awake()
    {
        url = String.Format("?lat0={0}&lon0={1}&h0={2}", latPivot, lonPivot, hPivot);
        setNewPos(35.688734683333,51.438907011667,1189.375977);

    }
    
    
    //deprecated for old python api
    public void setNewPos(double lat, double lon, double h)
    {
        String additionalString = String.Format("&lat={0}&lon={1}&h={2}",lat,lon,h);
        StartCoroutine(GetRequest( baseUrl + url + additionalString)) ;
    }
    
    
    IEnumerator GetRequest(string uri)
    {
        Debug.Log(uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    String result = webRequest.downloadHandler.text;
                    Debug.Log(pages[page] + ":\nReceived: " + result);
                    result = result.Substring(1, result.Length - 2);
                    String[] pos = result.Split(',');
                    this.transform.position = new Vector3(float.Parse(pos[0]),float.Parse(pos[1]),float.Parse(pos[2]));
                    break;
            }
        }
    }
}
