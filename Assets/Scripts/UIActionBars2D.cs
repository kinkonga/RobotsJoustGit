using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionBars2D : MonoBehaviour
{
    [SerializeField] GameObject bar;
    [SerializeField] int nbrBar = 5;
    [SerializeField] Vector2 position;
    [SerializeField] float gap = 0.2f;
    [SerializeField] Color unuseColor;
    [SerializeField] Color useColor;
    

    GameObject[] listBar;

    public void Awake()
    {
        listBar = new GameObject[nbrBar];
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        for (int i = 0; listBar.Length > i; i++)
        {
            
            listBar[i] = Instantiate(bar);
            listBar[i].transform.parent = transform;
            listBar[i].transform.position = new Vector3(0, -gap * i, 0);
        }
        transform.position = new Vector3(position.x, position.y, 0);
        GetComponent<RectTransform>().localPosition = new Vector3(position.x, position.y, 0);

    }

    public void SetNbrActive(int a)
    {
        for (int i = 0; listBar.Length > i; i++)
        {
            Image c = listBar[i].GetComponent<Image>();
            if (a-1 < i)
            {

                c.color = useColor;
            }
            else
            {
                c.color = unuseColor;
            }
        }
    }
    public void SetAllActive()
    {
        
        for (int i = 0; listBar.Length > i; i++)
        {
            Image c = listBar[i].GetComponent<Image>();
            

            listBar[i].GetComponent<Image>().color = unuseColor;
        }
    }
}
