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
    
    //LOGIC
    int numberPlanAction = 0;
    int precostEnergy = 20;

    //STATE
    bool isReady = false;

    //UI REFERENCE
    TextMeshProUGUI lifeLabel;
    TextMeshProUGUI energyLabel;
    TextMeshPro actionLabel;
    UIActionBars2D actionBars;
    ActionHandler actionHandler;
    ActionPool actionPool;

    //AUTO
    private void Awake()
    {

    }
    private void Start()
    {

        lifeLabel.text = Life.ToString();
        actionPool.setDefaultAction(this);
        actionPool.Print();
        actionBars.SetAllActive();

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

    //METHODE
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
        }
    }
    public override void setLife(int v)
    {
        Life += v;
        if (Life < 0) Life = 0;
        lifeLabel.text = Life.ToString();
        lifeLabel.GetComponent<BumpScale>().activeBump();
    }
    protected void updateLife()
    {
        if (Life < 0) Life = 0;
        lifeLabel.text = Life.ToString();
    }
    protected void updateEnergy()
    {
        if (Energy < 0) Energy = 0;
        energyLabel.text = Energy.ToString();
    }
    public virtual void setEnergy(int e)
    {
        Energy += e;
        if (Energy < 0) Energy = 0;
        energyLabel.text = Energy.ToString();
        energyLabel.GetComponent<BumpScale>().activeBump();
    }
    public void setLabels(ref TextMeshProUGUI life, ref UIActionBars2D aBars2D, ref ActionHandler aHandler, ref ActionPool aPool, ref TextMeshProUGUI e)
    {
        lifeLabel = life;
        energyLabel = e;
        actionBars = aBars2D;
        actionHandler = aHandler;
        actionPool = aPool;
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

