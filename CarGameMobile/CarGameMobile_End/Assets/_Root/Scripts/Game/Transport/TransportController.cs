using Features.AbilitySystem;
using Tool;
using UnityEngine;

namespace Game.Transport
{
    internal abstract class TransportController : BaseController, IAbilityActivator
    {
        public abstract GameObject ViewGameObject { get; }
    }
}
