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

    
    //LOGIC
    int numberPlanAction = 0;
    int precostEnergy = 20;

    //STATE
    bool isReady = false;

    //UI REFERENCE
    UiSliderBar lifeSlider;
    UiSliderBar energySlider;
    TextMeshProUGUI energyLabel;
    TextMeshPro actionLabel;
    UIActionBars2D actionBars;
    ActionHandler actionHandler;
    ActionPool actionPool;


    private void Start()
    {

        actionPool.setDefaultAction(this);
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
    public bool IsReady { get => isReady; set => isReady = value; }
}

