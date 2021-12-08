using Tool;
using UnityEngine;

namespace Features.Rewards
{
    internal class CurrencyController : BaseController
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        private const string MeatKey = nameof(MeatKey);
        private const string SilverKey = nameof(SilverKey);

        private readonly ResourcePath _resourcePath =
            new ResourcePath("Prefabs/Rewards/CurrencyView");
        private readonly CurrencyView _view;

        public CurrencyController(Transform placeForUi)
        {
            _view = LoadView(placeForUi);
            RefreshView();
        }

        private CurrencyView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CurrencyView>();
        }

        private int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey, 0);
            set => PlayerPrefs.SetInt(WoodKey, value);
        }

        private int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey, 0);
            set => PlayerPrefs.SetInt(DiamondKey, value);
        }
        
        private int Meat
        {
            get => PlayerPrefs.GetInt(MeatKey, 0);
            set => PlayerPrefs.SetInt(MeatKey, value);
        }
        
        private int Silver
        {
            get => PlayerPrefs.GetInt(SilverKey, 0);
            set => PlayerPrefs.SetInt(SilverKey, value);
        }

        public void AddWood(int value)
        {
            Wood += value;
            _view.SetWood(Wood);
        }
        
        public void AddDiamond(int value)
        {
            Diamond += value;
            _view.SetDiamond(Diamond);
        }
        
        public void AddMeat(int value)
        {
            Meat += value;
            _view.SetMeat(Meat);
        }
        
        public void AddSilver(int value)
        {
            Silver += value;
            _view.SetSilver(Silver);
        }

        public void RefreshView() => 
            _view.Init(Wood, Diamond, Meat, Silver);
    }
}