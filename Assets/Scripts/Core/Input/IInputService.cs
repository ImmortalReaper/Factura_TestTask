using System;
using UnityEngine;

namespace Core.Input
{
    public interface IInputService
    {
        public event Action<Vector2> OnInputPositionPerformed;
    }
}
