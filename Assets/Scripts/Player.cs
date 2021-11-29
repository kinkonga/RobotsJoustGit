using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Movable
{
    
    int numberOfActions = 5;
    int numberPlanAction = 0;

    int energy = 20;
    int precostEnergy = 20;

    TextMeshProUGUI lifeLabel;
    TextMeshProUGUI energyLabel;
    TextMeshPro actionLabel;
    UIActionBars2D actionBars;
    ActionHandler actionHandler;
    ActionPool actionPool;
    [SerializeField] int playerNbr;
    
    bool isReady = false;
   
    public List<Action> actionList = new List<Action>();


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

        if (actionList.Count == numberOfActions)
        {
            isReady = true;
        }
        else if (precostEnergy == 0)
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
    public void setEnergy(int e)
    {
        Energy += e;
        if (Energy < 0) Energy = 0;
        energyLabel.text = Energy.ToString();
        energyLabel.GetComponent<BumpScale>().activeBump();
    }
    private void updateLife()
    {
        if (Life < 0) Life = 0;
        lifeLabel.text = Life.ToString();
    }
    private void updateEnergy()
    {
        if (Energy < 0) Energy = 0;
        energyLabel.text = Energy.ToString();
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
        precostEnergy = energy;
        isReady = false;

    }
    public int NumberOfActions { get => numberOfActions; set => numberOfActions = value; }
    public int Energy { get => energy; set => energy = value; }
    public bool IsReady { get => isReady; set => isReady = value; }
}

