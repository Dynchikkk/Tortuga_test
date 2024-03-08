using EasyCharacterMovement;
using UnityEngine;

namespace QaTestCace.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public float rotationRate = 540.0f;

        public float maxSpeed = 5;

        public float acceleration = 20.0f;
        public float deceleration = 20.0f;

        public float groundFriction = 8.0f;
        public float airFriction = 0.5f;

        public float jumpImpulse = 6.5f;

        [Range(0.0f, 1.0f)]
        public float airControl = 0.3f;

        public Vector3 gravity = Vector3.down * 9.81f;

        private CharacterMovement _characterMovement;

        private Vector3 _movementDirection;
        private float _horizontal;
        private float _vertical;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();

        }

        public void SetHorizontal(float horizontal)
        {
            _horizontal = horizontal;
        }

        public void SetVertical(float vertical)
        {
            _vertical = vertical;
        }
        public void Jump()
        {
            if (!_characterMovement.isGrounded) {
                return;
            }
            _characterMovement.PauseGroundConstraint();
            _characterMovement.LaunchCharacter(Vector3.up * jumpImpulse, true);
        }
        
        

        private void Update()
        {
            

            // Create a movement direction vector (in world space)

            _movementDirection = Vector3.zero;
            _movementDirection += Vector3.forward * _vertical;
            _movementDirection += Vector3.right * _horizontal;

            // Make Sure it won't move faster diagonally

            _movementDirection = Vector3.ClampMagnitude(_movementDirection, 1.0f);


            // Rotate towards movement direction

            _characterMovement.RotateTowards(_movementDirection, rotationRate * Time.deltaTime);

            // Perform movement

            Vector3 desiredVelocity = _movementDirection * maxSpeed;

            float actualAcceleration = _characterMovement.isGrounded ? acceleration : acceleration * airControl;
            float actualDeceleration = _characterMovement.isGrounded ? deceleration : 0.0f;

            float actualFriction = _characterMovement.isGrounded ? groundFriction : airFriction;

            _characterMovement.SimpleMove(desiredVelocity, maxSpeed, actualAcceleration, actualDeceleration, actualFriction, actualFriction, gravity);
            // _horizontal = 0;
            // _vertical = 0;
        }
    }
}