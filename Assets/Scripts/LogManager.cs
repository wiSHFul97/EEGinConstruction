using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public class LogManager : MonoBehaviour
{
    [SerializeField] private float dataRatePerSec = 10;
    [SerializeField] private string fileName = "logGpsFinal";

    // Start is called before the first frame update
    private NetworkManager _NetworkManager;

    void Start()
    {
        _NetworkManager = this.gameObject.GetComponent<NetworkManager>();
        if (_NetworkManager.IsOnline())
        {
            //no needs to logs when we are in offline mode
            return;
        }

        if (fileName.Contains(".txt"))
        {
            fileName = fileName.Remove(fileName.Length - 4);
        }

        var textFile = Resources.Load<TextAsset>("logs/" + fileName);
        Debug.Log(textFile.text);
        StartCoroutine(logHandler(textFile.text));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator logHandler(string logs)
    {
        string[] lines = logs.Split('\n');
        Regex filter = new Regex("{.*}");
        Debug.Log(lines.Length);
        foreach (string line in lines)
        {
            var match = filter.Match(line);
            if (match.Success)
            {
                Debug.Log(match.Value);
                _NetworkManager.HandleJsonPosCrane(match.Value);
                yield return new WaitForSeconds(1 / dataRatePerSec);
            }
        }
    }
}