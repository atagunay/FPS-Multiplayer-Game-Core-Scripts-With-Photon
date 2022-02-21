using System;
using InputScripts;
using Photon.Realtime;
using PlayerScripts;
using UnityEngine;

namespace AttackScripts
{
    public class AttackManager: MonoBehaviour, IAttackManager
    {
        [SerializeField] private GameObject bulletImpact;
        
        public void Shoot()
        {
            Camera mainCamera = Camera.main;
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            ray.origin = mainCamera.transform.position;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Instantiate(bulletImpact, hit.point + (hit.normal * .002f), Quaternion.LookRotation(hit.normal ,Vector3.up));
            
            }
        }
    }
}