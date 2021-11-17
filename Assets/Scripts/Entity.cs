using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    string id = "Entity";
    int numberOfActions = 5;
    float speed = 4f;
    float rotationSpeed = 125;
    float rotationDelta = 0;

    Vector3 targetPosition;
    Quaternion targetRotation;

     
    Ball b;

    [SerializeField] Ball ball;


    public List<Action> actionList = new List<Action>();

   


    bool isInAction = false;

    private void Awake()
    {
      
    }

    private void FixedUpdate()
    {

        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,speed*Time.deltaTime);
        }
        if (transform.rotation != targetRotation)
        {
            transform.Rotate(new Vector3(0,rotationDelta * rotationSpeed * Time.deltaTime,0));
        }
        if(transform.position == targetPosition && transform.rotation == targetRotation && IsInAction && b==null)
        {
            transform.position = TargetPosition;
            transform.rotation = TargetRotation;
            IsInAction = false;
            
        }
        if (b!=null && b.transform.position == b.TargetPosition && IsInAction)
        {
            Destroy(b.gameObject);
            IsInAction = false;
            Debug.Log("OUT ACTION D");
        }
    }

    public void DoAction(Action a)
    {
        switch (a.Type)
        {
            case "Forward":
                Forward();
                break;
            case "Rotate":
                rotationDelta = a.RotationDelta;
                Rotate();
                break;
            case "Shoot":
                Shoot();
                break;
        }
    }

    void Forward()
    {

        IsInAction = true;
        Vector3 f = new Vector3();
        
        f = transform.position + getFowardVector();
 
        Debug.Log("IN PRE ACTION Foward -> Check Collision");
        
        //Collision
        if (f.x < GetComponentInParent<TileMap>().MapSize.x && 0 <= f.x && 0 <= f.z && f.z < GetComponentInParent<TileMap>().MapSize.y)
        {
            targetPosition = f;
            Debug.Log("Collision OK");
        }

        Debug.Log("IN ACTION Foward from :" + transform.position + " -> " + TargetPosition + " r: " + TargetRotation.eulerAngles + " f: " + f);
    }

    private Vector3 getFowardVector()
    {
        switch (transform.rotation.eulerAngles.y)
        {
            case 0:
                return new Vector3(0, 0, 1);
            case 90:
                return new Vector3(1, 0, 0);
            case 180:
                return new Vector3(0, 0, -1);   
            case 270:
                return new Vector3(-1, 0, 0);
            default:
                Debug.Log("no orientation");
                return new Vector3(0, 0, 0);
        }
    }

    void Rotate()
    {
        IsInAction = true;
        targetRotation = transform.rotation * Quaternion.Euler(0f, 90f * rotationDelta , 0f);
        Debug.Log("IN ACTION Rotate from :" + transform.rotation.eulerAngles + " -> " + TargetRotation.eulerAngles + " p: "+ transform.position);
    }

    void Shoot()
    {
        IsInAction = true;
        b = Instantiate(ball);
        
        b.transform.position = transform.position;
        b.transform.rotation = transform.rotation;
        b.TargetPosition = b.transform.position + getFowardVector() * 10;
        Debug.Log("IN ACTION Shoot from :" + b.transform.position + " -> " + b.TargetPosition);
    }
    public string Name { get => name; set => name = value; }
    public int NumberOfActions { get => numberOfActions; set => numberOfActions = value; }
    public bool IsInAction { get => isInAction; set => isInAction = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public Quaternion TargetRotation { get => targetRotation; set => targetRotation = value; }
}
