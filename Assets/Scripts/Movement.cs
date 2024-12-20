using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private float thrustStrength = 100f;

    private Rigidbody rb;

    private void OnEnable()
    {
        thrust.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (thrust.IsPressed())
        {
            Vector3 force = Vector3.up * thrustStrength * Time.fixedDeltaTime;
            rb.AddRelativeForce(force);
        }
    }
}
