using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    string name;
    string shootType;
    int damage;
    int range;
    int cost;
    
    Projectiles ammo;
    Texture2D logo;

    public Weapon(string n, string st, int d, int r, int c, Projectiles a, Texture2D l)
    {
        name = n;
        shootType = st;
        damage = d;
        range = r;
        cost = c;
        ammo = a;
        logo = l;
    }

    public void Print()
    {
        Debug.Log(name + " /t: " + shootType + " /d: " + damage + " /r: "+range+ " /c: " + cost + " /a: " + ammo.transform.name + " /t: " + logo);
    }


    //GETTER SETTER
    public string Name { get => name; set => name = value; }
    public string ShootType { get => shootType; set => shootType = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Cost { get => cost; set => cost = value; }
    public Projectiles Ammo { get => ammo; set => ammo = value; }
    public Texture2D Logo { get => logo; set => logo = value; }
    public int Range { get => range; set => range = value; }
}
