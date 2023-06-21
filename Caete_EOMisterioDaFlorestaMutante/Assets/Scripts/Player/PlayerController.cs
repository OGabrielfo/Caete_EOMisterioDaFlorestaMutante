using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int jumpLimit;
    //public float ClimbSpeed = 3f;
    //public float SwimSpeed = 5f;
    //public float empuxo = 5f;
    //public float flutuacao = 10f;
    public float vida;
    public float vidaMax;
    public float dano;
    public Transform groundCheck;
    public LayerMask ground;
    public GameObject AttackCol;
    //public LayerMask wall;
    //public Transform wallCheck;
    //public LayerMask water;
    //public Transform waterCheck;
    //public Vector3 vineVelocityWhenGrabbed;
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
    private bool _isClimbing = false;
    private bool _isSwiming = false;
    private bool _isSwinging = false;
    private bool _canDash = true;
    private float timer = 2f;

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
        /*
        if (_isSwinging)
        {
            _movement = Vector3.zero;
            transform.position = _currentSwingable.position;
            if (Input.GetButtonDown("Jump"))
            {
                _isSwinging = false;
                _rigidbody.velocity = new Vector3(_currentSwingable.GetComponent<Rigidbody>().velocity.x + 5, _currentSwingable.GetComponent<Rigidbody>().velocity.y + 5, _currentSwingable.GetComponent<Rigidbody>().velocity.z);
                _rigidbody.useGravity = true;
            }
        }
        */
        if (Input.GetButtonDown("Dash") && _canDash && (!_isSwinging || !_isAttacking || !_isSwiming || !_isClimbing || !_isTatuTransform))
        {
            _canDash = false;
            _anim.SetTrigger("IsDashing");
            _rigidbody.velocity = Vector3.zero;
        }
        
        if (!_isAttacking && !_isSwinging && _canDash) {
            PlayerMove();
        }
        
        if (Input.GetButtonDown("Jump") && ((_jumpCounter < jumpLimit) || IsGrounded()) && !_isTatuTransform)
        {
            Jump();
        }

        
        if (Input.GetButtonDown("Attack") && !_isTatuTransform && !_isSwiming && !_isClimbing)
        {
            AttackOn();
        }
        /*
        if (Input.GetButtonDown("Transform") && !_isClimbing && !_isAttacking && !_isSwiming)
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
        
        _anim.SetBool("Idle", _movement == Vector3.zero);
        _anim.SetBool("isGrounded", IsGrounded());
        _anim.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
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

    void Swim()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");
        _movement = new Vector3(horizontalInput, VerticalInput, 0f);


        _rigidbody.velocity = _movement * SwimSpeed;


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
    */
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
            transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else
        {
            transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        }

    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.02f, ground);

    }

    void OnCollisionEnter(Collision collision)
    {
        // Reinicia os pulos quando o jogador toca no chão
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
        _anim.SetTrigger("Attack");
        AttackCol.SetActive(true);
        _isAttacking = true;
    }

    void AttackOff()
    {
        AttackCol.SetActive(false);
        _isAttacking = false;
    }

    public void ReceberDano(float quantidade)
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

        if (transform.rotation.eulerAngles.y == 0f)
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

        _rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
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
