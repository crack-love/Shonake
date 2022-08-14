using UnityEngine;
using UnityCommon;
using System.Collections.Generic;

namespace Shotake
{
    /// <summary>
    /// manage a game sesstion from start to end
    /// </summary>
    class GameManager : MonobehaviourSingletone<GameManager>
    {
        public PlayerController PlayerController;
        public CameraController CameraController;
        public PlayerState PlayerStates;
        public Player Players;
    }
}
