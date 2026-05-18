using UnityEngine;

namespace SUG_UnityCore.Event
{
    public class PlayerDiedEvent: IEventBusR<float, int> { }
    public class PlayerAttackEvent : IEventBusR<float> { }
}