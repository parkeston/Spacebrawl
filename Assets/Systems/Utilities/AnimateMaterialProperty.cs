using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AnimateMaterialProperty : MonoBehaviour
{
   [SerializeField] private float delay;
   [SerializeField] private float duration;
   [SerializeField] private string propertyName;

   private Material material;

   private void Awake()
   {
      material = GetComponent<Renderer>().material;
   }

   private void OnEnable()
   {
      StartCoroutine(AnimateProperty());
   }

   private IEnumerator AnimateProperty()
   {
      float speed = 1 / duration;
      float t = 0;
      
      yield return new WaitForSeconds(delay);
      
      while (t<1)
      {
         t += speed * Time.deltaTime;
         material.SetFloat(propertyName,t);
         yield return null;
      }
   }
}
