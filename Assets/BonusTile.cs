using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTile : MonoBehaviour
{
    int respawnTime = 3;
    int destroyTurn = 0;
    bool isFull = false;

    public void Spawn(int turn)
    {
        var tmpBonus = GetComponentInParent<TileMap>();
        var ins = Instantiate(tmpBonus.GetBonus(Random.Range(0, tmpBonus.Bonus.Count)));
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
                var ins = Instantiate(tmpBonus.GetBonus(Random.Range(0, tmpBonus.Bonus.Count)));
                ins.transform.parent = transform;
                ins.transform.position = transform.position;
                isFull = true;
            } 

        }    
    }
}
