using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Infrastructure.Services.MainMenu.ItemsShowcase;
using GameCore.UI.Global.MenuView;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI.MainMenu.ItemsShowcaseMenu
{
    public class ItemsShowcaseMenuView : MenuView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IInventoryService inventoryService, IItemsShowcaseService itemsShowcaseService,
            IRewardsService rewardsService)
        {
            _menuLogic = new ItemsShowcaseMenuLogic(inventoryService, itemsShowcaseService, rewardsService, _menuVisualizer);
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private Button _closeButton;
        
        [SerializeField, Required]
        private Button _overlayCloseButton;
        
        [SerializeField, Required]
        private Button _singleEquipButton;
        
        [SerializeField, Required]
        private Button _equipButton;
        
        [SerializeField, Required]
        private Button _dropButton;

        [TitleGroup(Constants.Visualizer)]
        [BoxGroup(Constants.VisualizerIn, showLabel: false), SerializeField]
        private ItemsShowcaseMenuVisualizer _menuVisualizer;

        // FIELDS: --------------------------------------------------------------------------------

        private ItemsShowcaseMenuLogic _menuLogic;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClicked);
            _overlayCloseButton.onClick.AddListener(OnCloseClicked);
            _singleEquipButton.onClick.AddListener(OnEquipClicked);
            _equipButton.onClick.AddListener(OnEquipClicked);
            _dropButton.onClick.AddListener(OnDropClicked);
            
            DestroyOnHide();
        }

        private void Start()
        {
            _menuLogic.UpdateButtonsState();
            Show();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCloseClicked() => Hide();

        private void OnEquipClicked()
        {
            _menuLogic.HandleEquipLogic();
            OnCloseClicked();
        }

        private void OnDropClicked()
        {
            _menuLogic.HandleDropItemLogic();
            OnCloseClicked();
        }
    }
}