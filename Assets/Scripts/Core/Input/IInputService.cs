using System;
using UnityEngine;

namespace Core.Input
{
    public interface IInputService
    {
        public bool IsInteractionPressed { get; }
        public Vector2 InteractionPosition { get; }
        public event Action<Vector2> OnInputPositionPerformed;
    }
}
