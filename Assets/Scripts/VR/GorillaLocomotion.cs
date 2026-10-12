using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class GorillaLocomotion : MonoBehaviour
{
    [Header("Rig References")]
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float climbSpeed = 5.5f;
    [SerializeField] private float jumpForce = 5.5f;
    [SerializeField] private float maxVelocity = 8f;
    [SerializeField] private float handReachDistance = 0.45f;

    [Header("Climb Settings")]
    [SerializeField] private LayerMask climbLayers;
    [SerializeField] private float climbRayLength = 0.55f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool leftGripHeld;
    private bool rightGripHeld;
    private bool climbing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (head == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
                head = mainCamera.transform;
        }
    }

    private void Update()
    {
        leftGripHeld = ReadGripState(XRNode.LeftHand);
        rightGripHeld = ReadGripState(XRNode.RightHand);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }

        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                leftGripHeld = true;
                rightGripHeld = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 inputDirection = GetMovementInput();
        float speed = climbing ? climbSpeed : moveSpeed;

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Vector3 moveVector = head.forward * inputDirection.z + head.right * inputDirection.x;
            moveVector.y = 0f;
            moveVector = Vector3.ClampMagnitude(moveVector, 1f);

            if (climbing)
            {
                rb.velocity = new Vector3(moveVector.x * speed, rb.velocity.y, moveVector.z * speed);
            }
            else
            {
                rb.velocity = new Vector3(moveVector.x * speed, rb.velocity.y, moveVector.z * speed);
            }
        }
        else
        {
            if (!climbing)
            {
                Vector3 velocity = rb.velocity;
                velocity.x *= 0.92f;
                velocity.z *= 0.92f;
                rb.velocity = new Vector3(velocity.x, velocity.y, velocity.z);
            }
        }

        HandleClimbing();
    }

    private void HandleClimbing()
    {
        if (!leftGripHeld && !rightGripHeld)
        {
            climbing = false;
            return;
        }

        Vector3 originLeft = leftHand != null ? leftHand.position : transform.position;
        Vector3 originRight = rightHand != null ? rightHand.position : transform.position;

        bool leftCloseToWall = Physics.Raycast(originLeft, transform.forward, climbRayLength, climbLayers);
        bool rightCloseToWall = Physics.Raycast(originRight, transform.forward, climbRayLength, climbLayers);

        if (leftCloseToWall || rightCloseToWall)
        {
            climbing = true;
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -1f, 1f), rb.velocity.z);
            rb.AddForce(Vector3.up * climbSpeed * 0.25f, ForceMode.Acceleration);
        }
        else
        {
            climbing = false;
            rb.useGravity = true;
        }
    }

    private Vector3 GetMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Application.isEditor)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        return new Vector3(horizontal, 0f, vertical);
    }

    private bool ReadGripState(XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed))
            {
                return gripPressed;
            }
        }

        return false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 40f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
