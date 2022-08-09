using UnityEngine;

namespace Shotake
{
    public class PlayerController : MonoBehaviour
    {
        public float m_moveSpeed;
        public GameObject m_player;

        Joystick m_joystick;

        private void Start()
        {
            m_joystick = (Joystick)UIObjectManager.Instance.GetObject("Joystick");
        }

        private void Update()
        {
            Vector2 axis = m_joystick.GetAxis();
            transform.Translate(axis * m_moveSpeed * TimeManager.Instance.GameDeltaTime);
        }
        //public float MoveSpeedMps = 7.5f;
        //public Camera PlayerCamera;

        //void Start()
        //{        
        //    // Lock cursor
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}

        //void Update()
        //{
        //    // We are grounded, so recalculate move direction based on axes
        //    Vector3 forward = transform.TransformDirection(Vector3.forward);
        //    Vector3 right = transform.TransformDirection(Vector3.right);
        //    // Press Left Shift to run
        //    bool isRunning = Input.GetKey(KeyCode.LeftShift);
        //    float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        //    float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        //    float movementDirectionY = moveDirection.y;
        //    moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //    if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        //    {
        //        moveDirection.y = jumpSpeed;
        //    }
        //    else
        //    {
        //        moveDirection.y = movementDirectionY;
        //    }

        //    // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        //    // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        //    // as an acceleration (ms^-2)
        //    if (!characterController.isGrounded)
        //    {
        //        moveDirection.y -= gravity * Time.deltaTime;
        //    }

        //    // Move the controller
        //    characterController.Move(moveDirection * Time.deltaTime);

        //    // Player and Camera rotation
        //    if (canMove)
        //    {
        //        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        //        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        //        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        //    }
        //}
    }
}