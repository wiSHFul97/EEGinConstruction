using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // [SerializeField]private GameObject character;

    [SerializeField] private Transform targetCrane;
    [SerializeField] private Transform[] cameras;
    [SerializeField] private float maxAngle;
    [SerializeField] private float frequencyLog = 1;
    [SerializeField] private int camNumber = 0;

    private string txtDocName;
    private List<CamsStatus> allStats = new List<CamsStatus>();

    void Awake()
    {
        //turn off all cameras and then turn on the first one
        turnOffAllcams();
        cameras[0].gameObject.SetActive(true);
        Debug.Log(cameras.Length);
    }

    private void Start()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/CameraLogs/");
        txtDocName = Application.streamingAssetsPath + "/CameraLogs/" + "Camera.csv";
        StartCoroutine(logInFile());
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
        List<int> stats = new List<int>();
        for (int i = 0; i < cameras.Length; i++)
        {
            Debug.Log(cameras[i].name + ": line of sight result =>  " + lineOfSight(cameras[i]));
            stats.Add(lineOfSight(cameras[i]));
            Debug.Log(stats.Count + "////***");
        }

        CamsStatus camsStatus = new CamsStatus(stats);
        allStats.Add(camsStatus);
    }

    //return 
    // 1 => out of sight 
    // 2 => there some obstacle in line of sight
    // 3 => clear line of sight
    private int lineOfSight(Transform cameraTransform)
    {
        Vector3 direction = targetCrane.position - cameraTransform.position;
        Vector3 forward = cameraTransform.forward;
        float angle = Vector3.Angle(direction, forward);
        if (angle > maxAngle)
        {
            return 1;
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

    class CamsStatus
    {
        private List<int> stats;

        public CamsStatus(List<int> stats)
        {
            this.stats = stats;
        }

        public List<int> getStats()
        {
            return stats;
        }
    }

    private IEnumerator logInFile()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f / frequencyLog);
            checkAllLineOfSights();
            SaveToFileCameraStats();
        }
    }
    
    private void SaveToFileCameraStats()
    {
        int[] ones = new int[cameras.Length];
        int[] twos = new int[cameras.Length];
        int[] threes = new int[cameras.Length];

        for (int i = 0; i < allStats.Count; i++)
        {
            List<int> stats = allStats[i].getStats();
            for (int j = 0; j < stats.Count; j++)
            {
                switch (stats[j])
                {
                    case 1:
                        ones[j]++;
                        break;
                    case 2:
                        twos[j]++;
                        break;
                    case 3:
                        threes[j]++;
                        break;
                }
            }
        }

        for (int i = 0; i < cameras.Length; i++)
        {
            Debug.Log(allStats.Count );
            string txt = cameras[i].name + "," + 1.0f * ones[i] / allStats.Count + "," +
                         1.0f * twos[i] / allStats.Count + "," + 1.0f * threes[i] / allStats.Count +"," +allStats.Count;
            File.AppendAllText(txtDocName, txt + "\n");
            Debug.Log("*** log : " + txt);
        }
        File.AppendAllText(txtDocName, "\n");
    }
}