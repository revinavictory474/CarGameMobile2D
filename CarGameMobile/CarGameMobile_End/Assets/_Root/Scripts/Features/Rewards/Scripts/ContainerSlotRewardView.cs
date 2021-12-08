using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Features.Rewards
{
    internal class ContainerSlotRewardView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [FormerlySerializedAs("_textDays")] [SerializeField] private TMP_Text _textCooldownPeriod;
        [SerializeField] private TMP_Text _countReward;

        [Header("Settings")] [SerializeField] private string _cooldownPeriodKey;
        public void SetData(Reward reward, int countCooldownPeriod, bool isSelect)
        {
            _iconCurrency.sprite = reward.IconCurrency;
            _textCooldownPeriod.text = $"{_cooldownPeriodKey} {countCooldownPeriod}";
            _countReward.text = reward.CountCurrency.ToString();

            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}