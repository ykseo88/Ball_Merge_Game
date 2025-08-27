using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shake : MonoBehaviour
{
   public float shakeVolume;
   private Vector3 originalPos;
   public bool isShake =false;

   private void Update()
   {
      OnShake();
   }

   private void OnShake()
   {
      if (isShake)
      {
         isShake = false;
         originalPos = transform.position;
      }
      Vector3 randpoint = new Vector3(Random.Range(-shakeVolume, shakeVolume), Random.Range(-shakeVolume, shakeVolume),  0);
      transform.position =  originalPos + randpoint;
   }
}
