using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float gravity = 9.8f;

    private float horizontalMove; // eje X  A,D
    private float verticalMove; // eje Z    W,S
    private Vector3 playerInput;

    private CharacterController player;
    private Vector3 playerMove;
    private Vector3 playerRotation;
    public float playerSpeed;
    public float playerRotationSpeed;
    public float playerSlideSpeed;
    

    private float fallSpeed;
    public float jumpForce;

    //camera
    public Camera mainCamera;
    public Vector3 camForward;
    public Vector3 camRight;

    private bool isOnSlope = false;
    private Vector3 hitVectorNormal;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = Vector3.ClampMagnitude( new Vector3(horizontalMove, 0, verticalMove), 1);
        camDirection();

        playerMove = playerInput.z * camForward;
        playerMove *= playerSpeed;
        playerRotation = playerInput.x * camRight;
        playerRotation *= playerRotationSpeed;
        player.transform.LookAt(player.transform.position + camForward + playerRotation);

        setGravity();
        playerSkills();
        player.Move(playerMove * Time.deltaTime);
    }

    public void camDirection() {

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void setGravity() {
        if (player.isGrounded) {
            fallSpeed = -gravity * Time.deltaTime;
            playerMove.y = fallSpeed;
        } else {
            fallSpeed -= gravity * Time.deltaTime;
            playerMove.y = fallSpeed;
        }

        slideDown();
    }

    public void playerSkills() {
        if (player.isGrounded && Input.GetButtonDown("Jump")) {
            fallSpeed = jumpForce;
            playerMove.y = fallSpeed;
        }
    }

    public void slideDown() {
        isOnSlope = Vector3.Angle(Vector3.up, hitVectorNormal) >= player.slopeLimit;
        if (isOnSlope) {
            playerMove.x += hitVectorNormal.x * playerSlideSpeed;
            playerMove.z += hitVectorNormal.z * playerSlideSpeed;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        hitVectorNormal = hit.normal;
    }
}
