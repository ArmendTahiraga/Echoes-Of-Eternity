using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")] public float speed;
    public float deceleration;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        MyInput();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer() {
        if (horizontalInput == 0 && verticalInput == 0) {
            rb.velocity = new Vector3(
                Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.deltaTime),
                rb.velocity.y,
                Mathf.Lerp(rb.velocity.z, 0, deceleration * Time.deltaTime)
            );
        }
        else {
            movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(movementDirection * (speed * 10f), ForceMode.Force);
        }
    }
}