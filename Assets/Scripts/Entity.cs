using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour
{
    string id = "Entity";
    int life = 10;
    int numberOfActions = 5;
    float speed = 4f;
    float rotationSpeed = 125;
    float rotationDelta = 0;

    Vector3 targetPosition;
    Quaternion targetRotation;

    [SerializeField] Ball ball;
    TextMeshPro lifeLabel;
    Ball b;
    public List<Action> actionList = new List<Action>();

    bool isInAction = false;

    //INIT
    private void Awake()
    {
      
    }

    //UPDATE
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
            ;

        }
        if (b!=null && b.transform.position == b.TargetPosition && transform.rotation == targetRotation && IsInAction)
        {
            
            
            b.activeParticule();
            Destroy(b.gameObject,0.3f);
            IsInAction = false;
            
        }
    }

    //SWITCH ACTION
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

    //ACTIONS
    void Forward()
    {

        IsInAction = true;
        Vector3 f = new Vector3();
        
        f = transform.position + getFowardVector();
 

        //Collision
        if (!isCollide(f))
        {
            targetPosition = f;
        }

    }
    void Rotate()
    {
        IsInAction = true;
        targetRotation = transform.rotation * Quaternion.Euler(0f, 90f * rotationDelta , 0f);
    }
    void Shoot()
    {
        IsInAction = true;

        b = Instantiate(ball);
        b.transform.position = transform.position;
        b.transform.rotation = transform.rotation;
        int range = 10;
        bool isHit = false;

        //Get if Collide
        for(int i =  1; i < range; i++)
        {
            var l = GetComponentInParent<GameManager>().listEntities;
            foreach(Entity e in l)
            {
                Vector3 tmp = b.transform.position + getFowardVector() * i;
                Debug.Log(this + " IN PRE ACTION Shoot Collide check -> " + tmp + " = "+ e.transform.position);
                if (e.transform.position == b.transform.position + getFowardVector() * i)
                {
                    Debug.Log(" ------------------------------> Hit " + e);
                    b.TargetPosition = b.transform.position + getFowardVector() * i;
                    isHit = true;
                    e.setLife(-1);
                    Debug.Log(e + "life :"+ e.Life);
                    
                }
            }
            
        }
        if (!isHit)
        {
            b.TargetPosition = b.transform.position + getFowardVector() * 10;
        }
        
        Debug.Log(this + " IN ACTION Shoot from :" + b.transform.position + " -> " + b.TargetPosition);
    }

    private Vector3 getFowardVector()
    {
        
        switch (Mathf.Round(transform.rotation.eulerAngles.y))
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
                Debug.Log(this + " no orientation");
                return new Vector3(0, 0, 0);
        }
    }
    private bool isCollide(Vector3 f)
    {
        if (f.x < GetComponentInParent<TileMap>().MapSize.x && 0 <= f.x && 0 <= f.z && f.z < GetComponentInParent<TileMap>().MapSize.y)
        {
            var l = GetComponentInParent<GameManager>().listEntities;
            foreach (Entity e in l)
            {
                Debug.Log(this +"Check Collide : " + e + " " + e.targetPosition + " f " + f);
                if (e.TargetPosition == f)
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }
    public void setLife(int v)
    {
        Life += v;
        if (Life < 0) Life = 0;
        lifeLabel.text = Life.ToString();
    }
    public void setLifeLabel(ref TextMeshPro l)
    {
        lifeLabel = l;
    }

    public string Name { get => name; set => name = value; }
    public int NumberOfActions { get => numberOfActions; set => numberOfActions = value; }
    public bool IsInAction { get => isInAction; set => isInAction = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public Quaternion TargetRotation { get => targetRotation; set => targetRotation = value; }
    public int Life { get => life; set => life = value; }
}
