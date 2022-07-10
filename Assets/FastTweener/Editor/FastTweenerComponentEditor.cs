using UnityEditor;
using Kovnir.FastTweener;

namespace Kovnir.Editor.FastTweener
{
    [CustomEditor(typeof(FastTweenerComponent))]
    public class FastTweenerComponentEditor : UnityEditor.Editor
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
            int aliveTask;
            FastTweenerComponent.GetEditorData(out inited, out tasksInPool, out aliveTask);
            if (!inited)
            {                
                EditorGUILayout.LabelField("Not inited yet.");
                return;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Tasks:", EditorStyles.boldLabel);
            ShowStats(aliveTask, tasksInPool);
        }

        private static void ShowStats(int Alive, int inPool)
        {
            EditorGUILayout.LabelField("Alive", Alive.ToString());
            EditorGUILayout.LabelField("InPool", inPool.ToString());
        }
    }
}