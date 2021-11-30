using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : Entity
{

    int life = 10;

    [SerializeField] float speed = 4f;
    [SerializeField] float rotationSpeed = 125;

    [SerializeField] Bullets bullet;

    float rotationDelta = 0;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    bool isInAction = false;
    
    Bullets b;

    //GETTER SETTER
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float RotationDelta { get => rotationDelta; set => rotationDelta = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public Quaternion TargetRotation { get => targetRotation; set => targetRotation = value; }
    public bool IsInAction { get => isInAction; set => isInAction = value; }
    public int Life { get => life; set => life = value; }

    //UPDATE
    private void FixedUpdate()
    {
        DoForward();
        DoRotation();
        Collectable c = isOnCollectable();
        if (c != null)
        {
            activateCollectable(c);
        }
        FinishAction();
    }

    //UPDATE Methode
    protected void FinishAction()
    {
        if (transform.position == targetPosition && transform.rotation == targetRotation && IsInAction && b == null)
        {
            transform.position = TargetPosition;
            transform.rotation = TargetRotation;
            IsInAction = false;
            
        }
        if (b != null && b.transform.position == b.TargetPosition && transform.rotation == targetRotation && IsInAction)
        {


            b.activeParticule();
            Destroy(b.gameObject, 0.3f);
            IsInAction = false;
            

        }
    }
    protected void DoRotation()
    {
        if (transform.rotation != targetRotation)
        {
            transform.Rotate(new Vector3(0, rotationDelta * rotationSpeed * Time.deltaTime, 0));
        }
    }
    protected void DoForward()
    {
        if (transform.position != targetPosition)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    //ACTIONS
    protected void Forward()
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
    protected void Rotate()
    {
        IsInAction = true;
        targetRotation = transform.rotation * Quaternion.Euler(0f, 90f * rotationDelta, 0f);
    }
    protected void Shoot()
    {
        IsInAction = true;

        b = Instantiate(bullet);
        b.transform.position = transform.position;
        b.transform.rotation = transform.rotation;
        int range = 10;
        bool isHit = false;

        //Get if Collide
        for (int i = 1; i < range; i++)
        {
            GameObject[] tileList = GameObject.FindGameObjectsWithTag("Collider");
            foreach (GameObject tile in tileList)
            {
                Vector3 tmp = b.transform.position + getFowardVector() * i;

                if (tile.transform.position == b.transform.position + getFowardVector() * i && !isHit)
                {
                    b.TargetPosition = b.transform.position + getFowardVector() * i;
                    isHit = true;
                }
            }


            GameObject[] l = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject e in l)
            {
                Movable tmpe = e.GetComponent<Movable>();
                Vector3 tmp = b.transform.position + getFowardVector() * i;
                if (tmpe.transform.position == b.transform.position + getFowardVector() * i && !isHit)
                {
                    b.TargetPosition = b.transform.position + getFowardVector() * i;
                    isHit = true;
                    tmpe.setLife(-1);
                }
            }

        }
        if (!isHit)
        {
            b.TargetPosition = b.transform.position + getFowardVector() * 10;
        }

    }
    private void activateCollectable(Collectable c)
    {
        Debug.Log("Activate Collectable : " + c);
        setLife(5);
        Destroy(c.gameObject);
    }

    private Collectable isOnCollectable()
    {
        var lb = GameObject.FindGameObjectsWithTag("Collectable");
        foreach(GameObject go in lb)
        { 
            if(go.transform.position == transform.position)
            {
                return go.GetComponent<Collectable>();
            }
        }
        return null;
        
    }

    

   

    //METHODE
    protected bool isCollide(Vector3 f)
    {
        if (f.x < GetComponentInParent<TileMap>().MapSize.x && 0 <= f.x && 0 <= f.z && f.z < GetComponentInParent<TileMap>().MapSize.y)
        {
            var tileList = GameObject.FindGameObjectsWithTag("Collider");
            foreach (GameObject tile in tileList)
            {
                if (tile.transform.position == f)
                {
                    return true;
                }
            }

            var playerList = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in playerList)
            {
                Movable pe = player.GetComponent<Movable>();
                if (pe.TargetPosition == f)
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }

    public virtual void setLife(int v)
    {
        Life += v;
    }

}
