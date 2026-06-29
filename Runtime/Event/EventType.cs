using UnityEngine;

namespace SUG.UnityCore.Event
{
    public class PlayerDiedEvent: IEventBusR<float, int> { }
    public class PlayerAttackEvent : IEventBusR<float> { }
}