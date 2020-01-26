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
    public Vector3 initial;
    public Vector3 desired;
    public float increment; 
    public int speed = 20;
    
    
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
        //RetrieveAngles();
        //DisplayPosition();
        initial = GameObject.FindGameObjectWithTag("EndEffector").transform.position;
        desired.x = 0f;
        desired.y = 16f;
        desired.z = 9.5f;
        
        String message = UpdatePosition();
        Send(message);
        //Send("Initial message");
    }
    
    private void Update ()
    {
        String message = UpdatePosition();
        //RetrieveAngles(desired);
        
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("r") || Input.GetKeyUp("f"))
        {
            Send(message);
            Debug.Log("Message sent");
        }        
    }
    
    private void Send(string data)
    {
        writer.WriteLine(data);
        writer.Flush(); 
    }
    
    /*private string RetrievePosition(){
        Vector3 pos = GameObject.FindGameObjectWithTag("EndEffector").transform.position;
        String x_string = pos.x.ToString();
        String y_string = pos.y.ToString();
        String z_string = pos.z.ToString();
        return x_string + "#" + y_string + "#" + z_string;  
    }*/
    
    private string UpdatePosition(){
        //Due to intrinsic Unity-MATLAB error it's not feasible to send the resulting end-effector pose, as it may happen to be a non-reachable position 
        
        //Forward
        if (Input.GetKeyDown("w")){
            increment = 0.1f;
            if(Input.GetKey("w")){
            increment += speed * Time.deltaTime;}
            desired.z = desired.z + increment;
        }
        //Backwward
        if (Input.GetKeyDown("s")){
            increment = 0.1f;
            if(Input.GetKey("s")){
            increment += speed * Time.deltaTime;}
            desired.z = desired.z - increment;
        }
        //Left
        if (Input.GetKeyDown("a")){
            increment = 0.1f;
            if(Input.GetKey("a")){
            increment += speed * Time.deltaTime;}
            desired.x = desired.x + increment;
        }
        //Right
        if (Input.GetKeyDown("d")){
            increment = 0.1f;
            if(Input.GetKey("d")){
            increment += speed * Time.deltaTime;}
            desired.x = desired.x - increment;
        }
        //Up
        if (Input.GetKeyDown("r")){
            increment = 0.1f;
            if(Input.GetKey("r")){
            increment += speed * Time.deltaTime;}
            desired.y = desired.y + increment;
        }
        //Down
        if (Input.GetKeyDown("f")){
            increment = 0.1f;
            if(Input.GetKey("f")){
            increment += speed * Time.deltaTime;}
            desired.y = desired.y - increment;
        }
        String quaternion = RetrieveAngles(desired); 
        
        String x_string = desired.x.ToString();
        String y_string = desired.y.ToString();
        String z_string = desired.z.ToString();
        
        //Debug.Log(desired.x);
        //Debug.Log(desired.y);
        //Debug.Log(desired.z);
        //String quaternion = RetrieveAngles(desired); 
        
        return x_string + "#" + y_string + "#" + z_string + '#' + quaternion; 
    }
    
   /*private void DisplayPosition(){
        Vector3 dispos = GameObject.FindGameObjectWithTag("EndEffector").transform.position;
        Debug.Log(dispos);
        Vector3 shoulder = GameObject.Find("Ring2").transform.position;
        Debug.Log("Shoulder position:");
        Debug.Log(shoulder);
        Vector3 elbow = GameObject.Find("Ring4").transform.position;
        Debug.Log("Elbow position:");
        Debug.Log(elbow);
        Vector3 wrist = GameObject.Find("Head").transform.position;
        Debug.Log("Wrist position:");
        Debug.Log(wrist);
        Vector3 tool = GameObject.Find("Camera").transform.position;
        Debug.Log("Tool position:");
        Debug.Log(tool);
    }*/
    
    private string RetrieveAngles(Vector3 dir){
        Vector3 lookdir = GameObject.FindGameObjectWithTag("Kidney").transform.position - dir; 
        
        Quaternion quat = Quaternion.LookRotation(lookdir, Vector3.up); 
        return quat.ToString();
        
        
    }
}
