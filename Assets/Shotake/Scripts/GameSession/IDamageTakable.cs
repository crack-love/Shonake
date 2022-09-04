using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shotake
{
    interface IDamageTakable
    {
        void TakeDamage(GameObject src, GameObject instanter, float damage, int damageLayer);
    }
}
