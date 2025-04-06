using UnityEngine;

public class CameraHolderController : MonoBehaviour {
    public Transform cameraPosition;

    private void Update() {
        transform.position = cameraPosition.position;
    }
}