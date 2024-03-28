using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Parogue_Heights
{
    public sealed class JetBoots : ITool
    {
        public JetBoots()
        {
            Uses = initialUses;
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
            jetForce = Mathf.Sqrt(Mathf.Abs(jetHeight * Physics.gravity.y * 2));
        }

        private const int initialUses = 10;
        private const float jetHeight = 1f;
        private readonly Rigidbody body;
        private readonly float jetForce;

        #region Tool
        private int uses;
        public int Uses
        {
            get => uses;
            set
            {
                uses = value;
                InventoryHUD.ChangeSlotCount(InventorySlot, Uses);
            }
        }
        public InventorySlot InventorySlot { get; set; }

        public void GainUses() => Uses += initialUses;

        public void OnActivateBegin()
        {
            body.AddForce(Vector3.up * jetForce, ForceMode.VelocityChange);
            ITool.LowerUses(this);
        }

        public void OnActivateEnd()
        {
            
        }
        #endregion
    }
}
