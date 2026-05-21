using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float thrustPower = 12f;
    [SerializeField] private float rotationSpeed = 220f;
    [SerializeField] private float maxSpeed = 18f;

    [Header("Visuals (optional)")]
    [SerializeField] private ParticleSystem thrustParticles;

    private Rigidbody2D rb;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private bool isThrusting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Perfect space physics
        rb.drag = 0.85f;
        rb.angularDrag = 3f;
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void OnEnable()
    {
        if (inputActions == null)
            inputActions = new PlayerInputActions();

        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Thrust.performed += ctx => isThrusting = true;
        inputActions.Player.Thrust.canceled += ctx => isThrusting = false;
    }

    private void OnDisable()
    {
        inputActions?.Player.Disable();
    }

    private void FixedUpdate()
    {
        // === THRUST ===
        if (isThrusting)
        {
            Vector2 thrustDir = transform.up;
            rb.AddForce(thrustDir * thrustPower);

            // Cap max speed
            if (rb.velocity.magnitude > maxSpeed)
                rb.velocity = rb.velocity.normalized * maxSpeed;

            if (thrustParticles != null) thrustParticles.Play();
        }
        else if (thrustParticles != null)
        {
            thrustParticles.Stop();
        }

        // === ROTATION ===
        float rotationInput = -moveInput.x;
        float torque = rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.AddTorque(torque);
    }

    public Vector2 GetVelocity() => rb.velocity;
}