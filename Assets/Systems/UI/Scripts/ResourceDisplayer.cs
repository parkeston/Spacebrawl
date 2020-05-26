using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDisplayer : MonoBehaviour
{
   [SerializeField] private Stat stat; //todo: serialize through interface IStat
   [SerializeField] private UIResource uiResourcePrefab;
   [SerializeField] private GameObject canvasPrefab;
   [SerializeField] private Vector3 displayPositionOffset;

   private static GameObject canvas;
   private UIResource uiResource;

   private new Camera camera;
   
   private void Awake()
   {
      if (canvas == null)
         canvas = Instantiate(canvasPrefab);

      uiResource = Instantiate(uiResourcePrefab, canvas.transform, false);
      camera = Camera.main;

      stat.OnValueChanged += uiResource.UpdateResourceValue;
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
