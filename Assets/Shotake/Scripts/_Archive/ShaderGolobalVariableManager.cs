using UnityCommon;
using UnityEditor;
using UnityEngine;

namespace Shotake
{
    [ExecuteAlways]
    class ShaderGolobalVariableManager : MonobehaviourSingletone<ShaderGolobalVariableManager>
    {
        [SerializeField] bool m_updateConstantlyInEditor;
        
        int PlayerPositionID = -1;

        void ValidateID()
        {
            if (PlayerPositionID == -1)
            {
                PlayerPositionID = Shader.PropertyToID("_PlayerPosition");
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (!m_updateConstantlyInEditor)
            {
                return;
            }
#endif

            //ValidateID();
            //Vector3 v3ppos = GameManager.Instance.Player?.transform.position ?? Vector3.zero;
            //Shader.SetGlobalVector(PlayerPositionID, new UnityEngine.Vector4(v3ppos.x, v3ppos.y, v3ppos.z, 1));
        }
    }
}
