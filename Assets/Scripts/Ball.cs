using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Entity
{
    [SerializeField] ParticleSystem explose;


    public void activeParticule()
    {
        explose.Play();
    }

    

}
