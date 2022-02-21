using System;
using AttackScripts;
using InfimaGames.LowPolyShooterPack;
using InputScripts;
using InputScripts.KeyboardControls;
using InventoryScripts;
using MovementScripts;
using Photon.Pun;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private InventoryManager inventory;
 
        private IMovementManager _movementManager;
        private IAttackManager _attackManager;
        private InputHandler _inputHandler;

   
        private void Awake()
        {
            _movementManager = GetComponent<MovementManager>();
            _attackManager = GetComponent<AttackManager>();
            _inputHandler = GetComponent<KeyboardInput>();
        }

        private void Start()
        {
            inventory.Equip(0);
        }

        // Update is called once per frame
        private void Update()
        {
            if (photonView.IsMine)
            {

                _movementManager.HorizontalRotation(transform.rotation.eulerAngles);
                _movementManager.VerticalRotation();
                _movementManager.Movement(gameObject.transform, characterController);

                // first shoot
                if (_inputHandler.IsShoot)
                    _attackManager.Shoot();
                //inventory.GetEquipped().gameObject.GetComponent<Weapon>().Fire();



                if (_inputHandler.IsIncreaseChangeWeaponInventory)
                    inventory.IncreaseWeapon();

                if (_inputHandler.IsDecreaseChangeWeaponInventory)
                    inventory.DecreaseWeapon();

            }
        }
    
        private void LateUpdate()
        {
            if (photonView.IsMine)
            {
                _movementManager.CameraFollow();
            }
            
        }
    }
}
