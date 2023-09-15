using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameInput gameInput;
    [SerializeField] float playerRadius = 0.7f;
    [SerializeField] float playerHeight = 2f;

    private bool isWalking = false;

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
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
        if(!canMove)
        {
            // Check if X direction is blocked
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            
            if(canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Check if Z direction is blocked
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMove)
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
    /// Used to return if the player is walking to other classes
    /// </summary>
    /// <returns>True if the player is walking</returns>
    public bool IsWalking()
    {
        return isWalking;
    }
}
