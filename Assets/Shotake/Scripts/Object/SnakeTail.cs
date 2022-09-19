using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace Shotake
{
    class SnakeTail : SnakeComponent
    {
        public void SetSize(float size)
        {
            transform.localScale = new Vector3(size, 1, size);
        }
    }
}
