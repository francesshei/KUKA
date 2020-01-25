using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets; 
using System;
using System.Net; 
using System.IO;
using UnityEngine;
using System.Globalization;

public class Server : MonoBehaviour
{
    public int port = 6321;
    public float[] joints = new float[7];
    public float[] delta_joints = new float[7];
    //Control the speed here
    public int inv_speed = 5; 
    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;
    private GameObject tg1, tg2, tg3, tg4, tg5, tg6, tg7;  
    
    private TcpListener server;
    private bool serverStarted; 
    
    // Start is called before the first frame update
    private void Start()
    {
      clients = new List<ServerClient>();
      disconnectList = new List<ServerClient>();
      
      tg1 = GameObject.Find("Ring1");
      tg2 = GameObject.Find("Ring2");
      tg3 = GameObject.Find("Ring3");
      tg4 = GameObject.Find("Ring4");
      tg5 = GameObject.Find("Ring5");
      tg6 = GameObject.Find("Head");
      tg7 = GameObject.Find("Camera");
      
      try
      {
        server = new TcpListener(IPAddress.Any, port);
        server.Start();
        
        StartListening();
        serverStarted = true;
        Debug.Log("Server has been started");
      }
      catch (Exception e)
      {
        Debug.Log("Socket error:" + e);
      }
    
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!serverStarted)
            return; 
        foreach (ServerClient c in clients)
        {
            if(!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectList.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if(s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s,true);
                    string data = reader.ReadLine();
                    //Debug.Log(data);
                    joints = ReconstructJoints(data);
                    Debug.Log(joints);
                    StartCoroutine(MoveKuka(joints));
                    //MoveKuka(joints);
                    //if data not null add processing function here
                }
            }
        }
    }
    
    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }
    private bool IsConnected (TcpClient c)
    {
        try
        {
            if(c!=null && c.Client != null && c.Client.Connected)
            {
                if(c.Client.Poll(0,SelectMode.SelectRead))
                {
                return !(c.Client.Receive(new byte [1], SocketFlags.Peek)==0);
                }
                
                return true; 
            }
            else 
                return false; 
        }
        catch
        {
            return false;
        }
    
    }
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening(); 
    }
    
    private float[] ReconstructJoints(String data){
        String[] joints_str = data.Split('#');
        float[] joints = new float[7]; 
        for (int i =0; i < joints_str.Length; i++){
        joints[i] = float.Parse(joints_str[i], CultureInfo.InvariantCulture.NumberFormat);
        joints[i] = Convert.ToSingle(joints[i] * (180.0 / 3.1415));
        //Debug.Log(joints[i]);
        }
        return joints;
    }
    
    IEnumerator MoveKuka(float[] joints) {
        //TODO: dividere il valore dell'angolo di ogni joint in pezzi più piccoli 
        //e mettere un contatore per incrementare gradualmente lo spostamento
        Debug.Log("Moving Kuka");
        for (int i=0; i < joints.Length; i++){
        delta_joints[i] = joints[i]/inv_speed;  
        }
        
        for (int i=0; i < inv_speed; i++){
        tg1.transform.Rotate(0f, -delta_joints[0], 0f);
        tg2.transform.Rotate(0f, delta_joints[1], 0f);
        tg3.transform.Rotate(0f, -delta_joints[2], 0f);
        tg4.transform.Rotate(0f, -delta_joints[3], 0f);
        tg5.transform.Rotate(0f, -delta_joints[4], 0f);
        tg6.transform.Rotate(0f, delta_joints[5], 0f);
        tg7.transform.Rotate(0f, delta_joints[6], 0f);
        //Debug.Log(tg7.transform.position);
        yield return null;
        }
        
       
    }
    
    

    
}

public class ServerClient 
{
    public TcpClient tcp; 
    public string clientName; 
    
    public ServerClient (TcpClient clientSocket)
    {
        clientName = "Guest"; 
        tcp = clientSocket; 
    }
        
    
}