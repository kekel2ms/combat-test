using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    public Vector2 GetMouseAxis()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxis(InputConst.MouseX), Input.GetAxis(InputConst.MouseY));
        return mouseAxis;
    }

    public bool IsAttackInputDown()
    {
        return Input.GetButtonDown("Attack");
    }

    public Vector3 GetMovementInput()
    {
        //Vertical
        Vector3 forward = _camera.transform.forward;
        forward.y = 0;
        Vector3 verticalMovement = forward.normalized * Input.GetAxis(InputConst.Vertical);

        //Horizontal
        Vector3 horizontalMovement = _camera.transform.right * Input.GetAxis(InputConst.Horizontal);

        //Input Movement
        return (verticalMovement + horizontalMovement).normalized;
    }
}
