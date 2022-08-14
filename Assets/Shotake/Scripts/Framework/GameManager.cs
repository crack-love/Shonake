using UnityEngine;
using UnityCommon;

namespace Shotake
{
    /// <summary>
    /// manage a game sesstion from start to end
    /// </summary>
    class GameManager : MonobehaviourSingletone<GameManager>
    {
        PlayerController m_playerControllerPrefab;
        CameraController m_cameraControllerPrefab;

        void Awake()
        {

        }
    }
}
