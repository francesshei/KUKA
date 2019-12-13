using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;

public class Head : MonoBehaviour 
{
    // Set 2 floating numbers for the left and right directions 
    public float LEFT;
    public float RIGHT;
    public float UP;
    public float DOWN;
    // initialize the communication with matlab
    // Use this for initialization
    TcpListener listener;
    // the msg is the value that you put in the msg matrix in matlab 
    String msg;
    // Start is called before the first frame update 
    void Start() {
    // set the floating valus for the directions. 1f is positive, while -1f is negative.
    // each value correspondo to a direciton in the axix. for instance in the X the positive value is the right
    // direction, while the negative value is the left 
    LEFT = 1f;
    RIGHT = -1f;
    UP = 1f;
    DOWN = -1f;
    // LISTEN TO MATLAB. Set up unity listening to matlab
    listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 55001); listener.Start();
    print("is listening");
    }

    // Update is called once per frame 
    void Update() {
    if (!listener.Pending()) { }
    else {
    // print the message that unity is listening. 
    print("socket comes");
    TcpClient client = listener.AcceptTcpClient(); 
    NetworkStream ns = client.GetStream(); 
    StreamReader reader = new StreamReader(ns);
    msg = reader.ReadToEnd();
    print(msg);
    }
    // this command allows to concatenate different if statement 
    switch (msg.ToLower()) {
    // set the cilinder movement case by case depending on the mtlab inputs 
        case "2":
        transform.Translate(0f, UP * Time.deltaTime, 0f);
        break;
        case "-2":
        transform.Translate(0f, DOWN * Time.deltaTime, 0f);
        break;
        case "-1":
        transform.Translate(LEFT * Time.deltaTime,0f, 0f);
        break;
        case "1":
        transform.Translate(RIGHT * Time.deltaTime,0f, 0f);
        break;
        case "3":
        transform.Translate(RIGHT * Time.deltaTime, DOWN * Time.deltaTime, 0f); 
        break;
        case "-3":
        transform.Translate(LEFT * Time.deltaTime, DOWN * Time.deltaTime, 0f); 
        break;
        case "4":
        transform.Translate(RIGHT * Time.deltaTime, UP * Time.deltaTime, 0f);
        break;
        case "-4":
        transform.Translate(LEFT * Time.deltaTime, UP * Time.deltaTime, 0f); 
        break;
        case "0":
        transform.Translate(0f, 0f, 0f);
        break;
    } 
}
}
