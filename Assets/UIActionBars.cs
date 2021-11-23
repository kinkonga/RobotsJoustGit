using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionBars : MonoBehaviour
{
    [SerializeField] GameObject bar;
    [SerializeField] int nbrBar = 5;
    [SerializeField] Vector2 position;
    [SerializeField] int rotation;
    [SerializeField] float gap = 0.2f;

    GameObject[] listBar;

    public void Awake()
    {
        listBar = new GameObject[nbrBar];
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        for (int i = 0; listBar.Length > i; i++)
        {
            Debug.Log("--------------------------" + listBar.Length + this);
            listBar[i] = Instantiate(bar);
            listBar[i].transform.parent = transform;
            listBar[i].transform.position = new Vector3(0, gap * i, 0);
        }
        transform.position = new Vector3(position.x, 0, position.y);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void SetNbrActive(int a)
    {
        for(int i = 0; listBar.Length > i; i++)
        {
            Debug.Log("a:" + a + " i:" + i);
            if(a-1 < i)
            {
                Debug.Log("listbar:"+i+" disable");
                listBar[i].GetComponentInChildren<MeshRenderer>().enabled = false;
            }
            else
            {
                listBar[i].GetComponentInChildren<MeshRenderer>().enabled = true;
            }
        }
    }
    public void SetAllActive()
    {
        for (int i = 0; listBar.Length > i; i++)
        {
            
            listBar[i].GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }
}
