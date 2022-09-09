using UnityEngine;

namespace Shotake
{
    interface ISnakeCaptureTarget
    {
        Transform GetTransform();

        Transform GetBegPosition();

        Transform GetEndPosition();
    }
}
