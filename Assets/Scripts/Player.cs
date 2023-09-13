using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameInput gameInput;

    private bool isWalking = false;

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = (moveDir != Vector3.zero);

        transform.position += moveDir * Time.deltaTime * moveSpeed;
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
