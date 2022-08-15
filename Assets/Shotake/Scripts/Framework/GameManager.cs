using UnityEngine;
using UnityCommon;
using System.Collections.Generic;

namespace Shotake
{
    /// <summary>
    /// manage a game sesstion from start to end
    /// </summary>
    abstract class GameManager : MonobehaviourSingletone<GameManager>
    {
        [SerializeField] PlayerController m_playerController;
        [SerializeField] CameraController m_cameraController;
        [SerializeField] PlayerState m_playerState;
        [SerializeField] Player m_player;

        public PlayerController PlayerController => m_playerController;

        public CameraController CameraController => m_cameraController;

        public PlayerState PlayerState => m_playerState;

        public Player Player => m_player;
    }
}
