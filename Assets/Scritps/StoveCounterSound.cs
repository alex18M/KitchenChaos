using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
   [SerializeField] private StoveCounter stoveCounter;
   private AudioSource audioSource;
   private float warningSoundTimer;
   private bool playWarningSound;

   private void Awake()
   {
      audioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
      stoveCounter.OnProgressChanged += StoveCounterOnOnProgressChanged;
   }

   private void StoveCounterOnOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
   {
      float burnShowProgressAmount = .3f;
      playWarningSound = stoveCounter.IsFried() && e.progressNormalize >= burnShowProgressAmount;
   }

   private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
   {
      bool playsound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
      if (playsound)
      {
         audioSource.Play();
      }
      else
      {
         audioSource.Pause();
      }
      
   }

   private void Update()
   {
      if (playWarningSound)
      {
         warningSoundTimer -= Time.deltaTime;
         if (warningSoundTimer <= 0f)
         {
            float warningSoundTimerMax = .2f;
            warningSoundTimer = warningSoundTimerMax;
            
            
            SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
         }
      }
     
      
   }
}
