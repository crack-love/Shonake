using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shotake
{
    abstract class SnakeComponent : MonoBehaviour, ISnakeCaptureTarget
    {
        [SerializeField] Transform m_lookablePosition;

        public Transform SpawnPositionTrans => m_lookablePosition;

        public abstract float GetComponentWidth();

        Transform ISnakeCaptureTarget.GetTransform()
        {
            return transform;
        }
    }
}
