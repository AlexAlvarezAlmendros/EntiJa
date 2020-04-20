using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    private float speed = 5;
    private float walkAcceleration = 75;
    private float groundFriction = 70;
    private float jumpForce = 4;
    
    private bool isjumping = false;

    private Animator anim;
    private Vector2 velocity;
    private BoxCollider2D boxCollider;
    private Rigidbody2D Man;
    public GameObject cam;

    public GameObject laser;
    

    void Start()
    {
        GameController.Instance.setEnergy(50);
        transform.position = new Vector3(-4.44f, -3.16f, 0f);
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        Man = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");
        
    }

    private void Update()
    {
        if (transform.position.x < cam.transform.position.x - 3.65f)
        {
            transform.position = new Vector3(cam.transform.position.x - 3.65f, transform.position.y, transform.position.z);
        }

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Man.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
            isjumping = true;
        }
        else
        {
            isjumping = false;
        }

        if (Input.GetMouseButtonDown(1))
        {

        }
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundFriction * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);

        float energy = GameController.Instance.getEnergy();
    }

    void FixedUpdate()
    {


    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("Enemy"))
        {
            SceneManager.LoadScene("GameOver");
        }
        if (c.gameObject.tag.Equals("Energy"))
        {
            GameController.Instance.setEnergy(20);
        }

    }
    private bool IsGrounded()
    {
        float extraHeightText = .01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + extraHeightText, groundLayerMask);
        Color rayColor;
        if ( raycastHit.collider != null )
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
}
