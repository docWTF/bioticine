using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiteStateManager : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform ghostTarget;
    private Transform player;
    //public GameObject particle;
    //public SpriteRenderer spriteRenderer;
    //public Animator animator;
    //public GameObject exclamation;
    public GameObject pointer;
    private RandomRotation randomRotation;

    private MiteBaseState currentState;
    //public MitePursueState PursueState = new MitePursueState();
    public MitePatrolState PatrolState = new MitePatrolState();
    //public MiteExplodeState ExplodeState = new MiteExplodeState();
    public MiteYeetState YeetState = new MiteYeetState();

    public float speed;
    //public float pursueRange;
    //public LayerMask sightLayers;

    [HideInInspector] public float ogAngularDrag;
    private float slowestAlowableSpeed = 9f;
    private float tossForce = 2000f;
    bool tossed = false;

    void Start()
    {
        player = GameObject.Find("Player").transform;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnCollision(this, collision);
    }

    public void ChangeState(MiteBaseState state)
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
                playerScript.isDashing = false;
                ChangeState(YeetState);
            }
        }

        if (collision.gameObject.CompareTag("React") || collision.gameObject.CompareTag("Aftermath"))
        {
            player.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

    public void MoveTowards(Transform target)
    {
        Vector2 moveDirection = (target.position - this.transform.position).normalized;
        rb.velocity = moveDirection * speed * Time.fixedDeltaTime;
    }

    //public void RotateTowards(Transform target)
    //{
    //    Vector2 moveDirection = (target.position - this.transform.position).normalized;
    //    float rotateAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
    //    this.transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
    //}



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
        randomRotation.enabled = false;
        rb.isKinematic = false;
        rb.freezeRotation = false;

        ogAngularDrag = rb.drag;
        Time.timeScale = 0.05f;
    }

    public void DestroyAndTeleportWhenTouchWall(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("destroy and teleport");
            player.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
