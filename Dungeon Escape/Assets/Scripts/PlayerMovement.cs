using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rb;
    PlayerAnimation _playerAnim;

    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] SpriteRenderer _swordArcSprite;

    [SerializeField] float _playerSpeed = 5f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] Vector2 _boxSize;
    [SerializeField] float _castDistance;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        Move();
        Jump();
        if ( Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnim.AttackAnim();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Flip(horizontalInput);

        _playerAnim.MoveAnim(Mathf.Abs(horizontalInput));
        _rb.linearVelocityX = horizontalInput * _playerSpeed;
    }

    private void Flip(float horizontalInput)
    {
        if (horizontalInput < 0) // Facing Left
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -0.75f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (horizontalInput > 0) // Facing Right
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 0.75f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    private void Jump()
    {
        _playerAnim.JumpAnim(!IsGrounded());
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.linearVelocityY = _jumpForce;
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, _boxSize, 0, -transform.up, _castDistance, 1 << 6))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _castDistance, _boxSize);
    }
}
