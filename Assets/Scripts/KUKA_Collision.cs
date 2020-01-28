using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KUKA_Collision : MonoBehaviour
{

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
        Debug.Log(collider.gameObject.name);
    }
}
