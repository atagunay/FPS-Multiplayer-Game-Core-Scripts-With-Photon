using UnityEngine;

namespace InputScripts
{
    public class InputHandler : MonoBehaviour
    {
        protected Vector2 lookingAroundInformations;
        protected Vector3 movementInformations;
        protected bool isSlowWalk = false;
        protected bool isJump = false;
        protected bool isShoot = false;
        protected bool isIncreaseChangeWeaponInventory = false;
        protected bool isDecreaseChangeWeaponInventory = false;

      

        public Vector2 LookingAroundInformations => lookingAroundInformations;
        public Vector3 MovementInformations => movementInformations;
        public bool IsSlowWalk => isSlowWalk;
        public bool IsJump => isJump;
        public bool IsShoot => isShoot;
        public bool IsIncreaseChangeWeaponInventory => isIncreaseChangeWeaponInventory;
        public bool IsDecreaseChangeWeaponInventory => isDecreaseChangeWeaponInventory;
    }
}