using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Entity
{
    [Header("COLLECTABLE")]
    [SerializeField] bool isRotate = false;
    [SerializeField] bool heal = false;
    [SerializeField] int healAmount = 5;
    [SerializeField] bool energy = false;
    [SerializeField] int energyAmount = 10;


    void Update()
    {
        if (isRotate)
        DoRotation();
    }

    virtual public void Activated(Movable p)
    {
        if (heal)
        {
            p.setLife(healAmount);
        }
        if (energy)
        {
            p.setEnergy(energyAmount);
        }
    }
}
