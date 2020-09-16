using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movementchar : MonoBehaviour
{
    // fuerza de salto y velocidad
    private float speed = 15;
    private float jumpForce = 25;
    private float moveInput;

    public Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumpsLeft;
    private int extraJumpsUnlocked;
    public int DashCount;
    public float fattness;
    public GameObject dashEffect;
    public bool DoubleJumpPicked;

    public Animator animator;





    void Start()
    {
        extraJumpsUnlocked = 0;
        extraJumpsLeft = extraJumpsUnlocked;
        rb = GetComponent<Rigidbody2D>();



    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(moveInput));

        if(facingRight == false && moveInput > 0){

        Flip();

        }

        else if (facingRight == true && moveInput < 0){

        Flip();

        }
    }

    void Update(){

       if(isGrounded == true){
            extraJumpsLeft = extraJumpsUnlocked;
        }


        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumpsLeft > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJumpsLeft--;
        }

        if(Input.GetKeyDown(KeyCode.K)){
            fattness += 1;
            fatCheck();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && DashCount == 1 || (Input.GetKeyDown(KeyCode.RightShift) && DashCount == 1))
        {
            StartCoroutine ("DashMove");
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }

        animator.SetFloat("fat", fattness);


    }

    void Flip(){

       facingRight = !facingRight;

       transform.Rotate (0f, 180f, 0f);
    }

    public void fatCheck(){
        if (fattness < 3){
            speed = 15;
            jumpForce = 25;

        }
        else if (fattness == 3){
            speed -= 5;
            jumpForce -= 3;
        }
        else{
            if (fattness == 5){
            speed -= 7;
            jumpForce -= 7;
            }
        } 
    }

    public void winGame(){
    Destroy(gameObject);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    


void OnTriggerEnter2D(Collider2D other){               //SiEntraEnContacto
    if(other.tag == "Death"){                          //BuscarTag"Death"

        Destroy(gameObject);                  //DestruirPlayer
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //ResetearScene

        }
    if(other.tag == "Win"){                          
                   
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       

        }

    if (other.tag == "DoubleJump"){
                    Destroy(other.gameObject);

                    DoubleJumpPicked = true;
                    DashCount = 0;

                    extraJumpsUnlocked = 0;    //Para que no pueda stack saltos
                    extraJumpsUnlocked += 1;

                    fattness += 1;
                    fatCheck();
                }

    if (other.tag == "Dash"){
                    Destroy(other.gameObject);

                    extraJumpsUnlocked = 0;
                    DoubleJumpPicked = false;
                    
                    DashCount = 0;
                    DashCount += 1;

                    fattness += 1;
                    fatCheck();
                }
    if (other.tag == "Pizza"){
                    Destroy(other.gameObject);
                    
                    fattness += 1;
                    fatCheck();
                }
    if (other.tag == "Salad"){
                    Destroy(other.gameObject);
                    
                    fattness = 0;
                    fatCheck();
                }
}

IEnumerator DashMove()
{
    speed += 40;
    yield return new WaitForSeconds(.3f);
    speed -= 40;
    DashCount = 0;
         if (isGrounded == true && DoubleJumpPicked == false){
             DashCount += 1;
        }else{
            yield return new WaitForSecondsRealtime(2);
            DashCount += 1;
            if (DoubleJumpPicked == true){
            DashCount -=1;
            }
        }
}
}

