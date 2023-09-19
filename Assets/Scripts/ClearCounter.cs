using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    /// <summary>
    /// Interact logic for the counter
    /// Spawns a kitchen object if kitchen object is not present
    /// </summary>
    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }
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
