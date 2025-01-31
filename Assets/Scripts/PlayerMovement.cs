using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementDirection;
    private new Rigidbody rigidbody;
    public bool isPlayerMoving = true;
    private Vector3 loadPositionToChange = Vector3.zero;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    void Update() {
        if (loadPositionToChange != Vector3.zero) {
            transform.position = loadPositionToChange;
            loadPositionToChange = Vector3.zero;
            return;
        }
        
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
            rigidbody.velocity = Vector3.zero;
        } else {
            rigidbody.velocity = movementDirection * speed;
        }
    }

    public void Save(ref PlayerSaveData playerSaveData) {
        playerSaveData.position = gameObject.transform.position;
        playerSaveData.isPlayerMoving = isPlayerMoving;
    }

    public void Load(PlayerSaveData playerSaveData) {
        loadPositionToChange = playerSaveData.position;
        isPlayerMoving = playerSaveData.isPlayerMoving;
    }
}