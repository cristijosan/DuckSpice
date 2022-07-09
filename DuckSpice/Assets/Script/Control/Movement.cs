using System;
using UnityEngine;

namespace Script.Control
{
    public class Movement : MonoBehaviour
    {
        // Actions
        public static Action<float, float> ChangeVector;
        public static Action<bool> ChangeHideAction;
        public static Action ChangeCollectAction;
        // Components
        private Rigidbody rb;
        private Animator animator;
        // Local variables
        private Vector3 input;
        private bool isHide;
        private bool isCollect;
        // Attributes
        [Header("Attributes :")]
        public float speed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            // Actions
            ChangeVector += ChangeInputVector;
            ChangeHideAction += Hide;
            ChangeCollectAction += PickUp;
        }

        private void OnDestroy()
        {
            ChangeVector -= ChangeInputVector;
            ChangeHideAction -= Hide;
            ChangeCollectAction -= PickUp;
        }

        private void ChangeInputVector(float horizontal, float vertical)
        {
            input.x = horizontal;
            input.y = vertical;
            
            Move();
            SetAnimation();
        }

        // Move
        private void Move()
        {
            if (isHide)
                return;

            var direction = new Vector3(input.x, 0f, input.y);

            if (direction.magnitude >= .1f)
            {
                var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
                var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                rb.velocity = new Vector3(moveDir.normalized.x * speed, rb.velocity.y, moveDir.normalized.z * speed);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }

        // Set animation value for animator
        private void SetAnimation()
        {
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
            animator.SetFloat("Speed", input.sqrMagnitude);

            if (input.x > 0 || input.x < 0 || input.y > 0 || input.y < 0)
            {
                animator.SetFloat("LastHorizontal",  animator.GetFloat("Horizontal"));
                animator.SetFloat("LastVertical", animator.GetFloat("Vertical"));
            }
        }

        // Action for Hide and Unhide
        private void Hide(bool state) {
            if (state)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                animator.SetBool("IsHide", true);
                isHide = true;
            }
            else
            {
                animator.SetBool("IsHide", false);
                isHide = false;
            }
        }

        // Pick Up something or interact with other object's
        private void PickUp()
        {
            animator.SetTrigger("DoCollect");
        }
    }
}