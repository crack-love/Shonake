using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shotake
{
    interface IUIManager
    {
        UIObject GetObjectByName(string name);

        UIObject GetObjectByID(int id);

        T GetObjectByType<T>() where T : class;

#if UNITY_EDITOR
        int AddObjectPersistant(UIObject o);

        bool RemoveObjectPersistant(UIObject o);
#endif
    }
}
