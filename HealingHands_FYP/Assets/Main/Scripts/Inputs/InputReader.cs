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
        public Action OpenShopEvent;
        public Action CloseShopEvent;
        public Action OpenWeaponEvent;
        public Action OpenProtagonistRoomEvent;
        public Action ReturnEvent;
        public Action CloseWeaponEvent;
        public Action PressedEvent;
        public Action PauseEvent;
        public Action ResumeEvent;

        float _holdTime;

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

        #region Gameplay Move(WASD), Attack/Interact(J), Dash( ), 
        private void IsInteraction(bool val)
        {
            _interactionEnabled = (val) ? _interactionEnabled = true : _interactionEnabled = false;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (_interactionEnabled) { InteractEvent?.Invoke(); }
                else { AttackEvent?.Invoke(); }
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                DashEvent?.Invoke();
            }
        }

        public void OnReturn(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                _holdTime = Time.time;
            }

            switch (context.phase)
            { 
                case InputActionPhase.Performed:
                    {
                        ReturnEvent?.Invoke();
                        Debug.Log("Held for 2 Seconds!");
                        break;
                    }
                    

                case InputActionPhase.Canceled:
                    {
                        float heldDuration = Time.time - _holdTime;
                        Debug.Log(heldDuration);
                        break;
                    } 
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

        public void OnOpenShop(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("Open Shop event triggered");
                OpenShopEvent?.Invoke();
                SetUI(); // Optional: switch to inventory input map if needed
            }
        }

        public void OnCloseShop(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                CloseShopEvent?.Invoke();
                SetGameplay(); // Optional: switch to inventory input map if needed
            }
        }
        public void OnOpenWeapon(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("Open Weapon event triggered");
                OpenWeaponEvent?.Invoke();
                SetUI(); // Optional: switch to inventory input map if needed
            }
        }

        public void OnCloseWeapon(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                CloseWeaponEvent?.Invoke();
                SetGameplay(); // Optional: switch to inventory input map if needed
            }
        }

        public void OnOpenProtagonistRoom(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                OpenProtagonistRoomEvent?.Invoke();
            }
        }

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

