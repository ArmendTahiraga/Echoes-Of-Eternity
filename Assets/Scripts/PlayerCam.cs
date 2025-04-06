using UnityEngine;

public class PlayerCam : MonoBehaviour {
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObjectOrientation;
    private float xRotation;
    private float yRotation;
    public bool lockCamera;
    public bool enableCursor;

    private void Start() {
        ConfigureShowCursor(false);
    }

    private void Update() {
        if (enableCursor) {
            ConfigureShowCursor(true);

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
        } else if (PauseMenuController.isGamePaused) {
            ConfigureShowCursor(true);
        } else {
            ConfigureShowCursor(false);

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

    private void ConfigureShowCursor(bool showCursor) {
        if (showCursor) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Save(ref PlayerCamSaveData playerCamSaveData) {
        playerCamSaveData.lockCamera = lockCamera;
    }

    public void Load(PlayerCamSaveData playerCamSaveData) {
        lockCamera = playerCamSaveData.lockCamera;
    }

    public void SetSensitivity(float x, float y) {
        sensitivityX = x;
        sensitivityY = y;
    }

    public float GetSensitivityX() {
        return sensitivityX;
    }

    public float GetSensitivityY() {
        return sensitivityY;
    }
}