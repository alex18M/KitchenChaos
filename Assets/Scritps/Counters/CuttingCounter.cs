using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
  public static event EventHandler OnAnyCut;

  new public static void ResetStaticData()
  {
    OnAnyCut = null;
  }
  
  
  public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;
  public event EventHandler OnCut; 


  [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

  private int cuttingProgress;
  
  public override void Interact(Player player)
  {
    if (!HasKitchenObjects())
    {
      // There is no KitchenObject here
      if (player.HasKitchenObjects())
      {
        // Player is carrying something
        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
          // Player carrying something that can be Cut
           player.GetKitchenObject().SetKitchenObjectParent(this);
           cuttingProgress = 0;
           
           CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSWithInput(GetKitchenObject().GetKitchenObjectSO());

           OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
           {
             progressNormalize =  (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
           });
        }
      }
      else
      {
        // Player not Carrying anything
      }
    }
    else
    {
      //There is a kitchenObject here
      if (player.HasKitchenObjects())
      {
        // Player is carrying something
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
          //Player is holding a Plate
          if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
          {
            GetKitchenObject().DestroySelf();
          }
        }
      }
      else
      {
        // Player is not Carrying anything
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }

  public override void InteractAlternate(Player player)
  {
    if (HasKitchenObjects() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
    {
      //There is a Kitchen Object here and it can be cut
      cuttingProgress++;
      
      OnCut?.Invoke(this, EventArgs.Empty);
      OnAnyCut?.Invoke(this,EventArgs.Empty);
      
      CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSWithInput(GetKitchenObject().GetKitchenObjectSO());
      
      OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
      {
        progressNormalize =  (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
      });

      if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
      {
        KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
      
        GetKitchenObject().DestroySelf();
     
        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
      }
    }
  }

  private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSWithInput(inputKitchenObjectSO);
    return cuttingRecipeSO != null;
  }
  private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
  {
    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSWithInput(inputKitchenObjectSO);
    
    if (cuttingRecipeSO != null)
    {
      return cuttingRecipeSO.output;
    }
    else
    {
      return null;
    }
  }

  private CuttingRecipeSO GetCuttingRecipeSWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
    {
      if (cuttingRecipeSO.input == inputKitchenObjectSO)
      {
        return cuttingRecipeSO;
      }
    }
    return null;
  }
}
