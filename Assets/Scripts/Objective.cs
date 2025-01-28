using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour {
    [SerializeField] private string objective;
    public UnityEvent onObjectiveComplete;
    public bool isActive;
    public string uniqueID;
    
    public string GetObjective() {
        return objective;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && isActive) {
            CompleteObjective();
        }
    }

    public void CompleteObjective() {
        onObjectiveComplete?.Invoke();
        isActive = false;
    }
}