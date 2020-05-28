using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ModifierBehaviour))]
public class SharedNetworkModifier : MonoBehaviour
{
    private ModifierBehaviour modifierBehaviour;
    [SerializeField] private UnityEvent onModify;

    private PhotonView photonView;

    private void Awake()
    {
        modifierBehaviour = GetComponent<ModifierBehaviour>();
        photonView = GetComponent<PhotonView>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PhotonView photonView))
        {
            if (photonView.IsMine)
            {
                Debug.Log("Take Cell");
                modifierBehaviour.Use(other.gameObject);
                this.photonView.RPC(nameof(Modify),RpcTarget.All);
            }
            else
            {
                Debug.Log("Do not take cell");
            }
        }
    }

    [PunRPC]
    private void Modify()
    {
        onModify?.Invoke();
    }
}
