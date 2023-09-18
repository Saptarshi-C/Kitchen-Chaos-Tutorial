using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    /// <summary>
    /// Returns the KitchenObjectSO attached to the object
    /// </summary>
    /// <returns>KitchenObject Scriptable Object</returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    /// <summary>
    /// Move object to new counter
    /// </summary>
    /// <param name="clearCounter">The new counter to move object to</param>
    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter !=null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter is full");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Get the clear counter the object is on
    /// </summary>
    /// <returns>The counter the object is on</returns>
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
