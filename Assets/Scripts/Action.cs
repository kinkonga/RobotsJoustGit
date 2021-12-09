using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    // - GENERAL -
    Player player;
    string type; // Type d'action
    [SerializeField] List<GameObject> actionIcons; //list des Icones

    int energyCost;
    
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
        Debug.Log("Player : " + player + " / Action : " + Type + " / EnergyCost : "+ EnergyCost);
    }

    public string Type { get => type; set => type = value; }
    public float RotationDelta { get => rotationDelta; set => rotationDelta = value; }
    public Player Player { get => player; set => player = value; }
    public int EnergyCost { get => energyCost; set => energyCost = value; }
}

public class Forward : Action
{
    public Forward()
    {
        Type = "Forward";
        EnergyCost = 1;
    }
    public Forward(Player p)
    {
        Type = "Forward";
        EnergyCost = 1;
        Player = p;
    }
}

public class Rotate : Action
{
    public Rotate(float r)
    {
        Type = "Rotate";
        EnergyCost = 1;
        RotationDelta = r;
    }
    public Rotate(Player p, float r)
    {
        
        RotationDelta = r;
        Player = p;
        EnergyCost = 1;

        if (RotationDelta == 1)
            Type = "RotateR";
        else
            Type = "RotateL";
    }
    public override void Print()
    {
        Debug.Log("Player : " + Player + " / Action : " + Type + " / Delta : " + RotationDelta + " / EnergyCost : " + EnergyCost);
    }
}

public class Shoot : Action
{

    public Shoot()
    {
        Type = "Shoot";
        EnergyCost = 2;
    }
    public Shoot(Player p)
    {
        Type = "Shoot";
        Player = p;
        EnergyCost = p.getEquipedWeapon().Cost;
        
    }
}
public class StopEnergy : Action
{
    public StopEnergy()
    {
        Type = "StopEnergy";
        EnergyCost = 0;
    }
    public StopEnergy(Player p)
    {
        Type = "StopEnergy";
        Player = p;
        EnergyCost = 0;
    }
}

public class Switch : Action
{

    public Switch()
    {
        Type = "Switch";
    }
    public Switch(Player p)
    {
        Type = "Switch";
        Player = p;
        EnergyCost = 1;
    }
}



