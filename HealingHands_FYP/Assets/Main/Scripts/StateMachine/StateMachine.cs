using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private StateSO _initialState;

    public State _currentState { get; set; }
    private Dictionary<Type, Component> _cachedComponent = new Dictionary<Type, Component>();
    public Dictionary<ScriptableObject, object> _createdInstances = new Dictionary<ScriptableObject, object>();

    public string CurrentStateName;
    void Start()
    {
        _currentState = _initialState.GetState(this, _createdInstances);
        _currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnUpdate();  
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
        _currentState.OnStateExit();
        Debug.Log($"From {_currentState._originSO.name} to {transitionState._originSO.name}");
        _currentState = transitionState;
        CurrentStateName = _currentState._originSO.name;
        _currentState.OnStateEnter();
    }
}
