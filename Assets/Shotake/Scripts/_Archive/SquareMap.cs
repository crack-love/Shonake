//using Codice.CM.WorkspaceServer.Tree.GameUI.Checkin.Updater;
//using UnityCommon;
//using UnityEditor.AI;
//using UnityEngine;

//namespace Shotake
//{
//    class SquareMap : MonoBehaviour
//    {
//        public float m_width = 10;
//        public float m_height = 10;
//        [ReadOnly] public GameObject m_floor;

//        private void OnValidate()
//        {
//            const float YSize = 0.1f;
//            transform.position = new Vector3(0, 0, 0);
//            m_floor.transform.localPosition = new Vector3(0, -YSize/2f, 0);
//            m_floor.transform.localScale = new Vector3(m_width, YSize, m_height);
//            gameObject.isStatic = true;
//            m_floor.isStatic = true;
//        }

//        private void Reset()
//        {
//            if (!m_floor)
//            {
//                GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                o.name = "Floor";
//                o.transform.parent = transform;
//                m_floor = o;
//            }
//        }
//    }
//}
