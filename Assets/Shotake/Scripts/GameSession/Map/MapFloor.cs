using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AI;
#endif

namespace Shotake
{
    // plane map basis floor
    // seamless texture material
    class MapFloor : MonoBehaviour
    {
        [SerializeField] GameObject m_plane;
        [SerializeField] Vector2 m_size;

#if UNITY_EDITOR
        [CustomEditor(typeof(MapFloor))]
        class MapFloorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                var target = base.target as MapFloor;
                if (!target) return;

                EditorGUI.BeginChangeCheck();
                target.m_plane = EditorGUILayout.ObjectField("Plane", target.m_plane, typeof(GameObject), true) as GameObject;
                float x = EditorGUILayout.DelayedFloatField("Size X", target.m_size.x);
                float y = EditorGUILayout.DelayedFloatField("Size Y", target.m_size.y);
                if (EditorGUI.EndChangeCheck())
                {
                    target.m_size = new Vector2(x, y);
                    target.transform.localScale = Vector3.one;
                    target.m_plane.transform.localScale = new Vector3(x, 0.1f, y);
                }

                if (!NavMeshBuilder.isRunning && GUILayout.Button("Build Navmesh"))
                {
                    NavMeshBuilder.BuildNavMeshAsync();
                }

                EditorUtility.SetDirty(target);
            }
        }
#endif
    }
}
