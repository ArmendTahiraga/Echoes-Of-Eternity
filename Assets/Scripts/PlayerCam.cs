using UnityEngine;

public class PlayerCam : MonoBehaviour {
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObjectOrientation;
    private float xRotation;
    private float yRotation;
    public bool lockCamera;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (PauseMenuController.isGamePaused) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (!lockCamera) {
                float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        
                xRotation -= mouseY;
                yRotation += mouseX;
                xRotation = Mathf.Clamp(xRotation, -90f, 45f);
        
                transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
                orientation.rotation = Quaternion.Euler(0, yRotation, 0);
                playerObjectOrientation.rotation = Quaternion.Euler(0, yRotation, 0);
            }
        }
    }

    public void Save(ref PlayerCamSaveData playerCamSaveData) {
        playerCamSaveData.lockCamera = lockCamera;
    }
    
    public void Load(PlayerCamSaveData playerCamSaveData) {
        lockCamera = playerCamSaveData.lockCamera;
    }
}