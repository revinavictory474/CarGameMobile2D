using UnityEngine;

namespace Features.Rewards
{

    internal class CurrencyView : MonoBehaviour
    {
        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currentDiamond;
        [SerializeField] private CurrencySlotView _currencyMeat;
        [SerializeField] private CurrencySlotView _currentSilver;

        public void Init(int woodCount, int diamondCount, int meatCount, int silverCount)
        {
            SetWood(woodCount);
            SetDiamond(diamondCount);
            SetMeat(meatCount);
            SetSilver(silverCount);
        }
        public void SetWood(int value) => _currencyWood.SetData(value);

        public void SetDiamond(int value) => _currentDiamond.SetData(value);

        public void SetMeat(int value) => _currencyMeat.SetData(value);

        public void SetSilver(int value) => _currentSilver.SetData(value);

    }

}