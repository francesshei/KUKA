using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets; 
using System;
using System.Net; 
using System.IO;
using UnityEngine;

public class Server : MonoBehaviour
{
    public int port = 6321;
    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;
    
    private TcpListener server;
    private bool serverStarted; 
    
    // Start is called before the first frame update
    private void Start()
    {
      clients = new List<ServerClient>();
      disconnectList = new List<ServerClient>();
      
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
                    Debug.Log(data);
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