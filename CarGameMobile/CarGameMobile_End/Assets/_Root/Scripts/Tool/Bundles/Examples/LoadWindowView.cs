using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")] [SerializeField]
        private Button _loadAsssetsButton;

        [SerializeField] private Button _setBackgroundButton;

        [Header("Addressables")] [SerializeField]
        private AssetReference _spawningButtonPrefab;

        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;
        [Space(10)] [SerializeField] private AssetReference _spawningBackgroundPrefab;
        [SerializeField] private RectTransform _spawnedBackgroundContainer;
        [SerializeField] private Button _spawnAssetBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();


        private void Start()
        {
            _loadAsssetsButton.onClick.AddListener(LoadAsset);
            _setBackgroundButton.onClick.AddListener(SetButtonBackground);
            _spawnAssetButton.onClick.AddListener(SpawnButton);
            _spawnAssetBackgroundButton.onClick.AddListener(SpawnCanvasBackground);
            _removeBackgroundButton.onClick.AddListener(RemoveBackground);
        }

        private void OnDestroy()
        {
            _loadAsssetsButton.onClick.RemoveAllListeners();
            _spawnAssetButton.onClick.RemoveAllListeners();

            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }

        private void LoadAsset()
        {
            _loadAsssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
        }

        private void SpawnPrefab(AssetReference assetReference, RectTransform rectTransform)
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(assetReference, rectTransform);

            _addressablePrefabs.Add(addressablePrefab);
        }

        private void SpawnButton() => SpawnPrefab(_spawningButtonPrefab, _spawnedButtonsContainer);

        private void SpawnCanvasBackground() => SpawnPrefab(_spawningBackgroundPrefab, _spawnedBackgroundContainer);

        private void RemoveBackground()
        {
            _spawnedBackgroundContainer.gameObject.GetComponent<Image>().sprite = null;
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);
        }

        private void SetButtonBackground()
        {
            _setBackgroundButton.interactable = false;
            StartCoroutine(DownloadAndSetBackgroundButton());
        }
    }
}