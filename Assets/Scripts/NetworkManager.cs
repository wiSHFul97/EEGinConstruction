using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Esri.GameEngine.Location;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Components;
using Newtonsoft.Json;

public class NetworkManager : Singleton<NetworkManager>
{
    [SerializeField] private Text QosText;
    [SerializeField] private int scale;
    [SerializeField] private GameObject workerGameObject;
    [SerializeField] private GameObject emptyTarget;
    [SerializeField] private string craneName = "Crane";
    [SerializeField] private Transform craneTarget;
    [SerializeField] private string ip = "127.0.0.1";
    [SerializeField] private bool isUTM;
    [SerializeField] private GameObject pivot;
    [SerializeField] private string[] gpsPivotData;
    [SerializeField] private bool isOnline;
    private ConcurrentQueue<Worker> tasks;
    private ConcurrentQueue<CraneTask> craneTasks;

    [SerializeField] private Dictionary<string, Worker> workers;
    // Start is called before the first frame update

    private double latitudePivot;
    private double longtitudePivot;
    private float heightPivot;
    private Vector3 globalVectorPosPivot;

    private const float EarthRadius = 6371000 + 1200; // R + Tehran H // not important ,no usage any more
    private const float ToMilimeter = 1000;


    private static string ACCEPTABLE = "Reliable";
    private static string NOT_ACCEPTABLE = "Not reliable";


    //todo check it to run
    void Awake()
    {
        workers = new Dictionary<string, Worker>();
        tasks = new ConcurrentQueue<Worker>();
        craneTasks = new ConcurrentQueue<CraneTask>();
        latitudePivot = latitudeConvertToDegree(gpsPivotData[0]);
        longtitudePivot = longtitudeConvertToDegree(gpsPivotData[1]);
        heightPivot = float.Parse(gpsPivotData[2]);
        globalVectorPosPivot = globalPosWithGps(latitudePivot, longtitudePivot);
        globalVectorPosPivot = globalVectorPosPivot + globalVectorPosPivot.normalized * (float) heightPivot;
    }

    void Start()
    {
        if (!isOnline)
        {
            // do nothing if its offline 
            // in offline mode LogHandler will handle the positions of the crans by the help of this class
            return;
        }

        // This constructor arbitrarily assigns the local port number.
        UdpClient udpClient = new UdpClient();

        try
        {
            udpClient.Connect(ip, 1053);

            // Sends a message to the host to which you have connected.
            Byte[] sendBytes = Encoding.ASCII.GetBytes("connect");

            udpClient.Send(sendBytes, sendBytes.Length);

            // // Sends a message to a different host using optional hostname and port parameters.
            // UdpClient udpClientB = new UdpClient();
            // udpClientB.Send(sendBytes, sendBytes.Length, "127.0.0.1", 1053);

            //IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);


            UdpState s = new UdpState();
            s.e = RemoteIpEndPoint;
            s.u = udpClient;

            udpClient.BeginReceive(ReceiveCallback, s);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    void Update()
    {
        //while (!tasks.IsEmpty && !craneTasks.IsEmpty)
        //{
        //Debug.Log("updateeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
        //Worker worker;
        //bool isFind = tasks.TryDequeue(out worker);
        //if (isFind)
        //{
        //    updateWorkers(worker);
        //}

        CraneTask craneTask;
        bool isFind = craneTasks.TryDequeue(out craneTask);
        if (isFind)
        {
            craneUpdate(craneTask);
        }

        //}
    }

    private void craneUpdate(CraneTask craneTask)
    {
        //new gps tool kit (RTK)
        Debug.Log("---- " + craneTask.x + " " + craneTask.y + " " + craneTask.z + " qos :" + craneTask.qos);
        UpdateUiQos(craneTask.qos);
        var latlon = new LatLon(Double.Parse(craneTask.y), Double.Parse(craneTask.x), Double.Parse(craneTask.z));
        latlon.Altitude -= 1271;
        Debug.Log(latlon.Latitude + " " + latlon.Longitude + " " + latlon.Altitude);
        ArcGISLocationComponent arcGISLocationComponent = craneTarget.GetComponent<ArcGISLocationComponent>();
        arcGISLocationComponent.Position = latlon;
    }


    private void UpdateUiQos(string qos)
    {
        string msg = qos + "_";
        switch (qos)
        {
            case "1":
            case "2":
            case "3":
            case "4":
                // 1 or 2 or 3 or 4
                msg += ACCEPTABLE;
                QosText.color = Color.green;
                break;
            default:
                msg += NOT_ACCEPTABLE;
                QosText.color = Color.red;
                break;
        }

        QosText.text = msg;
    }


    public void ReceiveCallback(IAsyncResult ar)
    {
        //Debug.Log("i am here");
        UdpState udpState = ((UdpState) (ar.AsyncState));
        UdpClient u = udpState.u;
        IPEndPoint e = udpState.e;

        byte[] receiveBytes = u.EndReceive(ar, ref e);
        string receiveString = Encoding.ASCII.GetString(receiveBytes);
        //Debug.Log(receiveString);
        incomingMsgHandler(receiveString);
        u.BeginReceive(ReceiveCallback, udpState);
    }


    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }


    private void incomingMsgHandler(string str)
    {
        Debug.Log("***" + str + "***");
        if (str == "accept" || str == "refreshed")
        {
            //ignore in this cases
            Debug.Log("ignored");
        }
        else
        {
            // just work for new sensor other else cluase never use any more but if u want work with 
            // old sensor u must clean code and comment this part
            HandleJsonPosCrane(str);
        }
    }

    public void HandleJsonPosCrane(string json)
    {
        CraneTask craneTask = new CraneTask();
        craneTask = JsonConvert.DeserializeObject<CraneTask>(json);
        craneTasks.Enqueue(craneTask);
        return;
    }


    private double latitudeConvertToDegree(string latitude) // ddmm.mmmmmmm
    {
        if (isUTM)
        {
            string degreeStr = latitude[0].ToString() + latitude[1];
            double degree = double.Parse(degreeStr);
            Debug.Log("latitude " + degreeStr + " degree : " + degree);

            string min = "";
            for (int i = 2; i < latitude.Length; i++)
            {
                min += latitude[i];
            }

            double minDegree = float.Parse(min) / 60;
            return degree + minDegree;
        }
        else
        {
            // its angle
            return double.Parse(latitude);
        }
    }

    private double longtitudeConvertToDegree(string longitude) //dddmm.mmmmmmm
    {
        if (isUTM)
        {
            string degreeStr = longitude[0].ToString() + longitude[1] + longitude[2];
            double degree = double.Parse(degreeStr);

            string min = "";
            for (int i = 3; i < longitude.Length; i++)
            {
                min += longitude[i];
            }

            double minDegree = double.Parse(min) / 60;

            return degree + minDegree;
        }
        else
        {
            return double.Parse(longitude);
        }
    }

    [Serializable]
    public struct Worker
    {
        public string name;
        public int x;
        public int y;
        public int z;
        public GameObject target;


        public void SetParams(string name, int x, int y, int z, GameObject target = null)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
            this.target = target;
        }
    }

