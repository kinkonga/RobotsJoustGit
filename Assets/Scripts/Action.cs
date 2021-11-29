using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    // - GENERAL -
    Player player;
    string type; // Type d'action
    [SerializeField] List<GameObject> actionIcons; //list des Icones
    // Cout en energie ?
    
    // - MOUVEMENT -
    float rotationDelta;

    // - SHOOT -
    //List<Weapon> ListWeapons = new List<Weapon>();
    //Definir vecteur de tir
    //Definir range de tir
    //Définir degat de tir


    // - SPECIAL -

         


    public Action()
    {
        type = "no type";
    }
    public Action(int player,string type)
    {
        this.type = type;
    }

    public virtual void Print()
    {
        Debug.Log("Player : " + player + " / Action : " + Type);
    }

    public string Type { get => type; set => type = value; }
    public float RotationDelta { get => rotationDelta; set => rotationDelta = value; }
    public Player Player { get => player; set => player = value; }
}

public class Forward : Action
{
    public Forward()
    {
        Type = "Forward";
    }
    public Forward(Player p)
    {
        Type = "Forward";
        Player = p;
    }
}

public class Rotate : Action
{
    public Rotate(float r)
    {
        Type = "Rotate";
        RotationDelta = r;
    }
    public Rotate(Player p, float r)
    {
        
        RotationDelta = r;
        Player = p;

        if (RotationDelta == 1)
            Type = "RotateR";
        else
            Type = "RotateL";
    }
    public override void Print()
    {
        Debug.Log("Player : " + Player + " / Action : " + Type + " / Delta : " + RotationDelta);
    }
}

public class Shoot : Action
{

    public Shoot()
    {
        Type = "Shoot";
    }
    public Shoot(Player p)
    {
        Type = "Shoot";
        Player = p;
    }
}
public class Empty : Action
{

    public Empty()
    {
        Type = "Empty";
    }
}



