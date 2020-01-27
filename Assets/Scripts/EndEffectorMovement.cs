using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEffectorMovement : MonoBehaviour
{
    private GameObject joint5, joint6; 
    public float RotationSpeed = 1;
    public float RotationAcceleration = 1; 
    public float MaxRotationSpeed = 50; 
    private float currentRotationSpeed; 
    
    public bool moved = false; 
    public float angle  = 30f;
    public int inv_speed = 10; 
    // Start is called before the first frame update
    void Start()
    {
        joint5 = GameObject.Find("Ring5");
        joint6 = GameObject.Find("Head");
        currentRotationSpeed = RotationSpeed; 
    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log(joints);
        if(Input.GetKey("q")){
            currentRotationSpeed += Time.deltaTime * RotationAcceleration; 
            angle = Mathf.Clamp(currentRotationSpeed, 0, MaxRotationSpeed); 
            StartCoroutine(MoveEE(angle));
        }
        
        if(Input.GetKey("e")){
            currentRotationSpeed -= Time.deltaTime * RotationAcceleration;
            angle = Mathf.Clamp(currentRotationSpeed, 0, MaxRotationSpeed);
            StartCoroutine(MoveEE(-angle));
        }
        //Link6
        if(Input.GetKey("z")){
            currentRotationSpeed += Time.deltaTime * RotationAcceleration; 
            angle = Mathf.Clamp(currentRotationSpeed, 0, MaxRotationSpeed); 
            StartCoroutine(MoveJ(angle));
        }
        
        if(Input.GetKey("x")){
            currentRotationSpeed -= Time.deltaTime * RotationAcceleration;
            angle = Mathf.Clamp(currentRotationSpeed, 0, MaxRotationSpeed);
            StartCoroutine(MoveJ(-angle));
        }
    /*    
        if (Input.GetKeyUp("q")){
            angle = Mathf.Clamp(currentRotationSpeed, 0, MaxRotationSpeed); 
            StartCoroutine(MoveEE(angle));
        }
        if (Input.GetKeyUp("e")){
            angle = Mathf.Clamp(currentRotationSpeed, 0, MaxRotationSpeed);
            StartCoroutine(MoveEE(-angle));
        }*/
        
    }

    IEnumerator MoveEE(float joint) {
        //TODO: dividere il valore dell'angolo di ogni joint in pezzi più piccoli 
        //e mettere un contatore per incrementare gradualmente lo spostamento
        Debug.Log("Moving Kuka");
        float delta_joint = joint/inv_speed;  
        
        for (int i=0; i < inv_speed; i++){
        joint6.transform.Rotate(0f, delta_joint, 0f);
        yield return null;
        }}
        
    IEnumerator MoveJ(float joint) {
        //TODO: dividere il valore dell'angolo di ogni joint in pezzi più piccoli 
        //e mettere un contatore per incrementare gradualmente lo spostamento
        Debug.Log("Moving Kuka");
        float delta_joint = joint/inv_speed;  
        
        for (int i=0; i < inv_speed; i++){
        joint5.transform.Rotate(0f, delta_joint, 0f);
        yield return null;
        }}
        
}
