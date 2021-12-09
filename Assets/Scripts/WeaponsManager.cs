using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] List<Texture2D> list_Textures;
    [SerializeField] List<Projectiles> list_Ammos;
    
    List<Weapon> list_Weapons = new List<Weapon>();

    private void Awake()
    {
        
        list_Weapons.Add(new Weapon("Normal", "Normal", 2,10, 2, list_Ammos[0], list_Textures[0]));
        list_Weapons.Add(new Weapon("Lateral", "Lateral", 1,10, 2, list_Ammos[0], list_Textures[1]));

        foreach(Weapon w in list_Weapons)
        {
            w.Print();
        }
    }

    public Weapon getWeapon(int i)
    {
        if (i > list_Weapons.Count)
        {
            Debug.Log("Weapons List Out of Bound");
            return list_Weapons[0];
        }
        return list_Weapons[i];
    }
}
