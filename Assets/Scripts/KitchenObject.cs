using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// Returns the KitchenObjectSO attached to the object
    /// </summary>
    /// <returns>KitchenObject Scriptable Object</returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
