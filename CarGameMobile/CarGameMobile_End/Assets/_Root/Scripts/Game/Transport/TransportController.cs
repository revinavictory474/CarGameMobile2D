using Features.AbilitySystem;
using Tool;
using UnityEngine;

namespace Game.Transport
{
    internal abstract class TransportController : BaseController, IAbilityActivator
    {
        private readonly TransportModel _model;

        public float JumpHeight => _model.JumpHeight;
        public abstract GameObject ViewGameObject { get; }


        protected TransportController(TransportModel model) =>
            _model = model;
    }
}
