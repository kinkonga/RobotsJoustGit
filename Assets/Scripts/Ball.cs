using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Entity
{
    [SerializeField] ParticleSystem explose;

    private void Update()
    {
        DoForward();
    }
    public void activeParticule()
    {
        explose.Play();
    }

    

}
