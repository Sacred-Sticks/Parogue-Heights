using Kickstarter.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kickstarter.Bootstrapper
{
    public class Bootstrapper : PersistentSignleton<Bootstrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private async static void Init()
        {
            await SceneManager.LoadSceneAsync("Bootstrapper", mode: LoadSceneMode.Single);
        }
    }
}

