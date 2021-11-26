using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace  Rewards
{
    internal class DailyRewardView : RewardView, IRewardView
    {
        private const float DAY = 86400;
        private const float TWODAYS = 172800;
        
        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public override float TimeCooldown { get; set; } = DAY;
        [field: SerializeField] public override float TimeDeadline { get; set; } = TWODAYS;
        
        [field: Header("Settings Rewards")]
        [field: SerializeField] public override List<Reward> Rewards { get; set; }
        
        [field: Header("Ui Elements")]
        [field: SerializeField] public override TMP_Text TimerNewReward { get; set; }
        [field: SerializeField] public override Transform MountRootSlotsReward { get; set; }
        [field: SerializeField] public override ContainerSlotRewardView ContainerSlotRewardPrefab { get; set; }
        [field: SerializeField] public override Button GetRewardButton { get; set; }
        [field: SerializeField] public override Button ResetButton { get; set; }
        
        public override sealed int CurrentSlotInActive { get; set; }
        public override sealed DateTime? TimeGetReward { get; set; }
    }
}