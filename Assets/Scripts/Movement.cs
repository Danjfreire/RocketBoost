using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float thrustStrength = 100f;
    [SerializeField] private float rotationStrength = 100f;

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
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void HandleRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput != 0)
        {
            float rotationMultiplier = rotationStrength * Time.fixedDeltaTime;
            Vector3 rotationDirection = rotationInput > 0 ? Vector3.back : Vector3.forward;

            rb.freezeRotation = true;
            transform.Rotate(rotationDirection * rotationMultiplier);
            rb.freezeRotation = false;
        }
    }
}
