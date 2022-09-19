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
        [SerializeField] Transform m_lookingPosition;
        [SerializeField] Transform m_lookedPosition;

        public Transform GetBegPosition()
        {
            return m_lookingPosition;
        }

        public Transform GetEndPosition()
        {
            return m_lookedPosition;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}