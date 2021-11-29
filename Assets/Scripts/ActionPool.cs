using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPool : MonoBehaviour
{
    [SerializeField] List<GameObject> actionBox;
    [SerializeField] List<Texture2D> listTextures;
    Action[] pool;
    int poolSize = 5;

    private void Awake()
    {
        pool = new Action[poolSize];
    }

    public void setDefaultAction(Player p)
    {
        pool[0] = new Forward(p);
        pool[1] = new Rotate(p, 1);
        pool[2] = new Rotate(p,-1);
        pool[3] = new Shoot(p);
        pool[4] = new Empty();

        setActionsIcon();
    }

    public Action GetAction(int i)
    {
        if(i < 0 || i >= poolSize)
        {
            Debug.Log("ACTIONPOOL Out of Bound");
            return pool[0];
        }
        return pool[i];
    }

    private void setActionsIcon()
    {
        for(int i = 0; pool.Length > i; i++)
        {
            switch (pool[i].Type)
            {
                case "Forward":
                    actionBox[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = listTextures[0];
                    break;
                case "RotateR":
                    actionBox[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = listTextures[1];
                    break;
                case "RotateL":
                    actionBox[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = listTextures[2];
                    break;
                case "Shoot":
                    actionBox[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = listTextures[3];
                    break;
            }
            
        }
        Debug.Log("OK -> Action Icons");
    }

    public void Print()
    {
        Debug.Log("ACTION POOL : " + pool[0].Player);
        foreach(Action a in pool)
        {
            a.Print();
        }
    }
}