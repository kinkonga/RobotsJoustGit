using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTile : MonoBehaviour
{
    int id;
    [SerializeField] int respawnTime = 3;
    [SerializeField] Collectable bonus;
    int destroyTurn = 0;
    bool isFull = false;
    
    /*
    0 = Health
    1 = Energy
    */

    public void Spawn(int turn)
    {
        var tmpBonus = GetComponentInParent<TileMap>();
        var ins = Instantiate(bonus);
        ins.transform.parent = transform;
        ins.transform.position = transform.position;
        isFull = true;
    }
   
    public void UpdateState(int turn)
    {
        var tmpBonus = GetComponentInParent<TileMap>();
        if (isFull)
        {
            var cc = this.transform.childCount;
            Debug.Log("obj : " + cc);
            if (cc == 1)
            {
                isFull = false;
                destroyTurn = turn;
            }
        }
        if (!isFull)
        {
            if(destroyTurn == turn - respawnTime)
            {
                var ins = Instantiate(bonus);
                ins.transform.parent = transform;
                ins.transform.position = transform.position;
                isFull = true;
            } 

        }    
    }
}
