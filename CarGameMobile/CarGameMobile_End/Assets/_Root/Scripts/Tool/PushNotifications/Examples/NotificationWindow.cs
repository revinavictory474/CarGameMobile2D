using UnityEngine;
using UnityEngine.UI;
using Tool.PushNotifications;
using Tool.PushNotifications.Settings;

namespace Tool.Notifications.Examples
{
    internal class NotificationWindow : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private NotificationSettings _settings;

        [Header("Scene Components")] 
        [SerializeField] private Button _buttonNotification;

        private NotificationSchedulerFactory _factory;
        private INotificationScheduler _scheduler;


        private void Awake()
        {
            _factory = new NotificationSchedulerFactory(_settings);
            _scheduler = _factory.CreateScheduler();
        }

        private void OnEnable() =>
            _buttonNotification.onClick.AddListener(CreateNotification);

        private void OnDisable() =>
            _buttonNotification.onClick.RemoveAllListeners();

        private void CreateNotification()
        {
            foreach (NotificationData notificationData in _settings.Notifications)
                _scheduler.ScheduleNotification(notificationData);
        }
    }
}