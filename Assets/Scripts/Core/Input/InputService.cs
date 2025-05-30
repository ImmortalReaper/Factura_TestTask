using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Input
{
    public class InputService : IInputService, IInitializable, IDisposable 
    {
        private const string PLAYER_ACTION_MAP = "Player";
        private const string INPUT_POSITION_ACTION = "InputPosition";
        private readonly InputActionAsset _inputActions;
        private InputAction _interactionAction;
        
        public bool IsInteractionPressed => _interactionAction.IsPressed();
        public Vector2 InteractionPosition => _interactionAction.ReadValue<Vector2>();
        public event Action<Vector2> OnInputPositionPerformed;

        public InputService(InputActionAsset inputActionAsset)
        {
            _inputActions = inputActionAsset;
        }

        public void Initialize() 
        {
            _inputActions.Enable();
            _interactionAction = _inputActions.FindActionMap(PLAYER_ACTION_MAP).FindAction(INPUT_POSITION_ACTION);
        
            _interactionAction.performed += OnInputPosition;
        }

        public void Dispose() 
        {
            _inputActions.Disable();
            _interactionAction.performed -= OnInputPosition;
        }
    
        private void OnInputPosition(InputAction.CallbackContext context) 
        {
            if (context.performed)
            {
                OnInputPositionPerformed?.Invoke(Pointer.current.position.ReadValue());
            }
        }
    }
}
