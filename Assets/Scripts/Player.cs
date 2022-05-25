using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float forceMultiplicer = 3f;
    [SerializeField]
    private float maximumVelocity = 3f;
    [SerializeField]
    private ParticleSystem deathParticles;

    public AudioSource somDoAndar;
    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

  
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance == null)
        {
            return;
        }

        var horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude <= maximumVelocity)
            { 
                rb.AddForce(new Vector3(horizontalInput * forceMultiplicer * Time.deltaTime, 0, 0));
                somDoAndar.Play();
            }
    }
    private void OnEnable()
    {
        transform.position = new Vector3(0, 0.2f, 0);
        transform.rotation = Quaternion.Euler(0,-180,0);
        rb.velocity = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameOver();

            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FallDown"))
        {
            GameOver();
        }
    }
    
    private void GameOver()
    {
        GameManager.Instance.GameOver();

        gameObject.SetActive(false);
    }
}
