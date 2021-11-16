namespace Game.Transport
{
    internal class TransportModel
    {
        public readonly float Speed;
        public readonly TransportType Type;

        public TransportModel(float speed, TransportType type)
        {
            Speed = speed;
            Type = type;
        }
    }
}
