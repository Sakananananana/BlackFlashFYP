using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputSystem
{

    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInputs.IUIActions, GameInputs.IGameplayActions, GameInputs.IInventoryActions
    {
        private GameInputs _gameInputs;

        public Action<Vector2> MoveEvent;
        public Action AttackEvent;
        public Action DashEvent;
        public Action InteractEvent;
        public Action OpenInventoryEvent;
        public Action CloseInventoryEvent;
        public Action PressedEvent;
        public Action PauseEvent;
        public Action ResumeEvent;

        private void OnEnable()
        {
            if (_gameInputs == null)
            { 
                _gameInputs = new GameInputs();

                _gameInputs.Inventory.SetCallbacks(this);
                _gameInputs.Gameplay.SetCallbacks(this);
                _gameInputs.UI.SetCallbacks(this);

                SetGameplay();
            }
        }

        private void OnDisable()
        {
            _gameInputs.Inventory.Disable();
            _gameInputs.Gameplay.Disable();
            _gameInputs.UI.Disable();
        }

        public void SetGameplay()
        {
            _gameInputs.Gameplay.Enable();

            _gameInputs.Inventory.Disable();
            _gameInputs.UI.Disable();
        }

        public void SetUI()
        {
            _gameInputs.UI.Enable();

            _gameInputs.Inventory.Disable();
            _gameInputs.Gameplay.Disable();
        }

        public void SetInventory()
        {
            _gameInputs.Inventory.Enable();

            _gameInputs.Gameplay.Disable();
            _gameInputs.UI.Disable();
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
            //probably can remove this, later add an interaction radius to Attack
            //if in talking radius 
            if (context.phase == InputActionPhase.Started)
            {
                InteractEvent?.Invoke();
            }
        }
        #endregion

        #region User Interface

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
                CloseInventoryEvent?.Invoke();
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
                SetUI();
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

        //Move Around Buttons
        public void OnNavigate(InputAction.CallbackContext context)
        {

        }

        //Choose (J)
        public void OnSubmit(InputAction.CallbackContext context)
        {

        }

        //Cancel Chosen Action (N)
        public void OnCancel(InputAction.CallbackContext context)
        {
            
        }
        #endregion
    }
}

