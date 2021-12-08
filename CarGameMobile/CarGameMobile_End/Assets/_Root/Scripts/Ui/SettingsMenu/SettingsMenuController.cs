using Profile;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

namespace UI.SettingsMenu
{
    internal class SettingsMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/SettingsMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsMenuView _view;

        public SettingsMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(Back);
        }

        private SettingsMenuView LoadView(Transform plaseForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, plaseForUI, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsMenuView>();
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

    }
}