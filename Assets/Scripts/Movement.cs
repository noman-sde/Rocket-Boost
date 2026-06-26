using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thurstStrength = 1f;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrength = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            startThrusting();
        }
        else
        {
            stopThrusting();
        }
    }

    private void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thurstStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void stopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        // if (rotationInput != 0)
        // {
        //     rb.AddRelativeTorque(Vector3.back * rotationInput * rotationStrength * Time.fixedDeltaTime);
        // }

        if (rotationInput < 0)
        {
            rotateRight();
        }
        else if (rotationInput > 0)
        {
            rotateLeft();
        }
        else
        {
            stopRotating();
        }
    }
    private void rotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }
    private void rotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }
    private void stopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
