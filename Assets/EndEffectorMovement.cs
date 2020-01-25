using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEffectorMovement : MonoBehaviour
{
    private GameObject tg1, tg2, tg3, tg4, tg5, tg6, tg7; 
    public float[] joints;
    public bool moved = false; 
    // Start is called before the first frame update
    void Start()
    {
        tg1 = GameObject.Find("Ring1");
        tg2 = GameObject.Find("Ring2");
        tg3 = GameObject.Find("Ring3");
        tg4 = GameObject.Find("Ring4");
        tg5 = GameObject.Find("Ring5");
        tg6 = GameObject.Find("Head");
        tg7 = GameObject.Find("Camera");
    
    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log(joints);
        if (Input.GetKeyUp("a")){
        if (moved==false && joints != null){
        Debug.Log("Moving Kuka");
        Debug.Log(joints[0]);
        tg1.transform.Rotate(0f, -joints[0], 0f);
        tg2.transform.Rotate(0f, joints[1], 0f);
        tg3.transform.Rotate(0f, -joints[2], 0f);
        tg4.transform.Rotate(0f, -joints[3], 0f);
        tg5.transform.Rotate(0f, -joints[4], 0f);
        tg6.transform.Rotate(0f, joints[5], 0f);
        tg7.transform.Rotate(0f, joints[6], 0f);
        moved = true; 
        }}
        
    }
}
