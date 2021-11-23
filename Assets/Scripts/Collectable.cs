using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Entity
{
    [SerializeField] float rotSpeed = 5;
    [SerializeField] bool isRotate = false;
    
    void Update()
    {
        if (isRotate)
            rotate(rotSpeed);
    }
}
