using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStateManager : MonoBehaviour
{
    
    private Rigidbody2D rb;

    public Transform ghostTarget;
    public Transform player;
    //public GameObject particle;
    //public SpriteRenderer spriteRenderer;
    public Animator animator;
    public GameObject exclamation;
    public GameObject pointer;
    //public GameObject bullets;
    private RandomRotation randomRotation;

    private BeeBaseState currentState;
    //public FlyPursueState PursueState = new FlyPursueState();
    public BeePatrolState PatrolState = new BeePatrolState();
    public BeeAttackState ShootState = new BeeAttackState();
    public BeeYeetState YeetState = new BeeYeetState();

    //public float blinkDuration;
    //public float blinkSpeed;
    public float speed;
    public float sightRange;
    //public float explosionRadius;
    public LayerMask sightLayers;

    [HideInInspector] public float ogAngularDrag;
    private float slowestAlowableSpeed = 9f;
    private float tossForce = 2000f;
    [HideInInspector] public bool tossed = false;
    private bool standStill = false;

    void Start()
    {
        randomRotation = GetComponent<RandomRotation>();

        //particle.SetActive(false);

        rb = GetComponent<Rigidbody2D>();

        currentState = PatrolState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this, ghostTarget, player);
        //YeetState?.Toss();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollide(this, collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnCollision(this, collision);
    }

    public void ChangeState(BeeBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void CollisionWithPlayer(Collider2D collision)
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
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Vector2 directionToPlayer = player.position - transform.position;
            Gizmos.DrawRay(transform.position, directionToPlayer.normalized * sightRange);
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

        if (distanceToPlayer < sightRange)
        {
            //Debug.Log("Should be pursuing");
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, directionToPlayer.normalized, sightRange, sightLayers);
            if (hit.collider != null && hit.collider.transform == player)
            {
                Debug.Log(hit.collider);
                ChangeState(ShootState);
            }
        }
    }





    //SHOOT STATE
    public void Explode(GameObject exclamationMark)
    {
        Destroy(exclamationMark);
        //spriteRenderer.enabled = false;
        //particle.SetActive(true);
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

    public void StartShooting()
    {
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        rb.velocity = Vector3.zero;

        randomRotation.enabled = false;
        animator.SetTrigger("triggerShootAnim");

        Vector3 exclamationPosition = new Vector3(this.transform.position.x, this.transform.position.y + 0.45f, this.transform.position.z);
        GameObject exclamationMark = Instantiate(exclamation, exclamationPosition, Quaternion.identity);

        yield return new WaitForSeconds(0.55f);

        standStill = true;
        //GameObject boollet = Instantiate(bullets, transform.position, Quaternion.identity);
        //boollet.transform.rotation = transform.rotation;

        Destroy(exclamationMark);

        yield return new WaitForSeconds(1f);

        randomRotation.enabled = true;
        standStill = false;
        ChangeState(PatrolState);
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
        if (collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("Enemy"))
        {
            GameObject.Destroy(this);
        }
    }

    public void BounceOffWall(Collision2D collision)
    {
        Vector2 currentVelocity = rb.velocity;
        if (collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall"))
        {
            Vector2 newDirection = Vector2.Reflect(currentVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = newDirection;

            float rotateAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0f, 0f, rotateAngle);

            GetComponent<Collider>().isTrigger = true;
        }
    }
}
