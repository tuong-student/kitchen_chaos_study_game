using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = gameInput.MovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        bool canMove;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerSize = 0.7f;
        float playerRadius = 2f;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerRadius, playerSize, moveDir, moveDistance);

        if (!canMove)
        {
            //Cannot move toward moveDir
            //Try to move on X direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerRadius, playerSize, moveDirX, moveDistance);

            if(canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move on the X
                //Try to move on the Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerRadius, playerSize, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can move on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move any direction
                }
            }
        }

        if(canMove)
            transform.position += moveDir * moveDistance;


        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
