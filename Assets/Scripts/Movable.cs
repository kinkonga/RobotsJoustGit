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
    

    protected Projectiles bullet;
    protected List<Projectiles> bullets = new List<Projectiles>();
    [SerializeField] ParticleSystem pSmoke;
    [SerializeField] protected ParticleSystem pEnergyBoost;
    protected Animator animator;

    //STATE
    float startWait = 0;
    float targetWait = 0;
    bool isWait = false;
    bool isInAction = false;

    

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
        if (isWait)
        {
            if(Time.realtimeSinceStartup >= targetWait)
            {
                isWait = false;
            }
        }

        if (IsArrived() && IsRotFinish() && IsInAction && bullets.Count == 0 && !isWait)
        {
            transform.position = TargetPosition;
            transform.rotation = TargetRotation;
            IsInAction = false;
            pSmoke.Stop();
            animator.SetBool("idle", true);
        }
        if(bullets.Count != 0)
        {
            Debug.Log(bullets.Count);
            for(int i = 0; i < bullets.Count;i++)
            {
                if (bullets[i].transform.position == bullets[i].TargetPosition)
                {
                    bullets[i].activeParticule();
                    
                    Destroy(bullets[i].gameObject, 0.4f);
                    bullets.RemoveAt(i);
                }
            }
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
        f = transform.position + GetNormalVector(1);

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
    protected virtual void Shoot()
    {
        IsInAction = true;

        bullet = Instantiate(bullet);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        int range = 10;
        int damage = 2;
       
        if (!GetIfCollide(range,damage, GetNormalVector(1)))
        {
            bullet.TargetPosition = bullet.transform.position + GetNormalVector(1) * range;
        }
        
    }

    protected bool GetIfCollide(int range,int damage,Vector3 vDelta)
    {
        bool h = false;
        //Get if Collide

        for (int i = 1; i < range; i++)
        {
            GameObject[] tileList = GameObject.FindGameObjectsWithTag("Collider");
            foreach (GameObject tile in tileList)
            {
                Vector3 tmp = bullet.transform.position + vDelta * i;

                if (tile.transform.position == bullet.transform.position + vDelta * i && !h)
                {
                    bullet.TargetPosition = bullet.transform.position + vDelta * i;
                    h = true;
                }
            }


            GameObject[] l = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject e in l)
            {
                Movable tmpe = e.GetComponent<Movable>();
                Vector3 tmp = bullet.transform.position + vDelta * i;
                if (tmpe.transform.position == bullet.transform.position + vDelta * i && !h)
                {
                    bullet.TargetPosition = bullet.transform.position + vDelta * i;
                    h = true;
                    tmpe.setLife(-damage);
                }
            }

        }

        return h;
    }

    protected void StopRecharge()
    {
        IsInAction = true;
        setEnergy(1);
        pEnergyBoost.Play();
        WaitToFinishAction(0.5f);
    }
    protected virtual void Switch()
    {
        IsInAction = true;
        Debug.Log("Switch");
        animator.SetTrigger("Switch");
        WaitToFinishAction(1f);
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
    
    protected void WaitToFinishAction(float s)
    {
        
        isWait = true;
        startWait = Time.realtimeSinceStartup;
        targetWait = startWait + s;
    }

}
