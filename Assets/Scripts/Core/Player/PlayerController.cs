using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{


    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("General")]
        PlayerControllerState state = PlayerControllerState.Idle;

        [Header("Walking")]
        [SerializeField]
        float baseMoveSpeed = 5;
        [SerializeField]
        float groundMultiplier = 2,
            groundDrag = 4,
            stillDrag = 10;
        Vector3 moveVector = Vector3.zero;

        [Header("Stickiness")]
        [SerializeField]
        [Tooltip("Empty transform that denotes feet of character.")]
        Transform playerFeet;
        [SerializeField]
        LayerMask walkableMask;
        float checkDistance = 1.25f;
        RaycastHit hitInfo = new RaycastHit();

        //[Header("Events")]
        public delegate void OnPlayerControllerEvent();
        /// <summary>
        /// Fires when the player moves
        /// </summary>
        public static OnPlayerControllerEvent OnPlayerMove;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            GetInputMoveVec();
            ControlDrag();
            UpdateState();
        }

        private void FixedUpdate()
        {
            ApplyForce();
            FireStateEvents();
            //StickToGround();
        }

        /// <summary>
        /// Updates player controller state
        /// </summary>
        private void UpdateState()
        {
            if (moveVector.magnitude != 0)
            {
                state = PlayerControllerState.Moving;
            }
            else
            {
                state = PlayerControllerState.Idle;
            }
        }

        private void FireStateEvents()
        {
            if (state == PlayerControllerState.Moving)
            {
                OnPlayerMove?.Invoke();
            }
        }

        private void GetInputMoveVec()
        {
            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        }

        private void ControlDrag()
        {
            if (state == PlayerControllerState.Idle)
            {
                rb.drag = stillDrag;
            }
            else if (state == PlayerControllerState.Moving)
            {
                rb.drag = groundDrag;
            }
        }

        private void ApplyForce()
        {
            rb.AddForce(moveVector.normalized * baseMoveSpeed * groundMultiplier, ForceMode.Impulse);
        }

        /// <summary>
        /// Keeps the player stuck to the ground
        /// </summary>
        private void StickToGround()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, checkDistance, walkableMask))
            {
                transform.position = hitInfo.point + (playerFeet.transform.position - transform.position);
            }
        }

        private enum PlayerControllerState
        {
            Idle,
            Moving
        }
    }
}