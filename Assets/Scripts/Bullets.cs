using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : Movable
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
