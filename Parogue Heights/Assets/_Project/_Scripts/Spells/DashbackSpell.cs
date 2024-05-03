using UnityEngine;

namespace Parogue_Heights
{
    public sealed class DashbackSpell : ISpell
    {
        private const float thrusterForce = 10f;
        private const int initialUses = 4;
        private int uses;
        private readonly Rigidbody body;
        private readonly ParticlesMediator[] particleMediators;
        private readonly Transform cameraTransform;
        private bool active;

        public DashbackSpell()
        {
            Uses = initialUses;
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
            particleMediators = new[]
            {
                Registry.Get<ParticlesMediator>(RegistryStrings.LeftSmasher),
                Registry.Get<ParticlesMediator>(RegistryStrings.RightSmasher),
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
            if (active)
                return;
            body.AddForce(-cameraTransform.forward * thrusterForce, ForceMode.VelocityChange);
            foreach (var particleMediator in particleMediators)
                particleMediator.Play();
            active = true;
        }

        public void OnActivateEnd()
        {
            if (!active)
                return;
            foreach (var particleMediator in particleMediators)
                particleMediator.Stop();
            ISpell.LowerUses(this);
            active = false;
        }
        #endregion
    }
}
