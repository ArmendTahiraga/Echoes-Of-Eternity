using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementDirection;
    private Rigidbody rigidbody;
    public bool isPlayerMoving = true;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    void Update() {
        if (!isPlayerMoving) {
            return;
        }
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (movementDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (movementDirection == Vector3.zero) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        } else {
            GetComponent<Rigidbody>().velocity = movementDirection * speed;
        }
    }
}