    [Serializable]
    public struct CraneTask
    {
        public String name { get; set; }
        public String x { get; set; }
        public String y { get; set; }
        public String z { get; set; }
        public string qos { get; set; }


        public void SetParams(string x, string y, string z, GameObject target = null)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void SetParams(string x, string y, string z, string qos, GameObject target = null)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.qos = qos;
        }
    }


    private void updateWorkers(Worker workerData)
    {
        //handle the care
        if (workerData.name.Contains(craneName))
        {
            Debug.Log("we have a carne");
            craneTarget.position = converMilimeterToMeterAsVector(workerData.x, workerData.y, workerData.z) +
                                   pivot.transform.position;

            return;
        }

        Debug.Log("i got a workerData");
        //handle the others (workers)
        if (workers.ContainsKey(workerData.name))
        {
            //updateFields
            Worker old = workers[workerData.name];
            Debug.Log(old.name);
            workerData.target = old.target;
            workers[workerData.name] = workerData;
            updatePos(ref workerData);
        }
        else
        {
            instanceNewWorkerInPositions(ref workerData);
            workers.Add(workerData.name, workerData);
        }
    }

    private void instanceNewWorkerInPositions(ref Worker worker)
    {
        Debug.Log("i make a workerData");
        GameObject obj = Instantiate(workerGameObject, converMilimeterToMeterAsVector(worker.x, worker.y, worker.z),
            quaternion.identity);
        GameObject targetGameObject = Instantiate(emptyTarget,
            converMilimeterToMeterAsVector(worker.x, worker.y, worker.z), quaternion.identity);
        obj.GetComponent<PlayerControllers>().setTarget(targetGameObject.transform);
        worker.target = targetGameObject;
    }

    private void updatePos(ref Worker workerData)
    {
        Vector3 newPos = converMilimeterToMeterAsVector(workerData.x, workerData.y, workerData.z);
        Debug.Log("update");
        Debug.Log(workerData.target.name);
        Debug.Log(workerData.target.transform.position);
        Debug.Log(newPos);
        workerData.target.transform.position = newPos;
    }

    private Vector3 converMilimeterToMeterAsVector(int x, int y, int z)
    {
        return new Vector3((float) x / 1000, (float) y / 1000, (float) z / 1000);
    }

    //old
    private Vector3 findPosWithGpsData(double latitude, double longtitude, float h)
    {
        double deltaLat = latitude - latitudePivot;
        double deltaLong = longtitude - longtitudePivot;
        float deltaH = h - heightPivot;
        return new Vector3((float) movementBaseOnDegreeChange(deltaLat), (float) movementBaseOnDegreeChange(deltaLong),
            deltaH);
    }

    private Vector3 globalPosWithGps(double lat, double lon)
    {
        return new Vector3((float) (EarthRadius * math.cos(lat) * math.cos(lon)),
            (float) (EarthRadius * math.cos(lat) * math.sin(lon)),
            (float) (EarthRadius * math.sin(lat)));
    }

    private float movementBaseOnDegreeChange(float degree)
    {
        float radian = degree * math.PI / 180;
        return radian * EarthRadius;
    }

    private double movementBaseOnDegreeChange(double degree)
    {
        double radian = degree * math.PI / 180;
        return radian * EarthRadius;
    }

    public bool IsOnline()
    {
        return isOnline;
    }
}