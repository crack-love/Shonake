//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Shotake
//{
//    interface IChainFollowComponent
//    {
//        Transform GetTransform();

//        Transform GetForward();

//        Transform GetBackward();
//    }

//    // 앞 컴포넌트 따라가고 로테이트한다
//    class ChainFollowSolver
//    {
//        public void Solve(List<IChainFollowComponent> list)
//        {
//            for (int i = 1; i < list.Count; ++i)
//            {
//                var target = list[i - 1].GetBackward();
//                var forward = list[i].GetForward();
//                var backward = list[i].GetBackward();
//                var trans = list[i].GetTransform();
//                var position = trans.position;

//                // move delta f to preback
//                // ratate delta angle  b with target

//                //var bodyVector = forward.position - backward.position;
//                //var angle = Vector3.SignedAngle(bodyVector, target.position - position, Vector3.up) / 2f;
//                //Debug.Log(angle);
                
//                //list[i].GetTransform().Rotate(Vector3.up * angle);
//                //var rotdelta = Vector3.SignedAngle(target.position, backward.position, new Vector3(0, 1, 0));
//                //list[i].GetTransform().Rotate(0, rotdelta / 2f, 0);
//                //Debug.Log(rotdelta);

//                var movedelta = target.position - forward.position;
//                list[i].GetTransform().position += movedelta;
//            }
//        }
//    }
//}
