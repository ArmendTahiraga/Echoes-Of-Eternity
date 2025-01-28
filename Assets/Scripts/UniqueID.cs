using System;
using UnityEngine;

public class UniqueID : MonoBehaviour {
    [SerializeField] private string uniqueID;
    public string id => uniqueID;

    private void Awake() {
        if (string.IsNullOrEmpty(uniqueID)) {
            uniqueID = Guid.NewGuid().ToString();
        }
    }
    
    #if UNITY_EDITOR
    private void OnValidate() {
        if (string.IsNullOrEmpty(uniqueID)) {
            uniqueID = Guid.NewGuid().ToString();
        }
    }
    #endif
}