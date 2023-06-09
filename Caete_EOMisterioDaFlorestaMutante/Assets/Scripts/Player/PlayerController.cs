using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public SceneController sceneController;
    public int dashLimit;

    // Movement
    public float speed;
    public float jumpForce;
    
    //public float ClimbSpeed = 3f;
    
    // Swim
    public float SwimSpeed = 5f;
    public float empuxo = 5f;
    public float flutuacao = 10f;

    // Life and Damage
    public int vida;
    public int vidaMax;
    public float dano;
    public GameObject AttackCol;

    // GroundCheck
    public Transform groundCheck;
    public LayerMask ground;
    //public LayerMask wall;
    //public Transform wallCheck;
    public LayerMask water;
    //public Transform waterCheck;
    //public Vector3 vineVelocityWhenGrabbed;

    // Dash
    public float dashDistance;
    public float dashTime;
    public float dashCooldown;
    

    private Rigidbody _rigidbody;
    private Vector3 _movement;
    private Animator _anim;
   // private Transform _currentSwingable;
   // private GameObject _contactBlock;
   // public Image _vidaUI;
    
    private bool _faceRight = true;
    private int _jumpCounter;
    private bool _isTatuTransform = false;
    private bool _isAttacking = false;
    private int _attackCounter = 0;
    private bool _isClimbing = false;
    private bool _isSwiming = false;
    private bool _isSwinging = false;
    private bool _isDashing = false;
    //private float _timer = 2f;

    // Habilidades desbloqueadas
    //[HideInInspector]
    public int jumpLimit, dashCounter;
    [HideInInspector]
    public bool _canDash, _canSwim, _canTatuTransform, _canClimb;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Altera��o de Colisor se est� nadando
        if (_isSwiming)
        {
            gameObject.GetComponent<CapsuleCollider>().direction = 0;
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider>().direction = 1;
        }
       if (vida > 0 && Time.timeScale > 0f)
        {
            // Dash
            if (Input.GetButtonDown("Dash") && _canDash && (!_isSwinging || !_isAttacking || !_isSwiming || !_isClimbing || !_isTatuTransform) && dashCounter < dashLimit)
            {
                StopCoroutine("DashCooldown");
                StartCoroutine("DashCooldown");
                _canDash = false;
                _anim.SetTrigger("IsDashing");
                _rigidbody.velocity = Vector3.zero;
            }

            // Andar
            if (!_isAttacking && !_isSwinging && !_isDashing && !_isSwiming)
            {
                PlayerMove();
            }

            // Pular
            if (Input.GetButtonDown("Jump") && ((_jumpCounter < jumpLimit) || IsGrounded()) && !_isTatuTransform)
            {
                Jump();
            }

            // Atacar
            if (Input.GetButtonDown("Attack") && !_isTatuTransform && !_isSwiming && !_isClimbing)
            {
                AttackOn();
            }
        }
        

        /*
        // Transforma��o em Tatu
        if (Input.GetButtonDown("Transform") && !_isClimbing && !_isAttacking && !_isSwiming && _canTatuTransform)
        {
            TatuTransform();
        }

        if(_isTatuTransform && Input.GetButtonDown("Vertical") && _contactBlock != null)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            if(verticalInput < 0)
            {
                BlocoEscavacao escavacaoBloco = _contactBlock.GetComponent<BlocoEscavacao>();
                escavacaoBloco.EscavarBloco();
            }
        }
        
        // Escalar paredes
        if (Physics.CheckSphere(wallCheck.position, 0.01f, wall))
        {
            _isClimbing = true;
            Climb();
        } else {
            _isClimbing = false;
        }

        if (Physics.CheckSphere(waterCheck.position, 0.01f, water))
        {
            if (_isTatuTransform)
            {
                TatuTransform();
            }
            _isSwiming = true;
            Swim();
        } else {
            _isSwiming = false;
        }
        */
        /*
        // Controle de Vida e Morte
        if (vida <= 0)
        {
            transform.localScale = Vector3.zero;
            if (timer <= 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        */
    }

    void LateUpdate()
    {
        _anim.SetInteger("Life", vida);
        _anim.SetBool("Idle", _movement == Vector3.zero);
        _anim.SetBool("isGrounded", IsGrounded());
        _anim.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
        _anim.SetBool("IsSwiming",_isSwiming);
        _anim.SetBool("isOverWater", IsOverWater());
        /*
        _anim.SetBool("Transform",_isTatuTransform);
        _anim.SetBool("isClimbing", _isClimbing);

        _vidaUI.rectTransform.localScale = new Vector3(2.5f * (vida/100), _vidaUI.rectTransform.localScale.y, _vidaUI.rectTransform.localScale.z);
        */
    }

    void PlayerMove()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector3(horizontalInput, 0f, 0f);
        if (horizontalInput < 0f && _faceRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && _faceRight == false)
        {
            Flip();
        }
        
        float horizontalVelocity = _movement.normalized.x * speed;
        _rigidbody.velocity = new Vector3(horizontalVelocity, _rigidbody.velocity.y, _rigidbody.velocity.z);
    }
    /*
    void TatuTransform()
    {
        if (_isTatuTransform == false)
        {
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = false;
            _isTatuTransform = true;
        } else
        {
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<SphereCollider>().enabled = false;
            _isTatuTransform = false;
        }
    }

    void Climb()
    {
        float VerticalInput = Input.GetAxisRaw("Vertical");
        _movement = new Vector3(0f, VerticalInput, 0f);

        float verticalvelocity = _movement.normalized.y * ClimbSpeed;

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, verticalvelocity, _rigidbody.velocity.z);
    }
    */
    void Swim()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");
        _movement = new Vector3(horizontalInput, VerticalInput, 0f);


        _rigidbody.velocity = _movement * SwimSpeed;
        if (horizontalInput < 0f && _faceRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && _faceRight == false)
        {
            Flip();
        }

        bool acimaDaAgua = transform.position.y > 0;


        if (acimaDaAgua)
        {
            Vector3 forcaFlutuacao = Vector3.up * flutuacao;
            _rigidbody.AddForce(forcaFlutuacao, ForceMode.Force);
        }


        if (!acimaDaAgua)
        {
            Vector3 forcaEmpuxo = Vector3.down * empuxo;
            _rigidbody.AddForce(forcaEmpuxo, ForceMode.Force);
        }


    }
    
    void Jump()
    {
        _rigidbody.velocity = new Vector3(0f, 0f, 0f);
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _anim.SetTrigger("Jump");
        _jumpCounter++;
    }

    void Flip()
    {
        _faceRight = !_faceRight;

        if (_faceRight)
        {
            //transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            AttackCol.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else
        {
            //transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            AttackCol.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        }

    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.02f, ground);
    }

    bool IsOverWater()
    {
        return Physics.CheckSphere(new Vector3(groundCheck.position.x, groundCheck.position.y - 0.2f, groundCheck.position.z), 0.02f, water);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Reinicia os pulos quando o jogador toca no ch�o
        if (collision.gameObject.CompareTag("Ground") && IsGrounded())
        {
            _jumpCounter = 0;
        }
    }

    
    void AttackOn()
    {
        if (IsGrounded())
        {
            _rigidbody.velocity = Vector3.zero;
        }
        _attackCounter++;
        _anim.SetTrigger("Attack");
        AttackCol.SetActive(true);

        if (_attackCounter == 1)
        {
            AttackCol.GetComponent<BoxCollider>().enabled = true;
            AttackCol.GetComponent<CapsuleCollider>().enabled = false;
            AttackCol.GetComponent<SphereCollider>().enabled = false;
        }
        else if (_attackCounter == 2)
        {
            AttackCol.GetComponent<BoxCollider>().enabled = false;
            AttackCol.GetComponent<CapsuleCollider>().enabled = true;
            AttackCol.GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            AttackCol.GetComponent<BoxCollider>().enabled = false;
            AttackCol.GetComponent<CapsuleCollider>().enabled = false;
            AttackCol.GetComponent<SphereCollider>().enabled = true;
        }
        _isAttacking = true;
    }

    void AttackOff()
    {
        _attackCounter = 0;
        AttackCol.SetActive(false);
        _isAttacking = false;
    }

    public void ReceberDano(int quantidade)
    {
        if (vida > 0)
        {
            vida -= quantidade;
        }
    }
    

    IEnumerator Dash()
    {
        float startTime = Time.time;
        Vector3 dashDirection;
        _isDashing = true;

        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            dashDirection = new Vector3(1f, 0f, 0f);
        }
        else
        {
            dashDirection = new Vector3(-1f, 0f, 0f);
        }

        //Vector3 dashDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

        while (Time.time < startTime + dashTime)
        {
            _rigidbody.velocity = dashDirection * dashDistance;
            yield return null;
        }
        dashCounter++;
        _rigidbody.useGravity = true;

        _rigidbody.velocity = Vector3.zero;
        _canDash = true;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        
        if (dashCounter > 0)
        {
            if (!_isDashing)
            {
                dashCounter--;
                yield return null;
            }
            
            if (dashCounter > 0)
            {
                StartCoroutine("DashCooldown");
            }
        }
    }

    public void EndDash()
    {
        _isDashing = false;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            if(Physics.CheckSphere(transform.position, 0.02f, water) && _canSwim)
            {
                _isSwiming = true;
                _rigidbody.useGravity = false;
                Swim();
            }
            else
            {
                vida = 0;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            _jumpCounter = 0;
            _rigidbody.useGravity = true;
            _isSwiming = false;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RopeSegment" && !_isTatuTransform && _canDash)
        {
            vineVelocityWhenGrabbed = _rigidbody.velocity * 2.5f;
            other.GetComponent<Rigidbody>().velocity = vineVelocityWhenGrabbed;
            _isSwinging = true;
            _currentSwingable = other.transform;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "EscavationBlock")
        {
            _contactBlock = collision.gameObject;
        }
        
    }
    */
}
