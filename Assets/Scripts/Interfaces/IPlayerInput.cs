using UnityEngine;

namespace Interfaces
{
    public interface IPlayerInput
    {
        Vector2 MoveInput { get; }
        Vector2 LookInput { get; }
        bool JumpTriggered { get; }
        float SprintValue { get; }
    }
}
