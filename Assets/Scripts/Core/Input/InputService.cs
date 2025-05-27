using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    
        private bool IsPointerOverUI(Vector2 screenPosition)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = screenPosition;

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 0;
        }
    
        public void OnInputPosition(InputAction.CallbackContext context) 
        {
            if(IsPointerOverUI(Pointer.current.position.ReadValue()))
                return;
        
            if (context.performed)
                OnInputPositionPerformed?.Invoke(Pointer.current.position.ReadValue());
        }
    }
}
