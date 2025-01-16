using System;
using System.Collections;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObject;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform orientation;
    [SerializeField] private PlayerCam playerCamScript;
    [SerializeField] private string animationTrigger;
    [SerializeField] private Animator animator;
    private Vector3 previousPlayerPosition;
    private Quaternion previousPlayerRotation;
    private Vector3 previousPlayerObjectPosition;
    private Vector3 previousCameraPositionPosition;
    private Quaternion previousPlayerCamRotation;
    private Quaternion previousOrientationRotation;

    private void Update() {
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().isPlayerMoving && previousPlayerPosition != new Vector3(0 ,0, 0)) {
            MovePlayerBack();
            if (animationTrigger == "PlayerNotSitTrigger") {
                animator.SetTrigger(animationTrigger);
            }
        }
    }

    public void MovePlayer(Vector3 playerPosition, Quaternion playerRotation, Vector3 playerObjectPosition,
        Vector3 cameraPositionPosition,
        Quaternion playerCamRotation, Quaternion orientationRotation) {
        previousPlayerPosition = player.position;
        previousPlayerRotation = player.rotation;
        previousPlayerObjectPosition = playerObject.localPosition;
        previousCameraPositionPosition = cameraPosition.localPosition;
        previousPlayerCamRotation = playerCam.rotation;
        previousOrientationRotation = orientation.rotation;

        player.position = playerPosition;
        player.rotation = playerRotation;
        playerObject.localPosition = playerObjectPosition;
        cameraPosition.localPosition = cameraPositionPosition;
        playerCam.rotation = playerCamRotation;
        orientation.rotation = orientationRotation;
        // playerCamScript.lockCamera = true;
        // StartCoroutine(UnlockCam());
    }

    // private IEnumerator UnlockCam() {
    //     yield return new WaitForSeconds(1f);
    //     playerCamScript.lockCamera = false;
    // }

    private void MovePlayerBack() {
        player.position = previousPlayerPosition;
        player.rotation = previousPlayerRotation;
        playerObject.localPosition = previousPlayerObjectPosition;
        cameraPosition.localPosition = previousCameraPositionPosition;
        playerCam.rotation = previousPlayerCamRotation;
        orientation.rotation = previousOrientationRotation;
        
        previousPlayerPosition = Vector3.zero;
        previousPlayerRotation = Quaternion.identity;
        previousPlayerObjectPosition = Vector3.zero;
        previousCameraPositionPosition = Vector3.zero;
        previousPlayerCamRotation = Quaternion.identity;
        previousOrientationRotation = Quaternion.identity;
    }
}