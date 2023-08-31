using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
   public event EventHandler<OnIngredienteAddedEventArgs> OnIngredientAdded;

   public class OnIngredienteAddedEventArgs : EventArgs
   {
      public KitchenObjectSO kitchenObjectSO;
   }

   [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

   private List<KitchenObjectSO> kitchenObjectSOList;

   private void Awake()
   {
      kitchenObjectSOList = new List<KitchenObjectSO>();
   }

   public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
   {
      if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
      {
         //Not a Valid ingredient
         return false;
      }

      if (kitchenObjectSOList.Contains(kitchenObjectSO))
      {
         //Already hats this type
         return false;
      }
      else
      {
         kitchenObjectSOList.Add(kitchenObjectSO);

         OnIngredientAdded?.Invoke(this, new OnIngredienteAddedEventArgs()
         {
            kitchenObjectSO = kitchenObjectSO
         });

         return true;
      }

   }
   
   public List<KitchenObjectSO> GetKitchenObjectSOList()
   {
      return kitchenObjectSOList;
   }

}
