using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float thrustStrength = 100f;
    [SerializeField] private float rotationStrength = 100f;
    [SerializeField] private AudioClip mainThrusterAudioClip;
    [SerializeField] private ParticleSystem mainThrusterParticles;
    [SerializeField] private ParticleSystem leftThrusterParticles;
    [SerializeField] private ParticleSystem rightThrusterParticles;

    private AudioSource audioSource;
    private Rigidbody rb;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        HandleThrust();
        HandleRotation();
    }

    private void HandleThrust()
    {
        if (thrust.IsPressed())
        {
            Vector3 force = Vector3.up * thrustStrength * Time.fixedDeltaTime;
            rb.AddRelativeForce(force);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainThrusterAudioClip);
            }

            if (!mainThrusterParticles.isPlaying)
            {
                mainThrusterParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainThrusterParticles.Stop();
        }
    }

    private void HandleRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput != 0)
        {
            float rotationMultiplier = rotationStrength * Time.fixedDeltaTime;
            Vector3 rotationDirection;
            ParticleSystem thrusterParticles;

            // Figure out the rotation direction and which particle system should be used
            if (rotationInput > 0) // Rotating right 
            {
                rotationDirection = Vector3.back;
                thrusterParticles = leftThrusterParticles;
            }
            else // rotating left 
            {
                rotationDirection = Vector3.forward;
                thrusterParticles = rightThrusterParticles;
            }

            rb.freezeRotation = true;
            transform.Rotate(rotationDirection * rotationMultiplier);

            if (!thrusterParticles.isPlaying)
            {
                thrusterParticles.Play();
            }

            rb.freezeRotation = false;
        }
        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }
}
