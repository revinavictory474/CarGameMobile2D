using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Tool.Localization.Examples
{
    internal class LocalizationWindowByCode : LocalizationWindow
    {
        [SerializeField] private TMP_Text _changeText;

        [Header("Settings")]
        [SerializeField] private string _tableName;
        [SerializeField] private string _localizationTag;


        protected override void OnStarted()
        {
            OnSelectedLocaleChanged(null);
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        protected override void OnDestroyed()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }


        private void OnSelectedLocaleChanged(Locale locale) =>
            StartCoroutine(ChangingLocaleRoutine());

        private IEnumerator ChangingLocaleRoutine()
        {
            AsyncOperationHandle<StringTable> loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(_tableName);
            yield return loadingOperation;

            if (loadingOperation.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException);
                yield break;
            }

            StringTable table = loadingOperation.Result;
            _changeText.text = table.GetEntry(_localizationTag)?.GetLocalizedString();
        }
    }
}