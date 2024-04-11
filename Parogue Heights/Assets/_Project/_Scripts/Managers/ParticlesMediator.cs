using UnityEngine;

namespace Parogue_Heights
{
    public class ParticlesMediator : MonoBehaviour
    {
        [SerializeField] private ParticleType particleType;
        [SerializeField] private Side side;

        private enum ParticleType
        {
            Hookshot,
            JetBoots,
            Thrusters,
            Smashers,
        }
        private enum Side
        {
            Left,
            Right,
        }

        private string registryKey;
        private ParticleSystem[] particleSystems;

        #region UnityEvents
        private void OnEnable()
        {
            AddToRegistry();
        }

        private void OnDisable()
        {
            RemoveFromRegistry();
        }

        private void Start()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>();
        }
        #endregion

        private void AddToRegistry()
        {
            registryKey = side switch
            {
                Side.Left => particleType switch
                {
                    ParticleType.Hookshot => RegistryStrings.LeftHookshot,
                    ParticleType.JetBoots => RegistryStrings.LeftJetBoot,
                    ParticleType.Thrusters => RegistryStrings.LeftThruster,
                    ParticleType.Smashers => RegistryStrings.LeftSmasher,
                    _ => string.Empty,
                },
                Side.Right => particleType switch
                {
                    ParticleType.Hookshot => RegistryStrings.RightHookshot,
                    ParticleType.JetBoots => RegistryStrings.RightJetBoot,
                    ParticleType.Thrusters => RegistryStrings.RightThruster,
                    ParticleType.Smashers => RegistryStrings.RightSmasher,
                    _ => string.Empty,
                },
                _ => string.Empty,
            };
            Registry.Register(registryKey, this);
        }

        private void RemoveFromRegistry()
        {
            Registry.Deregister(registryKey);
        }

        public void Play()
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
        }

        public void Stop()
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }   
    }
}
