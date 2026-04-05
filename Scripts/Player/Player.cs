using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamagable
{
    public int diamonds;
    private Rigidbody2D _rb;

    [SerializeField]
    private float _jumpForce = 5.0f;

    private bool _resetJump = false;
    private bool _isGrounded = false;

    [SerializeField]
    private float _speed = 5.0f;

    private PlayerAnimations _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int Health { set;  get; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimations>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = 4;
    }

    void Update()
    {
        Movement();
        if(Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        float move = Input.GetAxis("Horizontal");
        _isGrounded = IsGrounded();

        if(move > 0)
        {
            Flip(true);
        }
        else if(move < 0)
        {
            Flip(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        _rb.velocity = new Vector2(move * _speed, _rb.velocity.y);
        _playerAnim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);

        if(hitInfo.collider != null)
        {
            if(_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }
    
    void Flip(bool faceRight)
    {
        if(faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;

        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if(Health < 1)
        {
            return;
        }

        Debug.Log("Player Damaged");
        Health--;
        UIManager.Instance.UpdateLives(Health);

        if(Health < 1)
        {
            _playerAnim.Death();
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}
