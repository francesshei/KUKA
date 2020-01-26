using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEffectorMovement : MonoBehaviour
{
    private GameObject joint5, joint6; 
    public float[] joints;
    public bool moved = false; 
    public int angle  = 30;
    public int inv_speed = 10; 
    // Start is called before the first frame update
    void Start()
    {
        joint5 = GameObject.Find("Ring5");
        joint6 = GameObject.Find("Head");
    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log(joints);
        if (Input.GetKeyUp("q")){
        StartCoroutine(MoveEE(angle));
        }
        if (Input.GetKeyUp("e")){
        StartCoroutine(MoveEE(-angle));
        }
        
    }

    IEnumerator MoveEE(int joint) {
        //TODO: dividere il valore dell'angolo di ogni joint in pezzi più piccoli 
        //e mettere un contatore per incrementare gradualmente lo spostamento
        Debug.Log("Moving Kuka");
        float delta_joint = joint/inv_speed;  
        
        for (int i=0; i < inv_speed; i++){
        joint6.transform.Rotate(0f, delta_joint, 0f);
        yield return null;
        }}
        
}
