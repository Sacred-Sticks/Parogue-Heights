using UnityEngine;

namespace Parogue_Heights
{
    public sealed class ThrusterSpell : ISpell
    {
        private const float thrusterForce = 8.75f;
        private const int initialUses = 2;
        private int uses;
        private readonly Rigidbody body;
        private readonly ParticlesMediator[] particleMediators;
        private readonly Transform cameraTransform;

        public ThrusterSpell()
        {
            Uses = initialUses;
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
            particleMediators = new[]
            {
                Registry.Get<ParticlesMediator>(RegistryStrings.LeftThruster),
                Registry.Get<ParticlesMediator>(RegistryStrings.RightThruster),
            };
            cameraTransform = Camera.main.transform;
        }

        #region Tool
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
            body.AddForce(cameraTransform.forward * thrusterForce, ForceMode.VelocityChange);
            foreach (var particleMediator in particleMediators)
                particleMediator.Play();
        }

        public void OnActivateEnd()
        {
            foreach (var particleMediator in particleMediators)
                particleMediator.Stop();
            ISpell.LowerUses(this);
        }
        #endregion
    }
}
