using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rewards
{

    internal class RewardView : MonoBehaviour
    {
        [SerializeField] private string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        [SerializeField] private string TimeGetRewardKey = nameof(TimeGetRewardKey);

        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public  float TimeCooldown { get; private set; }
        [field: SerializeField] public  float TimeDeadline { get; private set; }
        
        [field: Header("Settings Rewards")]
        [field: SerializeField] public  List<Reward> Rewards { get; private set; }
        
        [field: Header("Ui Elements")]
        [field: SerializeField] public  TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public  Transform MountRootSlotsReward { get; private set; }
        [field: SerializeField] public  ContainerSlotRewardView ContainerSlotRewardPrefab { get; private set; }
        [field: SerializeField] public  Button GetRewardButton { get; private set; }
        [field: SerializeField] public  Button ResetButton { get; private set; }
        [field: SerializeField] public  Button CloseButton { get; private set; }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey, null);
                return !string.IsNullOrEmpty(data) ? (DateTime?) DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }
    }
}