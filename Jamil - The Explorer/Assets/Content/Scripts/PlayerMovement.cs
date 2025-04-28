using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //CharacterController controller;
    
    public VariableJoystick joystick;
    public CharacterController controller;
    [Range(0f, 10f)] public float movementSpeed;
    [Range(0f, 10f)] public float rotationSpeed;
    
    public Canvas inputCanvas;
    public bool isJoystick;

    public Animator playerAnimatior;

    //void Awake()
    //{
    //    controller = GetComponent<CharacterController>();
    //}

    private void Start()
    {
        EnableJoystickInput();
    }

    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(isJoystick)
        {
            var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            controller.SimpleMove(movementDirection * movementSpeed);

            if (movementDirection.sqrMagnitude <= 0)
            {
                playerAnimatior.SetBool("run", false);
                return;
            }
            playerAnimatior.SetBool("run", true);
            var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection,
                rotationSpeed * Time.deltaTime, 0.0f);

            controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
