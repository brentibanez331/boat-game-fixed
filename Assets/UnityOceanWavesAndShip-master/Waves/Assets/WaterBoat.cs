using BoatGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WaterFloat))]
public class WaterBoat : MonoBehaviour
{
    //visible Properties
    public Transform Motor;
    [Header("Boat Speed")]
    public float Power = 5f;
    public float MaxSpeed = 10f;
    [Header("Boat Rotation")]
    public float SteerPower = 200f;
    public float Drag = 0.1f;

    //used Components
    protected Rigidbody Rigidbody;
    protected Quaternion StartRotation;
    protected ParticleSystem ParticleSystem;
    protected Camera Camera;

    //internal Properties
    protected Vector3 CamVel;

    bool collidedFloatingObj;
    GameObject FloatingObj;

    public MainMenu Menu;

    [SerializeField]
    ScoreManager scoreManager;
    [SerializeField]

    //crate Audio
    public AudioSource crateFX;

    public InputActionReference rotationAction;
    public InputActionReference velocityAction;


    private Vector3 angularVector;

    public void Awake()
    {
        //PickupButton.SetActive(false);
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        Rigidbody = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
        Camera = Camera.main;
    }

    public void FixedUpdate()
    {
        //float dirX = Input.acceleration.x;
        //float dirZ = Input.acceleration.z;
        //float dirX = playerControls.ReadValue<Vector2>().x;
        //float dirZ = playerControls.ReadValue<Vector2>().y;

        Vector2 rotation = rotationAction.action.ReadValue<Vector2>();
        Vector2 velocity = velocityAction.action.ReadValue<Vector2>();


        float dirX = rotation.x;
        float dirZ = velocity.y;

        //print(dirZ);
        //print(dirX);

        if (Menu.gameIsPaused)
        {
            SteerPower = 0;
            Power = 0;
        }
        else
        {
            SteerPower = 200f;
            Power = 5;
        }

        //default direction
        var forceDirection = transform.forward;
        var steer = 0;

        //steer direction [-1,0,1]
        if (dirX < -0.2)
            steer = 1;
        if (dirX > 0.2)
            steer = -1;
        if (dirX >= -0.2 && dirX <= 0.2)
        {
            steer = 0;
            angularVector = new Vector3(Rigidbody.angularVelocity.x, 0, Rigidbody.angularVelocity.z);
            //print(angularVector);
            Rigidbody.angularVelocity = angularVector;
        }

        //print(steer);


        //Rotational Force
        Vector3 force = steer * transform.right * SteerPower / 100f;
        Rigidbody.AddForceAtPosition(force, Motor.position);
        

        //compute vectors
        //var forward = Vector3.Scale(new Vector3(1,0,1), transform.forward);
        var forward = transform.forward * dirZ;
        var targetVel = Vector3.zero;

        PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * MaxSpeed, Power);

        //forward/backward poewr
        //if (Input.GetTouch(0).phase == TouchPhase.Stationary)
        //{
        //    print("You are pressing the screen");
        //    PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * MaxSpeed, Power);
        //    //print("Forward Velocity: " + Rigidbody.velocity);
        //}
        /* if (dirZ > 0.4)
        {
            PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, -forward * MaxSpeed, Power);
            //print("Reverse Velocity: " + Rigidbody.velocity);
        }*/
        /*if(dirZ >= -0.3)
        {
            //PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * 0, 0);
            Rigidbody.velocity = new Vector3(0, Rigidbody.velocity.y, 0);
        }*/

        //print(Rigidbody.velocity);
        //Motor Animation // Particle system
        Motor.SetPositionAndRotation(Motor.position, transform.rotation * StartRotation * Quaternion.Euler(0, 30f * steer, 0));
        if (ParticleSystem != null)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                ParticleSystem.Play();
            else
                ParticleSystem.Pause();
        }

        //moving forward
        var movingForward = Vector3.Cross(transform.forward, Rigidbody.velocity).y < 0;

        //move in direction
        Rigidbody.velocity = Quaternion.AngleAxis(Vector3.SignedAngle(Rigidbody.velocity, (movingForward ? 1f : 0f) * transform.forward, Vector3.up) * Drag, Vector3.up) * Rigidbody.velocity;
    }

    public void DestroyObject()
    {
        if (collidedFloatingObj)
        {
            scoreManager.AddScore();
            FloatingObj.GetComponent<WaterFloat>().enabled = false;
            FloatingObj.GetComponent<Rigidbody>().drag = 0;
            FloatingObj.GetComponent<Rigidbody>().useGravity = true;
            Destroy(FloatingObj, 3.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goods")
        {
            crateFX.Play();
            collidedFloatingObj = true;
            FloatingObj = other.gameObject;
            DestroyObject();
        }
    }
}