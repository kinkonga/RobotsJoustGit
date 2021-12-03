using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpScale : MonoBehaviour
{
    [SerializeField] float bumpSize;
    [SerializeField] float bumpSpeed;

    void FixedUpdate()
    {
        float speed = bumpSpeed * Time.deltaTime;
        
       if(transform.localScale.x > 1)
        {
            transform.localScale -= new Vector3(speed,speed,0) ;
        }
       if(transform.localScale.x < 1)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        
    }

    public void activeBump()
    {
        transform.localScale = new Vector3(bumpSize, bumpSize, 0);
    }
}
