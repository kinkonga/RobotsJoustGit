using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Movable
{
    
    int numberOfActions = 5;

    TextMeshPro lifeLabel;
    TextMeshPro actionLabel;
    UIActionBars actionBars;

    public List<Action> actionList = new List<Action>();

    private void Start()
    {
        actionLabel.text = " ";
        lifeLabel.text = Life.ToString();
    }

    private void Update()
    {
        updateLife();
    }

    //INPUT HANDLER
    void OnMove()
    {
        if(NumberOfActions > actionList.Count)
        {
            actionList.Add(new Foward());
            actionLabel.text += "M";
            actionBars.SetNbrActive(NumberOfActions - actionList.Count);
        }
        
    }
    void OnRotateR()
    {
        if (NumberOfActions > actionList.Count)
        {
            actionList.Add(new Rotate(1));
            actionLabel.text += "R";
            actionBars.SetNbrActive(NumberOfActions - actionList.Count);
        }     
    }
    void OnRotateL()
    {
        if (NumberOfActions > actionList.Count)
        {
            actionList.Add(new Rotate(-1));
            actionLabel.text += "R";
            actionBars.SetNbrActive(NumberOfActions - actionList.Count);
        }
    }
    void OnShoot()
    {
        if (NumberOfActions > actionList.Count)
        {
            actionList.Add(new Shoot());
            actionLabel.text += "S";
            actionBars.SetNbrActive(NumberOfActions - actionList.Count);
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
    public void setLabels(ref TextMeshPro life, ref TextMeshPro action, ref UIActionBars aBars )
    {
        lifeLabel = life;
        actionLabel = action;
        actionBars = aBars;
    }
    public void ResetLabel()
    {
        actionList.Clear();
        actionBars.SetAllActive();

    }
    public int NumberOfActions { get => numberOfActions; set => numberOfActions = value; }
}

