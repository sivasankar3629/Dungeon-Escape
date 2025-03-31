using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;

    [SerializeField] protected int health;
    [SerializeField] protected float speed = 1;
    [SerializeField] protected int gems;
    
    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool isHit = false;
    protected PlayerMovement playerScript;

    [SerializeField] protected Transform pointA, pointB;
    
    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    
    public virtual void Movement()
    {
        // Waypoint
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }
        if ( !isHit ) // else combat
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        // Whether to get out of combat
        float distance = Vector3.Distance(transform.position, playerScript.transform.position);
        if ( distance > 2)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        // Flip in attack mode to player
        Vector3 direction = playerScript.transform.localPosition - transform.localPosition;
        if (anim.GetBool("InCombat"))
        {
            if (direction.x > 0)
            {
                sprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = true;
            }
        }
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_anim") && anim.GetBool("InCombat") == false)
        {
            return;
        }
        Movement();
    }
}
