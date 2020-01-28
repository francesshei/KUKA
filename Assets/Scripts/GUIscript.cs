using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class GUIscript : MonoBehaviour
{
    public GameObject ee;
    public Camera cam; 
    public Camera cam2;
    public Vector3 des;
    //icons to be loaded in the GUI
    public Texture2D keyicon; 
    public Texture2D spaceicon; 
    public Texture2D zoominicon;
    public Texture2D zoomouticon;
    
    //desired pos to display in the dialog box
    string xdes;
    string ydes;
    string zdes;
    
    //paint styles
    public GUIStyle boxStyle; 
    public GUIStyle labelStyle; 
    
    //layout quantities 
    public float leftMargin = 35f;
    public float topMargin = 25f;
    public float keyMargin = 10f;
    public float textMargin = 40f; 
    public float size = 50f; 
    public float blockMargin = 150f; 
    
    public int width = Screen.width; 
    public int height = Screen.height;  
    //public int width = Display.renderingWidth; 

    // Update is called once per frame
    void Update()
    { 
        cam = GameObject.Find("CameraMovement").GetComponent<CameraMovement>().cam1; 
        cam2 = GameObject.Find("CameraMovement").GetComponent<CameraMovement>().cam2; 
        des = GameObject.Find("Client").GetComponent<Client>().desired; 
        xdes = des.x.ToString();
        ydes = des.y.ToString();
        zdes = des.z.ToString();
    }
    
    // GUI 
    void OnGUI(){
        //All the heavy hard-coding goes here
        DrawGUI();
        //Track end-effector position
        if(cam2.enabled){
        Vector3 eePos = cam.WorldToScreenPoint(ee.transform.position);
        GUI.Box(new Rect(eePos.x + 20f ,Screen.height - eePos.y + 20f, 120,90),"x:" + xdes + '\n' + "y:" + ydes + '\n' + "z:" + zdes, boxStyle);}
    }
    
    void DrawGUI(){
        //Rotation: 
        GUI.Label(new Rect(leftMargin,topMargin-5f,size*2,size*4),"Rotation \n commands:",labelStyle);
        //Q
        GUI.DrawTexture(new Rect(leftMargin,topMargin + 6*textMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin,topMargin + 6*textMargin,size,size),"Q",labelStyle);
        //E
        GUI.DrawTexture(new Rect(leftMargin + size + keyMargin,topMargin + 6*textMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin + size + keyMargin,topMargin + 6*textMargin,size,size),"E",labelStyle);
        //Z
        GUI.DrawTexture(new Rect(leftMargin,topMargin + size + keyMargin + 6*textMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin,topMargin + size + keyMargin + 6*textMargin,size,size),"Z",labelStyle);
        //X
        GUI.DrawTexture(new Rect(leftMargin + size + keyMargin,topMargin + size + keyMargin + 6*textMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin + size + keyMargin,topMargin + size + keyMargin + 6*textMargin,size,size),"X",labelStyle);
        
        //Translation:
        GUI.Label(new Rect(leftMargin, 2 * topMargin + blockMargin - 5f ,size*2,size*4),"Translation \n commands:",labelStyle);
        //W
        GUI.DrawTexture(new Rect(leftMargin + size + keyMargin, 2 * topMargin + blockMargin + keyMargin + size ,size,size),keyicon);
        GUI.Label(new Rect(leftMargin + size + keyMargin,2* topMargin + blockMargin + keyMargin + size,size,size),"W",labelStyle);
        //S
        GUI.DrawTexture(new Rect(leftMargin + size + keyMargin, 2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin + size + keyMargin, 2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),"S",labelStyle);
        //A
        GUI.DrawTexture(new Rect(leftMargin, 2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin,2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),"A",labelStyle);
        //D
        GUI.DrawTexture(new Rect(leftMargin + 2*size + 2*keyMargin, 2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin + 2*size + 2*keyMargin, 2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),"D",labelStyle);
        //R
        GUI.DrawTexture(new Rect(leftMargin + 3*size + 3*keyMargin, 2 * topMargin + blockMargin + keyMargin + size,size,size),keyicon);
        GUI.Label(new Rect(leftMargin + 3*size + 3*keyMargin, 2 * topMargin + blockMargin + keyMargin + size,size,size),"R",labelStyle);
        //F
        GUI.DrawTexture(new Rect(leftMargin + 3*size + 3*keyMargin, 2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),keyicon);
        GUI.Label(new Rect(leftMargin + 3*size + 3*keyMargin, 2* topMargin + blockMargin + 2*size + 2*keyMargin,size,size),"F",labelStyle);
        
        
        //Space
        GUI.Label(new Rect(width + size + keyMargin, 2*height - size,4*size,size),"Change camera:",labelStyle);
        GUI.DrawTexture(new Rect(width + size + leftMargin , 2*height ,4*size,size),spaceicon);
        
        
        //Check if main moving camera is active
        if (cam.enabled){
        GUI.Label(new Rect(2*width + 3*size, topMargin-5f,size*3,size*4),"Camera \n movements:",labelStyle);
        //↑
        GUI.DrawTexture(new Rect(2*width + 4*size + keyMargin, topMargin + 6*textMargin,size,size),keyicon);
        GUI.Label(new Rect(2*width + 4*size + keyMargin, topMargin + 6*textMargin,size,size),"↑",labelStyle);
        //↓
        GUI.DrawTexture(new Rect(2*width + 4*size + keyMargin, topMargin + 6*textMargin + size + keyMargin,size,size),keyicon);
        GUI.Label(new Rect(2*width + 4*size + keyMargin, topMargin + 6*textMargin + size + keyMargin ,size,size),"↓",labelStyle);
        //←
        GUI.DrawTexture(new Rect(2*width + 3*size, topMargin + 6*textMargin + size + keyMargin ,size,size),keyicon);
        GUI.Label(new Rect(2*width + 3*size,topMargin + 6*textMargin + size + keyMargin ,size,size),"←",labelStyle);
        //→
        GUI.DrawTexture(new Rect(2*width + 5*size + 2*keyMargin, topMargin + 6*textMargin + size + keyMargin ,size,size),keyicon);
        GUI.Label(new Rect(2*width + 5*size + 2*keyMargin, topMargin + 6*textMargin + size + keyMargin ,size,size),"→",labelStyle);
        
        //Zoom 
        //N
        GUI.DrawTexture(new Rect(2*width + 4*size + keyMargin, 2* topMargin + blockMargin + keyMargin,size/2,size/2),zoominicon);
        GUI.DrawTexture(new Rect(2*width + 4*size + keyMargin, 2* topMargin + blockMargin + size,size,size),keyicon);
        GUI.Label(new Rect(2*width + 4*size + keyMargin,2* topMargin + blockMargin + size, size,size),"N",labelStyle);
        //M
        GUI.DrawTexture(new Rect(2*width + 5*size + 2*keyMargin, 2* topMargin + blockMargin + keyMargin,size/2,size/2),zoomouticon);
        GUI.DrawTexture(new Rect(2*width + 5*size + 2*keyMargin,2* topMargin + blockMargin + size,size,size),keyicon);
        GUI.Label(new Rect(2*width + 5*size + 2*keyMargin, 2* topMargin + blockMargin + size,size,size),"M",labelStyle);
        
        
        }
        
        
    }
}
