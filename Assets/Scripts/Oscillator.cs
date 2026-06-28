using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed = 2f;
    float movementFactor;
    Vector3 startPosition;
    Vector3 endPosition;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }

}
