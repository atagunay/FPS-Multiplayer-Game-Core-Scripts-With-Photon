using InputScripts;
using InputScripts.KeyboardControls;
using UnityEngine;

namespace MovementScripts
{
    /// <summary>
    /// Provide keyboard interaction and player control
    /// </summary>
    public class MovementManager : MonoBehaviour, IMovementManager
    {
        #region Attributes

            [SerializeField] private Transform groundCheckPoint;
            [SerializeField] private LayerMask groundLayers;
            [SerializeField] private Transform viewPoint;
            [SerializeField] private InputHandler inputHandler;

            private float _verticalRotationStore;
            private float _horizontalRotationStore;

            private float _movementSpeed = 5f;
            private float _slowMovementSpeed = 1f;
            private float _jumpForce = 3f;

            private Vector3 _movement;
            private bool _isGround;
            private Camera _camera;


        #endregion

        #region Functions

            public virtual void Awake()
            {
                Cursor.lockState = CursorLockMode.Locked; // Cursor locked in the center of game window
                _camera = Camera.main; // Get main camera from inspector
            }


            /// <summary>
            /// Supply player movement
            /// </summary>
            /// <param name="playerTransform"></param>
            /// <param name="playerCharacterController"></param>
            public void Movement(Transform playerTransform, CharacterController playerCharacterController)
            {
                float activeSpeed = _movementSpeed; // This is normal walk/run speed

                if (inputHandler.IsSlowWalk)
                    activeSpeed = _slowMovementSpeed; // This is slow walk/run speed

                float yVel = _movement.y; // This is for our gravity force
                
                // Control direction of movement and normalized.
                // Because if we dont use normalized, player move faster to the cross direction.
                // Due to normalized all movement vector direction equals one.
                _movement = ((playerTransform.forward * inputHandler.MovementInformations.z) + (playerTransform.right * inputHandler.MovementInformations.x))
                    .normalized;
                
                _movement.y = yVel; // Assign new y axis

                // if player on the ground we dont want any gravity force
                if (playerCharacterController.isGrounded)
                    _movement.y = 0; 

                // Create a ray from groundCheckPoint.position and it longs 0.25f
                // if ray reach a collider which's tag is ground return true
                _isGround = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

                // if player is ground and user press space
                // we add jumpforce to the our direction vector
                if (inputHandler.IsJump && _isGround)
                    _movement.y = _jumpForce;
                
                // Every frame increase gravity force
                _movement.y += Physics.gravity.y * Time.deltaTime;

                // this is another way for transform +=
                // this function use with charachter controller
                playerCharacterController.Move(_movement * (activeSpeed * Time.deltaTime));
            }

            /// <summary>
            /// Change player rotation
            /// </summary>
            /// <param name="currentRotation"></param>
            public void HorizontalRotation(Vector3 currentRotation)
            {
                // calculate horizontal rotation
                _horizontalRotationStore += inputHandler.LookingAroundInformations.x;
                
                // rotate player with mouse
                transform.rotation = Quaternion.Euler(currentRotation.x,
                    _horizontalRotationStore, currentRotation.z);
            }

            /// <summary>
            /// Change camera rotation
            /// </summary>
            public void VerticalRotation()
            {
                // calculate vertical rotation
                Vector3 currentRotation = viewPoint.rotation.eulerAngles;
                _verticalRotationStore += inputHandler.LookingAroundInformations.y;
                _verticalRotationStore = Mathf.Clamp(_verticalRotationStore, -60, 60);
                
                // change camera rotation
                viewPoint.rotation = Quaternion.Euler(-_verticalRotationStore, currentRotation.y,
                    currentRotation.z);
            }
            

            /// <summary>
            /// Camera follow to player in the Late Update function in PlayerController
            /// </summary>
            public void CameraFollow()
            {
                var cameraTransform = _camera.transform;
                var viewPointTransform = viewPoint.transform;

                cameraTransform.position = viewPointTransform.position;
                cameraTransform.rotation = viewPointTransform.rotation;
            }

    
        #endregion
    }

    
    
}
