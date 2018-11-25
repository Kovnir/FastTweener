using UnityEditor;

namespace Kovnir.Tweener
{
    [CustomEditor(typeof(FastTweenerComponent))]
    public class FastTweenerComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            bool inited;
            int schedulingTasksInPool;
            int activeSchedulingTask;
            int floatTasksInPool;
            int activeFloatTasks;
            FastTweenerComponent.GetEditorData(out inited, out schedulingTasksInPool, out activeSchedulingTask, out floatTasksInPool, out activeFloatTasks);
            if (!inited)
            {                
                EditorGUILayout.LabelField("Not inited yet.");
                return;
            }
            EditorGUILayout.LabelField("Scheduling", EditorStyles.boldLabel);
            ShowStats(activeSchedulingTask, schedulingTasksInPool);
            
            EditorGUILayout.LabelField("Float", EditorStyles.boldLabel);
            ShowStats(activeFloatTasks, floatTasksInPool);
        }

        private static void ShowStats(int active, int inPool)
        {
            EditorGUILayout.LabelField("Active", active.ToString());
            EditorGUILayout.LabelField("InPool", inPool.ToString());
        }
    }
}