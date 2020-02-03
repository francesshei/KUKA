using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCamera : MonoBehaviour
{
    public Camera cam1; 
    public Camera cam2; 
    public Transform target;
    
    Vector3 cartesian_pos;
    Vector3 polar;
    
    float oldxinput = 0f;
    float oldyinput = 0f;
    
    float fDamping = 120f; 
    float zoom = 10f;
    
    Gyroscope m_Gyro;
    
    // Start is called before the first frame update
    void Start()
    {
        cam1.enabled = true; 
        cam2.enabled = false;  
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        
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
        MoveCamera();
        //if(Input.acceleration.x>0.1){
        //Debug.Log(Input.acceleration.y);}
        //Debug.Log(m_Gyro.rotationRate); 
        
    }
    
    void MoveCamera(){
        cartesian_pos = cam1.transform.position;
        
        //get new values from accelerometer 
        //smoothen the gyro input:
        float newxinput = m_Gyro.rotationRate[0];
        float newyinput = m_Gyro.rotationRate[1];
        
        newxinput = Mathf.Lerp(oldxinput,newxinput, fDamping * Time.deltaTime);
        newyinput =Mathf.Lerp(oldyinput,newyinput, fDamping * Time.deltaTime);
        
        //avoid small perturbances
        if(newxinput < 0.05f && newxinput > - 0.05f){
            newxinput = 0.0f;
        }
         if(newyinput < 0.05f && newyinput > - 0.05f){
            newyinput = 0.0f;
        }
        
        //touch on the left part of the screen to zoom out, on the right to zoom in
        //if the screen is touched:
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if(touch.position.x < 750){
                polar[0]+=zoom* Time.deltaTime; 
            }
            if(touch.position.x > 1200){
                polar[0]-=zoom* Time.deltaTime;
            }
            //Debug.Log(touch.position);
        }
        
        //polar[0] -= Input.acceleration.;
        polar[1]+= newxinput;
        polar[2]+= newyinput;
        
        
        //Update values 
        cartesian_pos.x=polar[0]*Mathf.Sin(polar[1])*Mathf.Cos(polar[2]);
        cartesian_pos.y=polar[0]*Mathf.Cos(polar[1]);
        if(cartesian_pos.y < 1){
            cartesian_pos.y = 1;
        }
        cartesian_pos.z=polar[0]*Mathf.Sin(polar[1])*Mathf.Sin(polar[2]);
        
        oldxinput = newxinput;
        oldyinput = newyinput;
        
        cam1.transform.position = cartesian_pos;
        cam1.transform.LookAt(target);
        }
           
}
