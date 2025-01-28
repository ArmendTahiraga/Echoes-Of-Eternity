using System;
using UnityEngine;

namespace DS.ScriptableObjects {
    public class DSDialogueGroupSO : ScriptableObject {
        public string groupName;
        public string uniqueID;

        public void Initialize(string groupName) {
            this.groupName = groupName;
            uniqueID = Guid.NewGuid().ToString();
        }
    }
}