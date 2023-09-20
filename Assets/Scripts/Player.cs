using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    // Singleton 
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask countersLayerMask;

    [SerializeField] private Transform KitchenObjectHoldPoint;

    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;

    private KitchenObject kitchenObject;

    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
    {
        // Set singleton
        if(Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Start is called before first frame
    /// </summary>
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    /// <summary>
    /// Used to return if the player is walking to other classes
    /// </summary>
    /// <returns>True if the player is walking</returns>
    public bool IsWalking()
    {
        return isWalking;
    }

    /// <summary>
    /// Gets a normalized input vector
    /// Checks for blocking objects
    /// Splits movement into component if blocking objects are present and player is moving
    /// Rotates player towards move direction
    /// </summary>
    private void HandleMovement()
    {
        // Get input vector and convert it to a Vector3
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = Time.deltaTime * moveSpeed;

        // If the move direction is set and a non-zero vector, player is walking
        isWalking = (moveDir != Vector3.zero);

        // Find if there is anything blocking the path
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // If player cannot move in move direction, check for individual components
        if (!canMove)
        {
            // Check if X direction is blocked
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Check if Z direction is blocked
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    // Player cannot move
                }
            }

        }

        // If there isn't anything blocking the path, player can move
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        // Rotate the player towards the move direction
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    /// <summary>
    /// Use raycast to determine if player hit object.
    /// </summary>
    private void HandleInteractions()
    {
        // Get input vector and convert it to a Vector3
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Set the last interact direction as long as the movedir is set
        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        // Fire a ray towards last interact direction to determine if it hit something in a particular layer.
        // If it hits something return the hit information as a RaycastHit object.
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            // Try to get the ClearCounter component from the object
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else // If there is no clearcounter component, it is not a clear counter and no counter is selected
            {
                SetSelectedCounter(null);
            }
        }
        else // If ray does not hit anything no counter is selected
        {
            SetSelectedCounter(null);
        }
    }

    /// <summary>
    /// Sets selectedcounter to the selected object
    /// Invokes the event for selected counter changed event
    /// </summary>
    /// <param name="selectedCounter">The selected object</param>
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    /// <summary>
    /// Get the clear counter counter top point
    /// </summary>
    /// <returns>Counter Top Point</returns>
    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
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
