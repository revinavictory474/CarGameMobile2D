using UnityEngine;

namespace Ui.PauseMenu
{
    internal class Pause
    {
        private const float PausedTimeScale = 0f;

        private readonly float _initialTimeScale;


        public Pause() =>
            _initialTimeScale = Time.timeScale;


        public void Enable() =>
            Time.timeScale = PausedTimeScale;

        public void Disable() =>
            Time.timeScale = _initialTimeScale;
    }
}