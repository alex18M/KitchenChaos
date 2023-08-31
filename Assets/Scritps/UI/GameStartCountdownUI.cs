using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameStartCountdownUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI countdownText;

  private void Start()
  {
    KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    
    Hide();
  }

  private void Update()
  {
      countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
  }

  private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
  {
      if (KitchenGameManager.Instance.IsCountdownToStartActive())
      {
          Show();
      }
      else
      {
          Hide();
      }
  }

  private void Show()
  {
      gameObject.SetActive(true);
  }

  private void Hide()
  {
      gameObject.SetActive(false);
  }

}
