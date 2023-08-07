using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private ParticleSystem _winParticles;

    public void PlayExplosionParticles(Vector3 position)
    {
        _explosionParticles.Play();
    }

    public void PlayWinParticles(Vector3 position)
    {
        _winParticles.Play();
    }
}
