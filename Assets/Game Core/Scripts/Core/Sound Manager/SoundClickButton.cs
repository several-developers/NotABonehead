using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class SoundClickButton : MonoBehaviour
    {
        public SoundType soundType;

        private void Awake() =>
            GetComponent<Button>().onClick.AddListener(PlaySound);

        private void PlaySound() =>
            Sounds.PlaySound(soundType);
    }
}