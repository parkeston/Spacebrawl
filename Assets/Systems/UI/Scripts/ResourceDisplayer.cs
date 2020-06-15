using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class ResourceDisplayer : MonoBehaviour
{
   [SerializeField] private Stat stat; //todo: serialize through interface IStat
   [SerializeField] private UIResource uiResourcePrefab;
   [SerializeField] private GameObject canvasPrefab;
   [SerializeField] private Vector3 displayPositionOffset;

   [SerializeField] private Color allyColor;
   [SerializeField] private Color enemyColor;

   private static GameObject canvas;
   private UIResource uiResource;

   private new Camera camera;


   public Stat DisplayedStat => stat;
   
   private void Awake()
   {
      if (canvas == null)
         canvas = Instantiate(canvasPrefab);
      
      uiResource = Instantiate(uiResourcePrefab, canvas.transform, false);
      uiResource.SetResourceDisplayColor(
         PhotonNetwork.LocalPlayer.GetPhotonTeam() == PhotonView.Get(this).Controller.GetPhotonTeam()
            ? allyColor
            : enemyColor);

      camera = Camera.main;
   }
   
   public void UpdateResourceDisplay(float value, float maxValue)
   {
         uiResource.UpdateResourceValue(value, maxValue);
   }

   private void FixedUpdate()
   {
      Vector3 screenPosition = camera.WorldToScreenPoint(transform.position + displayPositionOffset);
      uiResource.UpdatePosition(screenPosition);
   }

   private void OnEnable()
   {
      if(uiResource)
         uiResource.gameObject.SetActive(true);
   }

   private void OnDisable()
   {
      if(uiResource)
         uiResource.gameObject.SetActive(false);
   }
}
