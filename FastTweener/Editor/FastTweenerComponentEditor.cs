using UnityEditor;

namespace Kovnir.FastTweener
{
    [CustomEditor(typeof(FastTweenerComponent))]
    public class FastTweenerComponentEditor : Editor
    {
        void OnEnable() { EditorApplication.update += Update; }
        void OnDisable() { EditorApplication.update -= Update; }
        void Update()
        {
            Repaint();
        }

        public override void OnInspectorGUI()
        {
            bool inited;
            int tasksInPool;
            int activeTask;
            FastTweenerComponent.GetEditorData(out inited, out tasksInPool, out activeTask);
            if (!inited)
            {                
                EditorGUILayout.LabelField("Not inited yet.");
                return;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Tasks:", EditorStyles.boldLabel);
            ShowStats(activeTask, tasksInPool);
        }

        private static void ShowStats(int active, int inPool)
        {
            EditorGUILayout.LabelField("Active", active.ToString());
            EditorGUILayout.LabelField("InPool", inPool.ToString());
        }
    }
}