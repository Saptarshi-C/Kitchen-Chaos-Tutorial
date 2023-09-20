using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected Transform counterTopPoint;

    protected KitchenObject kitchenObject;


    public virtual void Interact(Player player)
    {

    }

    /// <summary>
    /// Get the clear counter counter top point
    /// </summary>
    /// <returns>Counter Top Point</returns>
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    /// <summary>
    /// Sets the kitchen object on the counter
    /// </summary>
    /// <param name="kitchenObject">Kitchen object to be set</param>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    /// <summary>
    /// Gets the kitchen object on the counter
    /// </summary>
    /// <returns>Kitchen object on the counter</returns>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// Removes kitchen object from counter
    /// </summary>
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    /// <summary>
    /// Check if counter is occupied by kitchen object
    /// </summary>
    /// <returns>True if kitchen object is present</returns>
    public bool HasKitchenObject()
    {
        return (kitchenObject != null);
    }
}
