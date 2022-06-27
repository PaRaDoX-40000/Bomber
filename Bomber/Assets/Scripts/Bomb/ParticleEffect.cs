using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;

    public void Play()
    {
        foreach(ParticleSystem particleSystem in _particleSystems)
        {
            particleSystem.Play();
        }
    }
}
