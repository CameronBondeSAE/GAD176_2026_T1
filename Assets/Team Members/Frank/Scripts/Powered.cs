using System;
using Frank;
using UnityEngine;

public class Powered : MonoBehaviour, IPowerable
{
   public bool isPowered = false;

   public void ActionWhenPowered()
   {
      if (isPowered == true)
      {
         gameObject.SetActive(false);
      }
      
      else if (isPowered == false)
      {
         gameObject.SetActive(true);
      }
   }
   
}
