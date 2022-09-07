using UnityEngine;

namespace Shotake
{
    // Holding global managers
    // 에디터 아무 씬에서 동일하게 사용할 수 있도록 프레펩으로 저장 할 것
    class GlobalManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}