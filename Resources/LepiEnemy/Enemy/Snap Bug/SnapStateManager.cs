using System.Buffers;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SnapStateManager : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform ghostTarget;
    private Transform player;
    public GameObject particle;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public GameObject exclamation;
    private GameObject exclamationMark;
    public GameObject pointer;
    public GameObject explodeZone;
    private RandomRotation randomRotation;

    private SnapBaseState currentState;
    public SnapPursueState PursueState = new SnapPursueState();
    public SnapPatrolState PatrolState = new SnapPatrolState();
    public SnapExplodeState ExplodeState = new SnapExplodeState();
    public SnapYeetState YeetState = new SnapYeetState();

    public float blowUpIncreasedSpeed;
    public float blinkDuration;
    public float blinkSpeed;
    public float speed;
    public float pursueRange;
    public float explosionRadius;
    public LayerMask sightLayers;
    public LayerMask explodeLayers;
    public float slowdownIntensity = 0.5f;
    private bool alreadyReacting = false;

    [HideInInspector] public float ogAngularDrag;
    private float slowestAlowableSpeed = 9f;
    private float tossForce = 2000f;
    bool tossed = false;
    private bool standStill = false;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        randomRotation = GetComponent<RandomRotation>();

        particle.SetActive(false);

        rb = GetComponent<Rigidbody2D>();

        currentState = PatrolState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this,ghostTarget, player);
        //YeetState?.Toss();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnCollision(this, collision);
    }

    public void ChangeState(SnapBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void CollisionWithPlayer (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript.isDashing)
            {
                randomRotation.enabled = false;
                playerScript.isDashing = false;
                ChangeState(YeetState);
            }
        }

        if (collision.gameObject.CompareTag("React") || collision.gameObject.CompareTag("Aftermath"))
        {
            standStill = true;
            explodeZone.tag = ("React");
            StartCoroutine("Explode");
        }
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Vector2 directionToPlayer = player.position - transform.position;
            Gizmos.DrawRay(transform.position, directionToPlayer.normalized * pursueRange);
        }
    }

    public void MoveTowards(Transform target)
    {
        Vector2 moveDirection = (target.position - this.transform.position).normalized;
        rb.velocity = moveDirection * speed * Time.fixedDeltaTime;
    }




    //PATROL STATE
    public void RaycastOnPlayer()
    {
        //Raycast towards player and if close enough then change state
        Vector2 directionToPlayer = player.position - this.transform.position;
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.position);

        //Debug.Log(distanceToPlayer);

        if (distanceToPlayer < pursueRange)
        {
            //Debug.Log("Should be pursuing");
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, directionToPlayer.normalized, pursueRange, sightLayers);
            if (hit.collider != null && hit.collider.transform == player)
            {
                //Debug.Log(hit.collider);
                ChangeState(ExplodeState);
            }
        }
    }





    //EXPLODE STATE
    IEnumerator Explode ()
    {
        //Debug.Log("Start explode");
        standStill = true;
        speed = 0f;
        rb.velocity = Vector2.zero;
        if (exclamationMark != null)
        {
            Destroy(exclamationMark);
        }
        spriteRenderer.enabled = false;
        explodeZone.tag = ("Aftermath");
        particle.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    IEnumerator ExplodePlayer()
    {
        //Debug.Log("Start explode");
        standStill = true;
        speed = 0f;
        rb.velocity = Vector2.zero;
        if (exclamationMark != null)
        {
            Destroy(exclamationMark);
        }
        spriteRenderer.enabled = false;
        particle.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    public void RotateTowards(Transform target)
    {
        if (!standStill)
        {
            Vector2 moveDirection = (target.position - this.transform.position).normalized;
            float rotateAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
        }        
    }

    public void StartExplode()
    {
        randomRotation.enabled = false;
        StartCoroutine(GraduallySlowDown());
        StartCoroutine(BlinkEffect());
    }

    IEnumerator GraduallySlowDown()
    {
        float duration = 3.0f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            speed = Mathf.Max(speed - Time.deltaTime * slowdownIntensity, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator BlinkEffect()
    {
        speed = blowUpIncreasedSpeed;
        float timePassed = 0f;

        while (timePassed < blinkDuration)
        {
            spriteRenderer.color = Color.red;
            spriteRenderer.gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
            //Debug.Log("blink");
            yield return new WaitForSeconds(blinkSpeed);
            spriteRenderer.color = Color.white;
            spriteRenderer.gameObject.transform.localScale = new Vector2(0.4499998f,0.4499998f);
            //Debug.Log("normal color");
            yield return new WaitForSeconds(blinkSpeed);

            timePassed += 2 * blinkSpeed;
        }

        //Beeg + stop
        spriteRenderer.gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
        speed = 0;

        //Display exclamation mark and play anim
        standStill = true;
        Vector3 exclamationPosition = new Vector3(this.transform.position.x, this.transform.position.y + 0.45f, this.transform.position.z);
        exclamationMark = Instantiate(exclamation, exclamationPosition, Quaternion.identity);
        animator.enabled = true;
        exclamation.GetComponent<SpriteRenderer>().enabled = true;


        yield return new WaitForSeconds(0.3f);

        //Blow up        
        StartCoroutine("ExplodePlayer");
    }




    //YEET STATE
    public void TossLogic()
    {
        if (tossed)
        {
            if (rb.velocity.magnitude < slowestAlowableSpeed)
            {
                rb.drag = 0f;
            }
            else
            {
                rb.drag = ogAngularDrag;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!tossed)
            {
                this.gameObject.tag = ("React");
                Time.timeScale = 1f;

                Transform bug = this.transform;

                tossed = true;
                rb.isKinematic = false;

                //Throw the bug
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = new Vector2(mouseWorldPos.x - bug.position.x, mouseWorldPos.y - bug.position.y).normalized;
                rb.AddForce(direction * tossForce);
            }
        }
    }

    public void YeetEnterState()
    {
        rb.isKinematic = false;
        rb.freezeRotation = false;

        ogAngularDrag = rb.drag;
        Time.timeScale = 0.05f;
    }

    public void DestroyWhenTouchWall(Collider2D collision)
    {
        if (!alreadyReacting)
        {
            if (collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("Enemy"))
            {
                speed = 0f;
                rb.velocity = Vector2.zero;
                //Debug.Log("Start Wall reaction");
                alreadyReacting = true;
                StartCoroutine("Explode");
            }
        }
    }

    //public void
}
