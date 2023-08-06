using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI.MainMenu.Character
{
    public class CharacterView : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _characterButton;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Awake() =>
            _characterButton.onClick.AddListener(OnCharacterClicked);

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCharacterClicked()
        {
            
        }
    }
}
