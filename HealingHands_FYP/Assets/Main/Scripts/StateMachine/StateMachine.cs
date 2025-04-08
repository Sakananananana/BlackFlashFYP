using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem.LowLevel;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _initialState;

    public string CurrentStateName;
    private State _currentState { get; set; }
    private Dictionary<Type, Component> _cachedComponent = new Dictionary<Type, Component>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentState = _initialState;
        _currentState.OnStateEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnUpdate();
        CurrentStateName = _currentState.name;
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    //When entering new state no need to Get Component that already got in the previous state
    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponent.ContainsKey(typeof(T)))
        {
            return _cachedComponent[typeof(T)] as T; 
        }

        if (TryGetComponent<T>(out var component))
        {
            _cachedComponent.Add(typeof(T), component);
        }
        return component;
    }

    public void ChangeState(State transitionState)
    {
        Debug.Log($"Changing State: {_currentState?.name} => {transitionState.name}");
        _currentState.OnStateExit();
        _currentState = transitionState;
        _currentState.OnStateEnter(this);
    }
}
