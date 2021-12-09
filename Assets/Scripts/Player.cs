using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Movable
{


    //VARIABLES
    [Header("PLAYER")]
    [SerializeField] int playerNbr;
    [SerializeField] int refuel = 0;
    [SerializeField] int finalRefuel = 3;

    List<Weapon> list_weapons = new List<Weapon>();
    int equipedWeapon = 0;
    //LOGIC
    int numberPlanAction = 0;
    int precostEnergy = 20;

    //STATE
    bool isReady = false;

    //UI REFERENCE
    UiSliderBar lifeSlider;
    UiSliderBar energySlider;
    UIActionBars2D actionBars;
    ActionHandler actionHandler;
    ActionPool actionPool;


    private void Start()
    {

        actionPool.setDefaultAction(this);
        actionPool.setWeaponIcon(list_weapons[equipedWeapon].Logo);
        actionPool.Print();
        actionBars.SetAllActive();
        lifeSlider.setCurrent(Life);
        lifeSlider.setMax(MaxLife);
        energySlider.setCurrent(Energy);
        energySlider.setMax(MaxEnergy);

    }
    private void Update()
    {
        updateLife();
        updateEnergy();

        if (actionList.Count == NumberOfActions && !isReady)
        {
            isReady = true;
        }
        else if (precostEnergy == 0 && !isReady)
        {
            isReady = true;
        }
    }

    //INPUT HANDLER
    void OnMove()
    {
        DoActionPool(0);
    }
    void OnRotateR()
    {
        DoActionPool(1);
    }
    void OnRotateL()
    {
        DoActionPool(2);
    }
    void OnShoot()
    {
        DoActionPool(3);
    }
    void OnStop()
    {
        DoActionPool(4);
    }
    void OnSwitch()
    {
        DoActionPool(5);
    }

    //ACTION
    protected override void Switch()
    {
        base.Switch();
        equipedWeapon++;
        if(equipedWeapon >= list_weapons.Count)
        {
            equipedWeapon = 0;
        }
        actionPool.setWeaponIcon(list_weapons[equipedWeapon].Logo);
    }
    protected override void Shoot()
    {
        Debug.Log("PlayerShoot");
        IsInAction = true;
        string type = list_weapons[equipedWeapon].ShootType;
        int range = list_weapons[equipedWeapon].Range;
        int damage = list_weapons[equipedWeapon].Damage;
        Debug.Log(" type : " + type);
        if(type == "Normal")
        {
            bullet = Instantiate(getEquipedWeapon().Ammo);
            bullets.Add(bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            if (!GetIfCollide(range, damage, GetNormalVector(1)))
            {
                bullet.TargetPosition = bullet.transform.position + GetNormalVector(1) * range;
            }
        }

        if(type == "Lateral")
        {
            bullet = Instantiate(getEquipedWeapon().Ammo);
            bullets.Add(bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
            if (!GetIfCollide(range, damage, GetLateralVector(1)))
            {
                bullet.TargetPosition = bullet.transform.position + GetLateralVector(1) * range;
            }

            bullet = Instantiate(getEquipedWeapon().Ammo);
            bullets.Add(bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, -90, 0);
            if (!GetIfCollide(range, damage, GetLateralVector(-1)))
            {
                bullet.TargetPosition = bullet.transform.position + GetLateralVector(-1) * range;
            }
        }
        

        



    }

    //METHODE
    public void turnRefuel()
    {
        if(Energy == 0)
        {
            setEnergy(finalRefuel);
        }
        else
        {
            setEnergy(refuel);
        }
        

    }
    private void DoActionPool(int i)
    {
       
        precostEnergy -= actionPool.GetAction(i).EnergyCost;
        Debug.Log(precostEnergy);
        if (NumberOfActions > numberPlanAction && precostEnergy >= 0 && !GetComponentInParent<GameManager>().IsPlaying)
        {
            actionList.Add(actionPool.GetAction(i));
            actionHandler.AddAction(actionPool.GetAction(i));
            numberPlanAction++;
            actionBars.SetNbrActive(NumberOfActions - numberPlanAction);
        }
        else
        {
            precostEnergy += actionPool.GetAction(i).EnergyCost;
        }
    }
    public void DoAction(Action a)
    {
        
        setEnergy(-a.EnergyCost);
   
        switch (a.Type)
        {
            case "Forward":
                Forward();
                break;
            case "RotateR":
                RotationDelta = a.RotationDelta;
                Rotate();
                break;
            case "RotateL":
                RotationDelta = a.RotationDelta;
                Rotate();
                break;
            case "Shoot":
                Shoot();
                break;
            case "StopEnergy":
                StopRecharge();
                break;
            case "Switch":
                Switch();
                break;
        }
    }
    public override void setLife(int v)
    {
        Life += v;
        updateLife();

        
    }
    public override void setEnergy(int e)
    {
        Energy += e;
        updateEnergy();

        
    }
    protected void updateLife()
    {
        if (Life < 0) Life = 0;
        if (Life > MaxLife) Life = MaxLife;

        lifeSlider.setCurrent(Life);
        
    }
    protected void updateEnergy()
    {
        if (Energy < 0) Energy = 0;
        if (Energy > MaxEnergy) Energy = MaxEnergy;

        energySlider.setCurrent(Energy);
    }
    public void setLabels(ref UIActionBars2D aBars2D, ref ActionHandler aHandler, ref ActionPool aPool,ref UiSliderBar l, ref UiSliderBar e)
    {
        energySlider = e;
        actionBars = aBars2D;
        actionHandler = aHandler;
        actionPool = aPool;
        lifeSlider = l;
    }
    public void ResetLabel()
    {
        numberPlanAction = 0;
        actionList.Clear();
        actionBars.SetAllActive();
        precostEnergy = Energy;
        isReady = false;

    }
    public void giveWeapon(Weapon w)
    {
        list_weapons.Add(w);
    }
    public bool IsReady { get => isReady; set => isReady = value; }
    public void Print()
    {
        Debug.Log("- " + this + " -");
        Debug.Log("Nbr Weapon : "+list_weapons.Count+" / Equiped : "+list_weapons[equipedWeapon].Name);
        
    }
    public Weapon getEquipedWeapon()
    {
        return list_weapons[equipedWeapon];
    }
}

