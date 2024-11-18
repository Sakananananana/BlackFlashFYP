using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputSystem
{

    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInputs.IInterfaceActions, GameInputs.IGameplayActions, GameInputs.IInventoryActions
    {
        private GameInputs _gameInputs;

        public Action<Vector2> MoveEvent;
        public Action AttackEvent;
        public Action DashEvent;
        public Action InteractEvent;
        public Action OpenInventoryEvent;
        public Action CloseInventoryEvent;
        public Action PauseEvent;
        public Action ResumeEvent;

        private void OnEnable()
        {
            if (_gameInputs == null)
            { 
                _gameInputs = new GameInputs();

                _gameInputs.Gameplay.SetCallbacks(this);
                _gameInputs.Interface.SetCallbacks(this);
                _gameInputs.Inventory.SetCallbacks(this);

                SetGameplay();
            }
        }

        public void SetGameplay()
        {
            _gameInputs.Gameplay.Enable();
            _gameInputs.Interface.Disable();
            _gameInputs.Inventory.Disable();
        }

        public void SetInterface()
        {
            _gameInputs.Interface.Enable();
            _gameInputs.Gameplay.Disable();
        }

        public void SetInventory()
        { 
            _gameInputs.Inventory.Enable();
            _gameInputs.Gameplay.Disable();
        }

        #region Gameplay Move(WASD), Attack(J), Dash( )
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                AttackEvent?.Invoke();
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                DashEvent?.Invoke();
            }
        }
        #endregion

        #region Interaction (J)
        public void OnInteract(InputAction.CallbackContext context)
        {
            //if in talking radius
            if (context.phase == InputActionPhase.Started)
            {
                InteractEvent?.Invoke();
            }
        }
        #endregion

        #region Inventory (I)
        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                OpenInventoryEvent?.Invoke();
                SetInventory();
            }
        }

        public void OnCloseInventory(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                OpenInventoryEvent?.Invoke();
                SetGameplay();
            }
        }
        #endregion

        #region Pause/Resume (Escape)
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                PauseEvent?.Invoke();
                SetInterface();
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                ResumeEvent?.Invoke();
                SetGameplay();
            }
        }
        #endregion
    }
}

