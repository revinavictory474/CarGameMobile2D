using Features.Inventory;
using Game.Transport;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InventoryModel InventoryModel;
        public readonly TransportModel CurrentTransportModel;


        public ProfilePlayer(float speedCar, TransportType carView, GameState initialState) : this(speedCar)
        {
            CurrentState.Value = initialState;
            InventoryModel = new InventoryModel();
            CurrentTransportModel = new TransportModel(speedCar, carView);
        }

        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
        }
    }
}
