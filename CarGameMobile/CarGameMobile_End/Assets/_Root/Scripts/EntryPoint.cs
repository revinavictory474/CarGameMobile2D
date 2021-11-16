using Game.Transport;
using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;
    private const TransportType TransportTypeConst = TransportType.Car;

    [SerializeField] private Transform _placeForUi;
    //[SerializeField] private IAPService _iapService;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, TransportTypeConst, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);


        //_iapService.Initialized.AddListener(OnIapInitialized);
        _adsService.Initialized.AddListener(_adsService.InterstitialPlayer.Play);
        _analytics.SendMainMenuOpened();
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }

    //private void OnIapInitialized() => _iapService.Buy("product_1");
    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
}
