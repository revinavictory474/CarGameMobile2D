using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal interface IRewardView
    {
        public abstract float TimeCooldown { get; set; }
        public abstract float TimeDeadline { get; set; }
        public abstract List<Reward> Rewards { get; set; }
        public abstract TMP_Text TimerNewReward { get; set; }
        public abstract Transform MountRootSlotsReward { get; set; }
        public abstract ContainerSlotRewardView ContainerSlotRewardPrefab { get; set; }
        public abstract Button GetRewardButton { get; set; }
        public abstract Button ResetButton { get; set; }
        public abstract int CurrentSlotInActive { get; set; }
        public abstract DateTime? TimeGetReward { get; set; }
    }

    internal abstract class RewardView : MonoBehaviour, IRewardView
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);


        public abstract float TimeCooldown { get; set; }
        public abstract float TimeDeadline { get; set; }
        public abstract List<Reward> Rewards { get; set; }
        public abstract TMP_Text TimerNewReward { get; set; }
        public abstract Transform MountRootSlotsReward { get; set; }
        public abstract ContainerSlotRewardView ContainerSlotRewardPrefab { get; set; }
        public abstract Button GetRewardButton { get; set; }
        public abstract Button ResetButton { get; set; }

        public virtual int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public virtual DateTime? TimeGetReward
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