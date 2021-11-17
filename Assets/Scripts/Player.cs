using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{

    void OnMove()
    {
        Debug.Log(this + "Add MOVE");
    }
    void OnRotate()
    {
        Debug.Log(this + "Add ROTATE");
    }
    void OnShoot()
    {
        Debug.Log(this + "Add SHOOT");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

