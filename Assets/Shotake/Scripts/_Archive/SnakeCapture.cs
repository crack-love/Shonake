//using System.Collections.Generic;
//using UnityEngine;

//namespace Shotake
//{
//    static class SnakeCapturesNodePool
//    {
//        static List<LinkedListNode<SnakeCapture>> m_list = new List<LinkedListNode<SnakeCapture>>();

//        public static LinkedListNode<SnakeCapture> Get()
//        {
//            if (m_list.Count <= 0)
//            {
//                var ret = new LinkedListNode<SnakeCapture>(new SnakeCapture());
//                return ret;
//            }
//            else
//            {
//                var ret = m_list[m_list.Count - 1];
//                m_list.RemoveAt(m_list.Count - 1);
//                return ret;
//            }
//        }

//        public static void Return(LinkedListNode<SnakeCapture> node)
//        {
//            node.Value.Position = default;
//            node.Value.Rotation = default;
//            node.Value.Finished = default;

//            m_list.Add(node);
//        }
//    }

//    class SnakeCaptures : LinkedList<SnakeCapture>
//    {
//    }

//    class SnakeCapture
//    {
//        public Vector3 Position;
//        public Vector3 Rotation;
//        public bool Finished;
//    }
//}
