using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace InventoryScripts
{
    public class InventoryManager : Inventory
    {
        private int _weaponIndex = 0;
        
        public void IncreaseWeapon()
        {
            _weaponIndex = GetNextIndex();
            Equip(_weaponIndex);
        }
        
        public void DecreaseWeapon()
        {
            _weaponIndex = GetLastIndex();
            Equip(_weaponIndex);
        }
    }
}