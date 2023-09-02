using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
  [SerializeField] private StoveCounter _stoveCounter;

  private void Start()
  {
    _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    Hide();
  }

  private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
  {
    float burnShowProgressAmount = .3f;
    bool show = _stoveCounter.IsFried() && e.progressNormalize >= burnShowProgressAmount;

    if (show)
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
