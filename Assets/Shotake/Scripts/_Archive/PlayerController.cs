//using UnityCommon;
//using UnityEngine;

//namespace Shotake.Archive
//{
//    public class PlayerController : MonobehaviourSingletone<PlayerController>
//    {
//        public float m_moveSpeed;
//        public float m_rotSpeedAtFast;
//        public float m_rotSpeedAtSlow;
//        public GameObject m_player;
//        public GameObject m_cameraHolder;

//        Joystick m_joystick;
//        public float m_angle;

//        private void Start()
//        {
//            m_joystick = (Joystick)UIObjectManager.Instance.GetObject("Joystick");
//        }

//        public GameObject GetPlayerObject()
//        {
//            return m_player;
//        }

//        private void Update()
//        {
//            var axis = m_joystick.GetAxis();
//            if (Mathf.Abs(axis.x) > 0 || Mathf.Abs(axis.y) > 0)
//            {
//                var axisMag = axis.magnitude;
//                var deltaTime = TimeManager.Instance.GameDeltaTime;

//                // rotate
//                var desireAngle = Vector2.SignedAngle(new Vector2(0, 1), axis);
//                var angleDiff = Mathf.DeltaAngle(m_angle, desireAngle);
//                angleDiff %= Mathf.Lerp(m_rotSpeedAtSlow, m_rotSpeedAtFast, axisMag) * deltaTime;
//                m_angle += angleDiff;
//                transform.rotation = Quaternion.Euler(0, -m_angle, 0);

//                // move
//                float moveDelta = axisMag * m_moveSpeed * deltaTime;
//                transform.position += transform.forward * moveDelta;
//                m_cameraHolder.transform.position = transform.position + new Vector3(0, m_cameraHolder.transform.position.y, 0);

//                //m_player.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveDelta);
//                //m_player.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, -m_angle, 0));
//            }
//        }
//    }
//}