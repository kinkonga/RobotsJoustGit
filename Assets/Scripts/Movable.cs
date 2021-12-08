using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : Entity
{
    //VARIABLES
    [Header("MOVABLE")]
    [SerializeField] int life = 10;
    int maxLife = 10;
    [SerializeField] int energy = 20;
    int maxEnergy = 20;
    [SerializeField] int numberOfActions = 5;
    public List<Action> actionList = new List<Action>();
    

    [SerializeField] Projectiles bullet;
    [SerializeField] ParticleSystem pSmoke;
    Animator animator;

    //STATE
    bool isInAction = false;

    Projectiles b;

    //GETTER SETTER
    public bool IsInAction { get => isInAction; set => isInAction = value; }
    public int NumberOfActions { get => numberOfActions; set => numberOfActions = value; }
    public int MaxLife { get => maxLife; set => maxLife = value; }
    public int MaxEnergy { get => maxEnergy; set => maxEnergy = value; }
    public int Life { get => life; set => life = value; }
    public int Energy { get => energy; set => energy = value; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("idle", true);

        maxLife = life;
        maxEnergy = energy;
    }

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

    //METHODE
   
    protected void FinishAction()
    {
        animator = GetComponent<Animator>();
        if (IsArrived() && IsRotFinish() && IsInAction && b == null)
        {
            transform.position = TargetPosition;
            transform.rotation = TargetRotation;
            IsInAction = false;
            pSmoke.Stop();
            animator.SetBool("idle", true);
        }
        if (b != null && b.transform.position == b.TargetPosition && transform.rotation == TargetRotation && IsInAction)
        {

            b.activeParticule(); 
            Destroy(b.gameObject, 0.4f);
            IsInAction = false;
            animator.SetBool("idle", true);

        }
    }
    

    //ACTIONS
    protected void Forward()
    {
        animator = GetComponent<Animator>();
        //ANIMATION AND PARTICULES
        pSmoke.Play();
        animator.SetBool("idle", false);

        //CHANGE STATE
        IsInAction = true;
        Vector3 f = new Vector3();

        //RECUPERATION DE LA CASE DEVANT
        f = transform.position + getFowardVector();

        //Collision
        if (!isCollide(f))
        {
            TargetPosition = f;
        }
    }
    protected void Rotate()
    {
        IsInAction = true;
        TargetRotation = transform.rotation * Quaternion.Euler(0f, 90f * RotationDelta, 0f);
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
    protected void StopRecharge()
    {
        setEnergy(1);
    }
    protected void Switch()
    {
        Debug.Log("Switch");
    }
    private void activateCollectable(Collectable c)
    {
        Debug.Log("Activate Collectable : " + c);
        c.Activated(this);
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
    public virtual  void setLife(int v)
    {
        life += v;
        if (life < 0) life = 0;
        if (life > maxLife) life = maxLife;
       
    }
    public virtual void setEnergy(int e)
    {
        energy += e;
        if (Energy < 0) energy = 0;
        if (Energy > maxEnergy) energy = maxEnergy;
    }
    
    protected bool isCollide(Vector3 f)
    {
        TileMap tm = GetComponentInParent<TileMap>();
        
        if (f.x < tm.MapSize.x && 0 <= f.x && 0 <= f.z && f.z < tm.MapSize.y)
        {
            if (!tm.IsPassable(f))
            {
                Debug.Log("Tile :"+ f + "p:" + tm.IsPassable(f));
                return true;
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
    

}
