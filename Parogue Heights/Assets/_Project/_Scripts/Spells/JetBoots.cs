using UnityEngine;

namespace Parogue_Heights
{
    public sealed class JetBoots : ISpell
    {
        public JetBoots()
        {
            Uses = initialUses;
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
            particleMediators = new[]
            {
                Registry.Get<ParticlesMediator>(RegistryStrings.LeftJetBoot),
                Registry.Get<ParticlesMediator>(RegistryStrings.RightJetBoot),
            };
            jetForce = Mathf.Sqrt(Mathf.Abs(jetHeight * Physics.gravity.y * 2));
        }

        private const int initialUses = 10;
        private const float jetHeight = 1f;
        private readonly Rigidbody body;
        private readonly float jetForce;
        private readonly ParticlesMediator[] particleMediators;

        private bool active;
        
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
            if (active)
                return;
            body.AddForce(Vector3.up * jetForce, ForceMode.VelocityChange);
            foreach (var particleMediator in particleMediators)
                particleMediator.Play();
            active = true;
        }

        public void OnActivateEnd()
        {
            if (!active)
                return;
            ISpell.LowerUses(this);
            active = false;
        }
        #endregion
    }
}
