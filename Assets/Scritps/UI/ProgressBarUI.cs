using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    
    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("Game Object " +hasProgressGameObject +" does not have a component that implements IHasProgress!");
        }
        hasProgress.OnProgressChanged += HasProgressOn_ProgressChanged;
        
        barImage.fillAmount = 0f;
        
        Hide();
    }

    private void HasProgressOn_ProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        barImage.fillAmount = e.progressNormalize;

        if (e.progressNormalize == 0f || e.progressNormalize == 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
