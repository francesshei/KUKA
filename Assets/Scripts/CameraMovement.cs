﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The camera movement implemented here is a look-at-camera, in which polar coordinate are used to determine the position of the camera point on a semisphere around the object (constrained by a radius value) and the view axis of the camera is fixed to the line connecting the center of the object to the camera point 

public class CameraMovement : MonoBehaviour
{
    Vector3 cartesian_pos;
    Vector3 polar;
    public Camera cam1; 
    public Camera cam2; 
    
    public Transform target;
    
    // Start is called before the first frame update
    // Declare the linear and angular velocity of the camera here 
    void Start()
    {
        cam1.enabled = true; 
        cam2.enabled = false;  
        
        //Cartesian camera coordinates
        cartesian_pos.x = 40;
        cartesian_pos.y = 40; 
        cartesian_pos.z = 0; 
        cam1.transform.position = cartesian_pos;
        //r
        polar[0] = Mathf.Sqrt(cartesian_pos.x * cartesian_pos.x + cartesian_pos.y * cartesian_pos.y + cartesian_pos.z * cartesian_pos.z);
        //theta
        polar[1] = Mathf.Acos(cartesian_pos.y / polar[0]);
        //phi
        polar[2] = Mathf.Atan(cartesian_pos.x / cartesian_pos.z);
        
        cam1.transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
      if(cam1.enabled==true){ 
        MoveCamera();
      }
      if(Input.GetKeyDown("space")){
        SwitchCameras();
      }
    }
    
    void SwitchCameras(){
        cam1.enabled = !cam1.enabled; 
        cam2.enabled = !cam2.enabled; 
    }
    
    void MoveCamera(){
    
    cartesian_pos = cam1.transform.position;
    
    if(Input.GetKey("right")){
      polar[2] = polar[2] + 0.1F;
      cartesian_pos.x = polar[0]*Mathf.Sin(polar[1])*Mathf.Cos(polar[2]);
      cartesian_pos.y = polar[0]*Mathf.Cos(polar[1]);
      cartesian_pos.z = polar[0]*Mathf.Sin(polar[1])*Mathf.Sin(polar[2]);
      //Update the camera matrix
      cam1.transform.position = cartesian_pos;
      cam1.transform.LookAt(target);
      }
      
      if(Input.GetKey("left")){
      polar[2] = polar[2] - 0.1F;
      cartesian_pos.x=polar[0]*Mathf.Sin(polar[1])*Mathf.Cos(polar[2]);
      cartesian_pos.y=polar[0]*Mathf.Cos(polar[1]);
      cartesian_pos.z=polar[0]*Mathf.Sin(polar[1])*Mathf.Sin(polar[2]);
      //Update the camera matrix
      cam1.transform.position = cartesian_pos;
      cam1.transform.LookAt(target);
      }
      
      if(Input.GetKey("up")){
      if (cartesian_pos.y < (polar[0] * 5.5/6.0)){
      polar[1] = polar[1] - 0.1F;
      cartesian_pos.x=polar[0]*Mathf.Sin(polar[1])*Mathf.Cos(polar[2]);
      cartesian_pos.y=polar[0]*Mathf.Cos(polar[1]);
      cartesian_pos.z=polar[0]*Mathf.Sin(polar[1])*Mathf.Sin(polar[2]);
      //Update the cmaera matrix
      cam1.transform.position = cartesian_pos;
      cam1.transform.LookAt(target);}
      }
      
      if(Input.GetKey("down")){
      if (cartesian_pos.y > 5.0){
      polar[1] = polar[1] + 0.1F;
      cartesian_pos.x=polar[0]*Mathf.Sin(polar[1])*Mathf.Cos(polar[2]);
      cartesian_pos.y=polar[0]*Mathf.Cos(polar[1]);
      cartesian_pos.z=polar[0]*Mathf.Sin(polar[1])*Mathf.Sin(polar[2]);
      //Update the matrix
      cam1.transform.position = cartesian_pos;
      cam1.transform.LookAt(target);}
      }
      
      if(Input.GetKey("n")){
      if (polar[0] > 15.0){
      polar[0] = polar[0] - 0.5F;
      cartesian_pos.x=polar[0]*Mathf.Sin(polar[1])*Mathf.Cos(polar[2]);
      cartesian_pos.y=polar[0]*Mathf.Cos(polar[1]);
      cartesian_pos.z=polar[0]*Mathf.Sin(polar[1])*Mathf.Sin(polar[2]);
      //Update the camera matrix
      cam1.transform.position = cartesian_pos;
      cam1.transform.LookAt(target);}
      }
      
      if(Input.GetKey("m")){
      if (polar[0] < 50.0){
      polar[0] = polar[0] + 0.5F;
      cartesian_pos.x=polar[0]*Mathf.Sin(polar[1])*Mathf.Cos(polar[2]);
      cartesian_pos.y=polar[0]*Mathf.Cos(polar[1]);
      cartesian_pos.z=polar[0]*Mathf.Sin(polar[1])*Mathf.Sin(polar[2]);
      //Update the camera matrix
      cam1.transform.position = cartesian_pos;
      cam1.transform.LookAt(target);}}
    }
}
