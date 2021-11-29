using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    List<Action> actionList = new List<Action>();

    public void AddAction(Action a)
    {
        actionList.Add(a);
    }

    public Action GetAction(int i)
    {
        if(i < 0 || i >= actionList.Count)
        {
            Debug.Log("ActionHandler Out Of Bound");
            return actionList[0];
        }
        return actionList[i];
    }

    public void ClearAction()
    {
        actionList.Clear();
    }

    public int Size()
    {
        return actionList.Count;
    }

    public void Print()
    {
        Debug.Log("--- ACTION HANLDER ---");
        Debug.Log("Nbr Actions : " + actionList.Count + " / Action :");
        foreach (Action a in actionList)
        {
            a.Print();
        }
        Debug.Log("--- -------------- ---");
    }


}
