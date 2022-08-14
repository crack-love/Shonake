using UnityEngine;

namespace Shotake
{
    [RequireComponent(typeof(CharacterController))]
    class PTPlayerCharCon : MonoBehaviour
    {
        public float speed = 10;

        void Update()
        {
            var con = GetComponent<CharacterController>();
            var axis = UIObjectManager.Instance.GetObject<Joystick>("Joystick").GetAxis();

            Vector3 axis3d = default;
            axis3d.x = axis.x;
            axis3d.y = -1;
            axis3d.z = axis.y;            
            con.Move(axis3d * speed * Time.deltaTime);
        }
    }
}
