using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionParticles : MonoBehaviour
{
    [Header("Stepping")]
    [SerializeField] private ParticleSystem _SteppingParticle;
    [SerializeField] private Transform[] _Feet;

    [Header("Landing")]
    [SerializeField] private ParticleSystem _LandingParticle;

    [Header("Jumping")]
    [SerializeField] private ParticleSystem _JumpingParticle;

    [Header("TarJump")]
    [SerializeField] private ParticleSystem _TarParticle;

    public void TookStep(bool pLeft)
    {
        _SteppingParticle.transform.position = _Feet[pLeft ? 0 : 1].position;
        _SteppingParticle.Play();
    }

    public void Landed()
    {
        _LandingParticle.Play();
    }

    public void Jumped()
    {
        _JumpingParticle.Play();
    }

    public void TarJump()
    {
        _TarParticle.Play();
    }
}
