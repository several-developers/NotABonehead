using UnityEngine;

namespace GameCore.Other
{
    public class Director : MonoBehaviour
    {
        // FIELDS: --------------------------------------------------------------------------------

        private static Director _instance;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() => TryRegisterDirector();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void TryRegisterDirector()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            
            DontDestroyOnLoad(target: this);
        }
    }
}
