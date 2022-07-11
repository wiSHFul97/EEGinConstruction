using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class MyTry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // This constructor arbitrarily assigns the local port number.
        UdpClient udpClient = new UdpClient();
        
        try{
            udpClient.Connect("127.0.0.1", 1053);

            // Sends a message to the host to which you have connected.
            Byte[] sendBytes = Encoding.ASCII.GetBytes("connect");

            udpClient.Send(sendBytes, sendBytes.Length);

            // Sends a message to a different host using optional hostname and port parameters.
            UdpClient udpClientB = new UdpClient();
            udpClientB.Send(sendBytes, sendBytes.Length, "127.0.0.1", 1053);

            //IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // Blocks until a message returns on this socket from a remote host.
            // Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
            // string returnData = Encoding.ASCII.GetString(receiveBytes);

            // Debug.Log(returnData);
            UdpState s = new UdpState();
            s.e = RemoteIpEndPoint;
            s.u = udpClient;

            udpClient.BeginReceive(ReceiveCallback, s);
            
            // // Uses the IPEndPoint object to determine which of these two hosts responded.
            // Console.WriteLine("This is the message you received " +
            //                   returnData.ToString());
            // Console.WriteLine("This message was sent from " +
            //                   RemoteIpEndPoint.Address.ToString() +
            //                   " on their port number " +
            //                   RemoteIpEndPoint.Port.ToString());

            // udpClient.Close();
            // udpClientB.Close();
        }
        catch (Exception e ) {
            Console.WriteLine(e.ToString());
        }
    }
    

    public static void ReceiveCallback(IAsyncResult ar)
    {
        Debug.Log("i am here");
        UdpState udpState = ((UdpState) (ar.AsyncState));
        UdpClient u = udpState.u;
        IPEndPoint e = udpState.e;
        
        byte[] receiveBytes = u.EndReceive(ar, ref e);
        string receiveString = Encoding.ASCII.GetString(receiveBytes);
        Debug.Log(receiveString);
        u.BeginReceive(ReceiveCallback, udpState );
    }
    
    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }
}
