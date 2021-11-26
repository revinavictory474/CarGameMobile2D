using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal class WeeklyRewardView : RewardView, IRewardView
    {
        private const float WEEK = 604800;
        private const float TWOWEEK = 1209600;
        
        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public override float TimeCooldown { get; set; } = WEEK;
        [field: SerializeField] public override float TimeDeadline { get; set; } = TWOWEEK;
        
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