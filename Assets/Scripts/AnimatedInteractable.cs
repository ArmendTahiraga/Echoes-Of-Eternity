using UnityEngine;

public class AnimatedInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private string[] animationTriggers;
    [SerializeField] private Animator[] animators;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    public void Interact() {
        for (int i = 0; i < animationTriggers.Length; i++) {
            animators[i].SetTrigger(animationTriggers[i]);
        }

        foreach (AudioClip audioClip in audioClips) {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}