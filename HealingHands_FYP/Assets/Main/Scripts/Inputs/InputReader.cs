using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputSystem
{

    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInputs.IUIActions, GameInputs.IGameplayActions, GameInputs.IInventoryActions
    {
        //Listens to interaction events
        [SerializeField] private BoolEventChannelSO _interactionEvent; 
        private bool _interactionEnabled;
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
            _interactionEvent.OnEventRaised += IsInteraction;

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
            _interactionEvent.OnEventRaised -= IsInteraction;

            _gameInputs.Inventory.Disable();
            _gameInputs.Gameplay.Disable();
            _gameInputs.UI.Disable();
        }

        internal void DisableAllInput()
        {
            _gameInputs.Gameplay.Disable();
            _gameInputs.Inventory.Disable();
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

        #region Gameplay Move(WASD), Attack/Interact(J), Dash( )
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (_interactionEnabled) { InteractEvent?.Invoke(); }
                else { AttackEvent?.Invoke(); }
            }
        }

        private void IsInteraction(bool val)
        {
            _interactionEnabled = (val) ? _interactionEnabled = true : _interactionEnabled = false;
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                DashEvent?.Invoke();
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

