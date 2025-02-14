using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _initialState;
    private BaseState _currentState { get; set; }

    private Dictionary<Type, Component> _cachedComponent = new Dictionary<Type, Component>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _currentState = _initialState;
        _currentState.StateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.StateFixedUpdate();
    }

    //When entering new state no need to Get Component that already got in the previous state
    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponent.ContainsKey(typeof(T)))
        {
            return _cachedComponent[typeof(T)] as T; 
        }

        var component = base.GetComponent<T>();
        if (component != null) 
        {
            _cachedComponent.Add(typeof(T), component);
        }

        return component;
    }

}
