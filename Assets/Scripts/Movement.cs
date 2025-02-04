using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] InputAction rotation;
    [SerializeField] InputAction thrust;
    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;


    Rigidbody rb;
    AudioSource audioSource;


    private void OnEnable()
    {
        rotation.Enable();
        thrust.Enable();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            ApplyRotation(rotationStrength);
            if (!rightParticles.isPlaying)
                rightParticles.Play();
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if (!leftParticles.isPlaying)
                leftParticles.Play();
        }
        else
        {
            leftParticles.Stop();
            rightParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
            startThrusting();
        else
        {
            stopThrusting();
        }
    }

    private void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);

        if (!mainParticles.isPlaying)
            mainParticles.Play();
    }
    private void stopThrusting()
    {
        audioSource.Stop();
        mainParticles.Stop();
    }
}
