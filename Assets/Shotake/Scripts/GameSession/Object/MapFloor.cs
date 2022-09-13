using UnityEngine;
using UnityCommon;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AI;
#endif

namespace Shotake
{
    // plane map basis floor
    // seamless texture material
    class MapFloor : MonobehaviourSingletone<MapFloor> 
    {
        [SerializeField] GameObject m_plane;
        [SerializeField] Vector2 m_size;

        public Vector2 MapSize => m_size;

        private void Reset()
        {
            if (m_plane) Destroy(m_plane);
            m_plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
            m_plane.name = "Plane";
            m_plane.transform.parent = transform;
            m_plane.isStatic = true;
            m_plane.GetComponent<Renderer>().material = new Material(Shader.Find("Shotake/WorldPlane"));
            SetSize(10, 10);
        }

        void SetSize(float x, float y)
        {
            m_size = new Vector2(x, y);
            transform.localScale = Vector3.one;
            if (m_plane)
            {
                m_plane.transform.localScale = new Vector3(x, 0.1f, y);
            }
        }

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
                    target.SetSize(x, y);
                    EditorUtility.SetDirty(target);
                }

                if (!NavMeshBuilder.isRunning && GUILayout.Button("Build Navmesh (Enigine Shortcut)"))
                {
                    NavMeshBuilder.BuildNavMeshAsync();
                }
            }
        }
#endif
    }
}
