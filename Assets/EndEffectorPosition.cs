using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEffectorPosition : MonoBehaviour
{
    Vector3 pos;
    Quaternion rot; 
    // Start is called before the first frame update
    void Start()
    {
        pos = GameObject.FindGameObjectWithTag("EndEffector").transform.position;
        rot = GameObject.FindGameObjectWithTag("EndEffector").transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
      Debug.Log(pos);
      Debug.Log(rot);
    }
}
