using DS.Utilities;
using UnityEditor;
using UnityEngine.UIElements;

namespace DS.Windows {
    public class DSEditorWindow : EditorWindow {
        [MenuItem("Window/DS/Dialogue Graph")]
        public static void ShowExample() {
            GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable() {
            AddGraphView();
            AddStyles();
        }

        private void AddGraphView() {
            DSGraphView graphView = new DSGraphView();
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void AddStyles() {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }
    }
}