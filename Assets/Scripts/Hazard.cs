using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    Vector3 rotation;
    
    [SerializeField]
    private ParticleSystem breakingEffect;

    private CinemachineImpulseSource cinemachineImpulseSource;
    private Player player;
    public AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindObjectOfType<Player>();

        var xRotation = Random.Range(90f, 180f);
        rotation = new Vector3(-xRotation, 0);
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hazard"))
        {
            
            Destroy(gameObject);
            Instantiate(breakingEffect, transform.position, Quaternion.identity);
            sound.Play();

            if (player != null)
            {
                var distrance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1f / distrance;
               

                cinemachineImpulseSource.GenerateImpulse(force);
            }
            


        }
    }
}
