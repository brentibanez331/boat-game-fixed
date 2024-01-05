using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    ParticleSystem explosion;
    MeshRenderer bomb;

    private void Awake()
    {
        explosion = gameObject.GetComponentInChildren<ParticleSystem>();
        bomb = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    public void Explode()
    {
        explosion.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bomb.enabled = false;
            Explode();
            Destroy(other.gameObject);
            Destroy(gameObject, 3.0f);
        }
    }
}