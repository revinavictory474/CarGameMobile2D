using UnityEngine;

namespace Rewards
{

    internal class CurrencyView : MonoBehaviour
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        private const string MeatKey = nameof(MeatKey);
        private const string SilverKey = nameof(SilverKey);

        public static CurrencyView Instance;

        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currentDiamond;
        [SerializeField] private CurrencySlotView _currencyMeat;
        [SerializeField] private CurrencySlotView _currentSilver;

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


        private void Awake() =>
            Instance = this;

        private void OnDestroy() =>
            Instance = null;

        private void Start() =>
            RefreshText();


        public void AddWood(int value)
        {
            Wood += value;
            RefreshText();
        }

        public void AddDiamond(int value)
        {
            Diamond += value;
            RefreshText();
        }

        public void AddMeat(int value)
        {
            Meat += value;
            RefreshText();
        }
        
        public void AddSilver(int value)
        {
            Silver += value;
            RefreshText();
        }

        private void RefreshText()
        {
            _currencyWood.SetData(Wood);
            _currentDiamond.SetData(Diamond);
            _currencyMeat.SetData(Meat);
            _currentSilver.SetData(Silver);
        }
    }

}