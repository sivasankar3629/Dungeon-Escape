using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    Rigidbody2D _rb;
    PlayerAnimation _playerAnim;

    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] SpriteRenderer _swordArcSprite;

    [SerializeField] float _playerSpeed = 5f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] Vector2 _boxSize;
    [SerializeField] float _castDistance;
    private PlayerInputs _playerInputs;

    public int _diamonds = 0;

    public int Health {  get; set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerInputs = new PlayerInputs();
        _playerInputs.Player.Enable();
        Health = 4;
    }

    void Update()
    {
        if (!GameManager.Instance._gameOver)
        {
            Move();
            Jump();
        }
        if ( _playerInputs.Player.Attack.triggered && IsGrounded())
        {
            _playerAnim.AttackAnim();
        }
    }

    private void Move()
    {
        float horizontalInput = _playerInputs.Player.Move.ReadValue<Vector2>().x;
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
        if (_playerInputs.Player.Move.ReadValue<Vector2>().y > 0.8 && IsGrounded())
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

    public void AddGems(int amount)
    {
        _diamonds += amount;
        UIManager.Instance.UpdateGemCount(_diamonds);
    }

    public void Damage()
    {
        if ( Health < 1)
        {
            return;
        }
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if ( Health < 1 )
        {
            GameManager.Instance.GameLost();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Spikes"))
        {
            GameManager.Instance.GameLost();
        }
    }
}
