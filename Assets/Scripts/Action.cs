using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    string type;
    float rotationDelta;

    public Action()
    {
        type = "no type";
    }
    public Action(string t)
    {
        type = t;
    }

    public string Type { get => type; set => type = value; }
    public float RotationDelta { get => rotationDelta; set => rotationDelta = value; }
}

public class Foward : Action
{
    public Foward()
    {
        Type = "Forward";
    }
}

public class Rotate : Action
{
    public Rotate(float r)
    {
        Type = "Rotate";
        RotationDelta = r;
    }
}


public class Shoot : Action
{
    public Shoot()
    {
        Type = "Shoot";

    }
}


