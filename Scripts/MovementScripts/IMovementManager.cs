using UnityEngine;
namespace MovementScripts
{
    public interface IMovementManager
    {
        public void Movement(Transform playerTransform, CharacterController playerCharacterController);
        public void HorizontalRotation(Vector3 currentRotation);
        public void VerticalRotation();
        public void CameraFollow();
    }
}