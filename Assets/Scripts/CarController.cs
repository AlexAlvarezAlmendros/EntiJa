using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    private float speed = 5;
    private float walkAcceleration = 75;
    private float groundFriction = 70;
    private float jumpForce = 4;
    
    private bool isjumping = false;

    private Animator anim;
    private Vector2 velocity;
    private BoxCollider2D boxCollider;
    private Rigidbody2D Man;
    private bool grounded;
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
        if (grounded)
        {
            velocity.y = 0;
            
            if (Input.GetButtonDown("Jump"))
            {
               Man.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
                isjumping = true;
            }
            else
            {
                isjumping = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            ShootLaser();
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
        if (isjumping && !grounded && energy > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * 0);
            }
        }

        //Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        //grounded = false;
        //foreach (Collider2D hit in hits)
        //{
        //    if (hit == boxCollider)
        //        continue;

        //    ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
        //    if (colliderDistance.isOverlapped)
        //    {
                
        //        transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
        //    }
        //    if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
        //    {
        //        grounded = true;
        //    }
        //}
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
        if (c.gameObject.tag.Equals("Ground"))
        {
            ColliderDistance2D colliderDistance = c.Distance(c);
            if (colliderDistance.isOverlapped)
            {

                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
        }

    }
    void ShootLaser()
    {
        Instantiate(laser,
        new Vector3(transform.position.x + 1, transform.position.y, 0),
        transform.rotation);
    }
}
