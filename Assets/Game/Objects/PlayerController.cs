using System;
using Pain.Physics.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Objects
{
    [RequireComponent(typeof(PhysicsObject))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float m_moveForce = 10f;
        
        private PhysicsObject m_playerObj;
        
        private InputAction m_moveAction;

        private Vector2 m_input;

        private void Awake()
        {
            m_moveAction = InputSystem.actions["Player/Move"];
            
            m_playerObj = GetComponent<PhysicsObject>();
        }

        private void OnEnable()
        {
            m_moveAction.Enable();
            m_moveAction.performed += UpdateInput;
            m_moveAction.canceled += UpdateInput;
        }

        private void OnDisable()
        {
            m_moveAction.Disable();
            m_moveAction.performed -= UpdateInput;
            m_moveAction.canceled -= UpdateInput;
        }

        private void FixedUpdate()
        {
            if (m_input != Vector2.zero)
                Move();
        }

        private void UpdateInput(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            m_input = input;
        }

        private void Move()
        {
            m_playerObj.AddForce(m_input * m_moveForce);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawRay(m_playerObj.transform.position, m_playerObj.Velocity);
        }
    }
}