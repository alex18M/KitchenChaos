using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
   private Player _player;
   private float footstepTimer;
   private float footstepTimerMax = .1f;

   private void Awake()
   {
      _player = GetComponent<Player>();
   }

   private void Update()
   {
      footstepTimer -= Time.deltaTime;
      if (footstepTimer < 0f)
      {
         footstepTimer = footstepTimerMax;

         if (_player.IsWalking())
         {
            float volume = 1f;
            SoundManager.Instance.PlayFootstepsSound(_player.transform.position, volume); 
         }
        
      }
   }
}
