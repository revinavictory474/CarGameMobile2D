using Tool;
using UnityEngine;

namespace Game.Transport

{
    internal abstract class TransportController : BaseController
    {
       public abstract GameObject ViewGameObject { get; }
    }
}
