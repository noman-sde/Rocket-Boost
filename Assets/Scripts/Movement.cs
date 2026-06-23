using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thurstStrength = 1f;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrength = 1f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
      rb.AddRelativeForce(Vector3.up * thurstStrength * Time.fixedDeltaTime);
    }
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
      ApplyRotation(rotationStrength);
    }
    else if (rotationInput > 0)
    {
      ApplyRotation(-rotationStrength);
    }
  }
  private void ApplyRotation(float rotationThisFrame)
  {
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
  }
}
