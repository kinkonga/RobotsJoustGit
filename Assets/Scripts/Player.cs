using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Entity
{
    TextMeshPro lifeLabel;
    TextMeshPro actionLabel;


    private void Start()
    {
        actionLabel.text = " ";
        lifeLabel.text = Life.ToString();
    }
    private void FixedUpdate()
    {
        DoForward();
        DoRotation();
        FinishAction();
    }


    void OnMove()
    {
        if(NumberOfActions > actionList.Count)
        {
            actionList.Add(new Foward());
            actionLabel.text += "M";
        }
        
    }
    void OnRotateR()
    {
        if (NumberOfActions > actionList.Count)
        {
            actionList.Add(new Rotate(1));
            actionLabel.text += "R";
        }     
    }
    void OnRotateL()
    {
        if (NumberOfActions > actionList.Count)
        {
            actionList.Add(new Rotate(-1));
            actionLabel.text += "R";
        }
    }
    void OnShoot()
    {
        if (NumberOfActions > actionList.Count)
        {
            actionList.Add(new Shoot());
            actionLabel.text += "S";
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
    public void setLabels(ref TextMeshPro Life, ref TextMeshPro Action)
    {
        lifeLabel = Life;
        actionLabel = Action;
    }

}

