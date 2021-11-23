using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour
{
    
    



    //UPDATE
    protected void rotate(float speed)
    {
        transform.Rotate(new Vector3(0, 10*speed*Time.deltaTime, 0));
    }
    protected Vector3 getFowardVector()
    {
        
        switch (Mathf.Round(transform.rotation.eulerAngles.y))
        {
            case 0:
                return new Vector3(0, 0, 1);
            case 90:
                return new Vector3(1, 0, 0);
            case 180:
                return new Vector3(0, 0, -1);
            case 270:
                return new Vector3(-1, 0, 0);
            default:
                Debug.Log(this + " no orientation");
                return new Vector3(0, 0, 0);
        }
    }
    
}
