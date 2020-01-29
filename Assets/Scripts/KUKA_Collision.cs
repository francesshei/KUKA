using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KUKA_Collision : MonoBehaviour
{
    public bool touched = false;
    public string contact; 
    void Update(){}
        
    void OnCollisionEnter(Collision collision){
        Debug.Log("Collision detected");
        Debug.Log(collision.gameObject.name);
        /*if (collision.gameObject.name == "Tool")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Whoops");}*/
    }

    private void OnTriggerEnter(Collider collider){
        Debug.Log("Whoops");
        touched = true; 
        contact = collider.gameObject.name;
        //Debug.Log(collider.gameObject.name);
    }
    private void OnTriggerExit(Collider collider){
        //Debug.Log("Whoops");
        touched=false; 
        //Debug.Log(collider.gameObject.name);
    }
}
