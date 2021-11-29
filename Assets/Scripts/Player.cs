using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Movable
{
    
    int numberOfActions = 5;
    int numberPlanAction = 0;

    TextMeshProUGUI lifeLabel;
    TextMeshPro actionLabel;
    UIActionBars2D actionBars;
    ActionHandler actionHandler;
    ActionPool actionPool;
    [SerializeField] int playerNbr;
    

   
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
        if (NumberOfActions > numberPlanAction)
        {
            actionList.Add(actionPool.GetAction(i));
            actionHandler.AddAction(actionPool.GetAction(i));
            numberPlanAction++;
            actionBars.SetNbrActive(NumberOfActions - numberPlanAction);
        }
    }

    public void DoAction(Action a)
    {
        
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
    }
    private void updateLife()
    {
        if (Life < 0) Life = 0;
        lifeLabel.text = Life.ToString();
    }
    public void setLabels(ref TextMeshProUGUI life, ref UIActionBars2D aBars2D, ref ActionHandler aHandler, ref ActionPool aPool)
    {
        lifeLabel = life;
        actionBars = aBars2D;
        actionHandler = aHandler;
        actionPool = aPool;
    }
    public void ResetLabel()
    {
        numberPlanAction = 0;
        actionList.Clear();
        actionBars.SetAllActive();

    }
    public int NumberOfActions { get => numberOfActions; set => numberOfActions = value; }
}

