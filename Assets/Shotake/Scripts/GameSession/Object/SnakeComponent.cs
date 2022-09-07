using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shotake
{
    abstract class SnakeComponent : MonoBehaviour, ISnakeCaptureTarget2
    {
        [SerializeField] Transform m_lookingPosition;
        [SerializeField] Transform m_lookedPosition;

        Transform ISnakeCaptureTarget2.GetBegPosition()
        {
            return m_lookingPosition;
        }

        Transform ISnakeCaptureTarget2.GetEndPosition()
        {
            return m_lookedPosition;
        }

        Transform ISnakeCaptureTarget2.GetTransform()
        {
            return transform;
        }
    }
}
