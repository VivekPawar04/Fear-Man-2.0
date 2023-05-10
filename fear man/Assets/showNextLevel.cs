using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showNextLevel : MonoBehaviour
{
   public GameObject NextBtn;

   private void OnTriggerEnter(Collider other)
   {
    if (other.CompareTag("Player"))
       NextBtn.SetActive(true);

   }

   public void GoToNextLevel()
   {
    GoogleAdMobController.AdmobManager.ShowInterstitialAd();

   }
}
