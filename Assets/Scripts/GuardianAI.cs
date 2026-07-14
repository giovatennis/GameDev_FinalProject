using UnityEngine;

public class GuardianAI : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public Transform player;
    public Animator animator;

    public float detectionRange = 15f;
    public float attackRange = 1.5f;
    public float moveSpeed = 3.5f;
    public int damagePerHit = 20;

    public Renderer eyeRenderer;
    public Color dormantColor = Color.gray;
    public Color activeColor = Color.red;

    [Header("Ground Snapping (for sloped terrain)")]
    public LayerMask groundLayers;
    public float groundCheckHeight = 5f;

    public AudioSource awakenSound;

    private enum State { Dormant, Hunting, Returning }
    private State state = State.Dormant;

    private Vector3 postPosition;
    private Quaternion postRotation;

    private float attackCooldown = 1.2f;
    private float attackTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        postPosition = transform.position;
        postRotation = transform.rotation;
        SetDormantVisual();
        SetAnimSpeed(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(dayNightCycle == null || player == null)
        {
            return;
        }

        SnapToGround();

        if(!dayNightCycle.isNight)
        {
            if(state != State.Dormant)
            {
                GoDormant();
            }
            return;
        }

        // it's night from here down
        if(state == State.Dormant)
        {
            Awaken();
        }

        if(attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(state == State.Returning)
        {
            if(distanceToPlayer <= detectionRange)
            {
                state = State.Hunting;
            }
            else
            {
                ReturnToPost();
            }
        }
        else if(state == State.Hunting)
        {
            if(distanceToPlayer > detectionRange)
            {
                state = State.Returning;
            }
            else
            {
                ChasePlayer(distanceToPlayer);
            }
        }
    }

    private void ChasePlayer(float distance)
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if(direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }

        if(distance > attackRange)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            SetAnimSpeed(moveSpeed);
        }
        else
        {
            SetAnimSpeed(0f); // stand still while in attack range

            if(attackTimer <= 0f)
            {
                AttackPlayer();
            }
        }
    }

    private void ReturnToPost()
    {
        Vector3 direction = postPosition - transform.position;
        direction.y = 0f;

        if(direction.magnitude <= 0.1f)
        {
            transform.position = postPosition;
            transform.rotation = postRotation;
            SetAnimSpeed(0f);
            return;
        }

        transform.rotation = Quaternion.LookRotation(direction.normalized);
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        SetAnimSpeed(moveSpeed);
    }

    private void AttackPlayer()
    {
        attackTimer = attackCooldown;
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if(health != null)
        {
            health.TakeDamage(damagePerHit);
        }
    }

    private void Awaken()
    {
        state = State.Returning; // starts "returning" so it immediately checks range from its post
        SetActiveVisual();
        if(awakenSound != null)
        {
            awakenSound.Play();
        }
    }

    private void GoDormant()
    {
        state = State.Dormant;
        transform.position = postPosition;
        transform.rotation = postRotation;
        SetDormantVisual();
        SetAnimSpeed(0f);
    }

    private void SetDormantVisual()
    {
        if(eyeRenderer != null)
        {
            eyeRenderer.material.color = dormantColor;
        }
    }

    private void SetActiveVisual()
    {
        if(eyeRenderer != null)
        {
            eyeRenderer.material.color = activeColor;
        }
    }

    private void SnapToGround()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * groundCheckHeight;
        if(Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, groundCheckHeight * 2f, groundLayers))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y;
            transform.position = pos;
        }
    }

    private void SetAnimSpeed(float speed)
    {
        if(animator != null)
        {
            animator.SetFloat("Speed", speed);
            animator.SetFloat("MotionSpeed", speed > 0f ? 1f : 0f);
        }
    }
}