using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GrabItem))]
public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float movementSpeed_;
    [SerializeField] private float acceleration_;
    [SerializeField] private float damping_;

    private float currentSpeed;
    public GameObject currentItem;
    private float XInput;
    private float YInput;

    private bool isRightSideMovement = false;
    private bool lastSideMovementIsRight = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator animator;

    private bool isMovementLocked = false;
    public bool isInMinigameNow = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        //Debug.LogError(isInMinigameNow + " " + isMovementLocked); // 2 тру
        if (!isMovementLocked)
        {
            XInput = Input.GetAxis("Horizontal") * movementSpeed_;
            YInput = Input.GetAxis("Vertical") * movementSpeed_;


            // Animation
            bool isMoving = rb.velocity.magnitude > 0.15f;
            animator.SetBool("IsMoving", isMoving);
            isRightSideMovement = XInput > 0;

            if (XInput != 0)
            {
                if (isRightSideMovement)
                {
                    sr.flipX = true;
                    if (currentItem != null)
                        currentItem.GetComponent<SpriteRenderer>().flipX = true;

                }
                else
                {
                    sr.flipX = false;
                    if (currentItem != null)
                        currentItem.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            //
            Vector3 movement = new Vector3(XInput * movementSpeed_, YInput * movementSpeed_, 0);
            if (currentItem != null)
                movement /= 2f;

            rb.velocity = movement;
        }
        else
        {
            animator.SetBool("IsMoving", false);
            rb.velocity = Vector2.zero;
        }
    }

    public void LockMovement(bool isLocked)
    {
        isMovementLocked = isLocked;
    }

    private void Update()
    {
        if (!isMovementLocked)
        {
            currentItem = GetComponent<GrabItem>().currentItem;

            if (Input.GetKey(KeyCode.E))
            {
                if (currentItem != null)
                    currentItem.GetComponent<IItem>().OnUse();
                else
                    GameEvents.current.HitInteract();
            }
            if (Input.GetKeyUp(KeyCode.G))
                GameEvents.current.HitGrab();
        }
    }
}
