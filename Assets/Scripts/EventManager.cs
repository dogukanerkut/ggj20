
using UnityEngine.Events;
using Events;
public static class EventManager
{
    public static EventWallBreak EventWallBreak = new EventWallBreak();
    public static EventGameOver EventGameOver = new EventGameOver();
}

namespace Events
{
    public class EventWallBreak : UnityEvent<WallBreakHandler> {}
    public class EventGameOver : UnityEvent {}
}