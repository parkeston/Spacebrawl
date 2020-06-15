using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class NetworkGameObject : MonoBehaviour
{
   private PhotonView photonView;
   private new Collider collider;

   [SerializeField] private bool disableColliderIfNotLocal;
   [SerializeField] private MonoBehaviour[] componentsToDisableIfNotLocal;
   
   private Transform parent;
   private Vector3 localPosition;

   
   private void Awake()
   {
      photonView = PhotonView.Get(this);
      collider = GetComponent<Collider>();

      if (!photonView.IsMine && PhotonNetwork.IsConnected)
      {
         if(disableColliderIfNotLocal) 
            collider.enabled = false;
         foreach (var component in componentsToDisableIfNotLocal)
         {
            component.enabled = false;
         }
      }
   }

   private void OnEnable()
   {
      if(photonView.IsMine)
         Activate(true,transform.position,transform.rotation);
   }

   private void OnDisable()
   {
      //if (photonView.IsMine)
      //{
         Activate(false,transform.position,transform.rotation);
      //}
   }

   private void OnDestroy()
   {
      photonView.RPC(nameof(DestroyOnNetwork),RpcTarget.Others);
   }

   public void SetParent(Transform parent, Vector3 localPosition)
   {
      this.parent = parent;
      this.localPosition = localPosition;
      photonView.RPC(nameof(SetParentOnNetwork),RpcTarget.All);
   }

   [PunRPC]
   private void SetParentOnNetwork()
   {
      transform.SetParent(parent);
      transform.localPosition = localPosition;
   }
   
   public void Activate(bool value,Vector3 position,Quaternion rotation)
   {
      if (photonView == null)
         photonView = GetComponent<PhotonView>();
      
      photonView.RPC(nameof(ActivateOnNetwork),RpcTarget.All,value,position,rotation);
   }

   [PunRPC]
   private void ActivateOnNetwork(bool value,Vector3 position, Quaternion rotation)
   {
      transform.position = position;
      transform.rotation = rotation;
      gameObject.SetActive(value);
   }

   [PunRPC]
   private void DestroyOnNetwork()
   {
      Destroy(gameObject);
   }
}
