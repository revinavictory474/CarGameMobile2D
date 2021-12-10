using System;
using System.Collections;
using System.Collections.Generic;
using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Rewards
{
    
    internal class RewardController : BaseController
    {
        private readonly ResourcePath _resourcePath = 
            new ResourcePath("Prefabs/Rewards/DailyRewardsWindow");
        private readonly RewardView _rewardView;
        private readonly ProfilePlayer _profilePlayer;
        private readonly CurrencyController _currencyController;
        
        private List<ContainerSlotRewardView> _slots;
        private Coroutine _coroutine;
        
        private bool _isGetReward;

        public RewardController(ProfilePlayer profilePlayer, Transform placeForUi)
        {
            _profilePlayer = profilePlayer;
            _rewardView = LoadView(placeForUi);
            _currencyController = CreateCurrencyController(placeForUi);
            InitView();
        }
 
        private CurrencyController CreateCurrencyController(Transform placeForUi)
        {
            var currencyController = new CurrencyController(placeForUi);
            AddController(currencyController);
            return currencyController;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            DeinitView();
        }
        
        public void InitView() 
        {
            InitSlots();
            RefreshUi();
            StartRewardsUpdating();
            SubscribeButtons();
        }

        public void DeinitView()
        {
            DeinitSlots();
            StopRewardsUpdating();
            UnsubscribeButtons();
        }

        private RewardView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<RewardView>();
        }

        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();

            for (int i = 0; i < _rewardView.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreateSlotRewardView();
                _slots.Add(instanceSlot);
            }
        }

        private ContainerSlotRewardView CreateSlotRewardView() =>
            Object.Instantiate(_rewardView.ContainerSlotRewardPrefab, _rewardView.MountRootSlotsReward, false);

        private void DeinitSlots()
        {
            foreach (ContainerSlotRewardView slot in _slots)
                Object.Destroy(slot.gameObject);

            _slots.Clear();
        }


        private void StartRewardsUpdating() =>
            _coroutine = _rewardView.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;

            _rewardView.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1);

            while (true)
            {
                RefreshRewardsState();
                RefreshUi();
                yield return waitForSecond;
            }
        }


        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _rewardView.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _rewardView.TimeGetReward.Value;
            bool isDeadlineElapsed = timeFromLastRewardGetting.Seconds >= _rewardView.TimeDeadline;
            bool isTimeToGetNewReward = timeFromLastRewardGetting.Seconds >= _rewardView.TimeCooldown;

            if (isDeadlineElapsed)
                ResetRewardsState();

            _isGetReward = isTimeToGetNewReward;
        }

        private void ResetRewardsState()
        {
            _rewardView.TimeGetReward = null;
            _rewardView.CurrentSlotInActive = 0;
        }


        private void RefreshUi()
        {
            _rewardView.GetRewardButton.interactable = _isGetReward;
            _rewardView.TimerNewReward.text = GetTimerNewRewardText();
            RefreshSlots();
        }

        private string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_rewardView.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _rewardView.TimeGetReward.Value.AddSeconds(_rewardView.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                string timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                                       $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }

        private void RefreshSlots()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                Reward reward = _rewardView.Rewards[i];
                int countCooldownPeriods = i + 1;
                bool isSelect = i == _rewardView.CurrentSlotInActive;

                _slots[i].SetData(reward, countCooldownPeriods, isSelect);
            }
        }


        private void SubscribeButtons()
        {
            _rewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _rewardView.ResetButton.onClick.AddListener(ResetTimer);
            _rewardView.CloseButton.onClick.AddListener(Close);
        }

        private void UnsubscribeButtons()
        {
            _rewardView.GetRewardButton.onClick.RemoveListener(ClaimReward);
            _rewardView.ResetButton.onClick.RemoveListener(ResetTimer);
            _rewardView.CloseButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            Reward reward = _rewardView.Rewards[_rewardView.CurrentSlotInActive];

            switch (reward.RewardType)
            {
                case RewardType.Wood:
                    _currencyController.AddWood(reward.CountCurrency);
                    break;
                case RewardType.Diamond:
                    _currencyController.AddDiamond(reward.CountCurrency);
                    break;
                case RewardType.Meat:
                    _currencyController.AddMeat(reward.CountCurrency);
                    break;
                case RewardType.Silver:
                    _currencyController.AddSilver(reward.CountCurrency);
                    break;
            }

            _rewardView.TimeGetReward = DateTime.UtcNow;
            _rewardView.CurrentSlotInActive++;

            RefreshRewardsState();
        }

        private void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
            _currencyController.RefreshView();
        }
    }
}