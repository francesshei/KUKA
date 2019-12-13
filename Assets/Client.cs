using System.Collections;
using System; 
using System.Collections.Generic;
using System.Net.Sockets; 
using System.IO; 
using UnityEngine;

public class Client : MonoBehaviour
{
    private bool socketReady = false; 
    private TcpClient socket; 
    private NetworkStream stream;
    private StreamWriter writer; 
    
    
    public void ConnectToServer()
    {
        Debug.Log("Connecting to server");
        //If already connected, skip this function
        if (socketReady)
            return; 
            
        //Default host / port values
        string host = "127.0.0.1";
        int port = 8080;
        
        //Create the socket
        try
        {
            socket = new TcpClient(host,port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            
            socketReady = true;
            Debug.Log("Connected to server");
        }
        catch(Exception e)
        {
            Debug.Log("Socket error:" + e.Message);
        }
        
        
    }
    
    private void Start () 
    {
        ConnectToServer();
        //Send("Initial message");
    }
    
    private void Update ()
    {
        if (Input.GetKeyUp("up"))
        {
            Send("1");
            Debug.Log("Message sent");
        }

        if (Input.GetKeyUp("down"))
        {
            Send("2");
            Debug.Log("Message sent");
        }
        
    }
    
    //TODO: KEY PRESSING WILL TRIGGER SEND 
    private void Send(string data)
    {
        writer.WriteLine(data);
        writer.Flush(); 
    }
}
