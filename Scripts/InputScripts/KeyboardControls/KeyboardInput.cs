using UnityEngine;

namespace InputScripts.KeyboardControls
{
    public class KeyboardInput : InputHandler, IKeyboardInput
    {
        private float _mouseSensitivity = 1f;
        private Vector2 _mouseInput;
        private Vector3 _keyboardInput;

        private void Update()
        {
            isJump = false;
            isSlowWalk = false;
            isShoot = false;
            
            GetMouseAxis();
            GetKeyboardAxis();

            if (Input.GetKey(KeyCode.LeftShift))
                isSlowWalk = true;

            if (Input.GetKeyDown(KeyCode.Space))
                isJump = true;

            if (Input.GetMouseButtonDown(0))
                isShoot = true;

            ControlChangeWeapon();
            
               
            

            CursorControl();
        }

        /// <summary>
        /// Get informations from mouse
        /// </summary>
        public void GetMouseAxis()
        {
            // Take axis and multiply with mouse sensitive attributes
            _mouseInput.x = Input.GetAxisRaw("Mouse X") * _mouseSensitivity;
            _mouseInput.y = Input.GetAxis("Mouse Y") * _mouseSensitivity;
            lookingAroundInformations = _mouseInput;
        }

        /// <summary>
        /// Get informations from keyboard
        /// </summary>
        public void GetKeyboardAxis()
        {
            _keyboardInput.x = Input.GetAxisRaw("Horizontal");
            _keyboardInput.z = Input.GetAxisRaw("Vertical");
            movementInformations = _keyboardInput;
        }
        
        /// <summary>
        /// For ESC button, cursor features
        /// </summary>
        public void CursorControl()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }

        }

        public void ControlChangeWeapon()
        {
            isIncreaseChangeWeaponInventory = false;
            isDecreaseChangeWeaponInventory = false;
            
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                isIncreaseChangeWeaponInventory = true;
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                isDecreaseChangeWeaponInventory = true;
            }
        }
    }
}
