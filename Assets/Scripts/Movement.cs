using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction rotation;
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;

    Rigidbody rb;


    private void OnEnable()
    {
        rotation.Enable();
        thrust.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        ProcessRotation();
        ProcessThrust();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRoation(rotationStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRoation(-rotationStrength);
        }
    }

    private void ApplyRoation(float roationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * roationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
    }
}
