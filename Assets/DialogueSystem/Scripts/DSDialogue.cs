using DS.ScriptableObjects;
using UnityEngine;

namespace DS {
    public class DSDialogue : MonoBehaviour {
        [SerializeField] private DSDialogueContainerSO dialogueContainer;
        [SerializeField] private DSDialogueGroupSO dialogueGroup;
        [SerializeField] private DSDialogueSO dialogue;
        [SerializeField] private bool groupedDialogues;
        [SerializeField] private bool startingDialoguesOnly;
        [SerializeField] private int selectedDialogueGroupIndex;
        [SerializeField] private int selectedDialogueIndex;
        public string uniqueID;
        
        public DSDialogueContainerSO GetDialogueContainer() {
            return dialogueContainer;
        }
        
        public DSDialogueGroupSO GetDialogueGroup() {
            return dialogueGroup;
        }
        
        public DSDialogueSO GetDialogue() {
            return dialogue;
        }

        public void SetDSDialogueSO(DSDialogueSO dialogue) {
            this.dialogue = dialogue;
        }
        
        public void SetDSDialogueGroupSO(DSDialogueGroupSO dialogueGroup) {
            this.dialogueGroup = dialogueGroup;
        }

        public void SetDSDialogueContainerSO(DSDialogueContainerSO dialogueContainer) {
            this.dialogueContainer = dialogueContainer;
        }
    }
}
