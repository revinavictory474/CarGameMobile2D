using Game.Transport;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using Game.Transport.Car;
using Game.Transport.Boat;
using System;

namespace Game
{
    internal class GameController : BaseController
    {
        private ProfilePlayer _profilePlayer;
        private TransportController _transportController;

        public GameController(ProfilePlayer profilePlayer)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentTransportModel);
            AddController(inputGameController);

            _transportController = CreateTransportController();
        }

        private TransportController CreateTransportController()
        {
            TransportController transportController;

            switch(_profilePlayer.CurrentTransportModel.Type)
            {
                case TransportType.Car:
                    transportController = new CarController();
                    break;
                case TransportType.Boat:
                    transportController = new BoatController();
                    break;
                default:
                    throw new ArgumentException(nameof(TransportType));
            }

            AddController(transportController);

            return transportController;
        }
    }
}
