using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manual : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform vehicle;
    private float v;
    private float w;
    private const float wheelRad = 30f; // mm
    private const float wheelDist = 54f; // mm
    public float leftMotor = 0.0f; // degree/sec
    public float rightMotor = 0.0f; // degree/sec

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            leftMotor = 300;
            rightMotor = 300;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            leftMotor = 0;
            rightMotor = 0;
        }
        if(Input.GetKeyDown(KeyCode.X)){
            leftMotor = -90;
            rightMotor = -90;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            leftMotor = 0;
            rightMotor = 80;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            leftMotor = 80;
            rightMotor = 0;
        }
        float dt = Time.deltaTime;
        v = 0.5f*wheelRad*(leftMotor*Mathf.PI/180f) + 0.5f*wheelRad*(rightMotor*Mathf.PI/180f);
        w = 0.5f*wheelRad*(leftMotor*Mathf.PI/180f)/wheelDist - 0.5f*wheelRad*(rightMotor*Mathf.PI/180f)/wheelDist;
        v*= 0.03f; 

        float delta_x = v*dt*Mathf.Cos(vehicle.eulerAngles.y*Mathf.PI/180f);
        float delta_y = v*dt*Mathf.Sin(vehicle.eulerAngles.y*Mathf.PI/180f);
        vehicle.position += new Vector3(delta_y, 0, delta_x);
        vehicle.eulerAngles += new Vector3(0,w*180/Mathf.PI*dt,0);     
        
    }
}
