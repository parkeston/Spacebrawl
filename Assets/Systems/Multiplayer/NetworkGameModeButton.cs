using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NetworkGameModeButton : MonoBehaviour
{
    [SerializeField] private NetworkInitializer networkInitializer;
    
    [SerializeField] private UnityEvent OnConnecting;
    [SerializeField] private UnityEvent OnConnectedSuccess;
    [SerializeField] private UnityEvent OnConnectedFailed;

    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>networkInitializer.ConnectToServer(this));
    }

    public void Connecting() => OnConnecting?.Invoke();
    public void ConnectingSuccess() => OnConnectedSuccess?.Invoke();
    public void ConnectingFailed() => OnConnectedFailed?.Invoke();
}
