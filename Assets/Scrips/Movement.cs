using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;

    private Vector2 _moveInput;
    private float _playerHalfHeight;
    private float _playerHalfWidth;
    private Vector2 _direction;
    private Animator _animator;

    private void Start()
    {
        _playerHalfHeight = gameObject.GetComponent<BoxCollider2D>().bounds.extents.y;
        _playerHalfWidth = gameObject.GetComponent<BoxCollider2D>().bounds.extents.x;
        _direction = Vector2.right;
        _animator = gameObject.GetComponent<Animator>();
    }
    
    public void OnMove(InputValue value)
    {
        _moveInput.x = value.Get<Vector2>().x;
        if (_moveInput.x != 0)
        {
            _direction = _moveInput.x > 0 ? Vector2.right : Vector2.left;
            _animator.SetBool("IsWalking", true);
            _animator.SetFloat("Direction", _moveInput.x < 0 ? 0.0f : 1.0f);
        }
        else
            _animator.SetBool("IsWalking", false);
    }

    public void OnJump(InputValue value)
    {
        if (IsGrounded())
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
    private bool IsGrounded()
    {
        bool hit = Physics2D.Raycast(new Vector2(transform.position.x - _playerHalfWidth, transform.position.y), Vector2.down, _playerHalfWidth + 0.1f, LayerMask.GetMask("Ground"))
            || Physics2D.Raycast(new Vector2(transform.position.x + _playerHalfWidth, transform.position.y), Vector2.down,  _playerHalfWidth + 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(new Vector2(transform.position.x - _playerHalfWidth, transform.position.y), Vector2.down, Color.blue, _playerHalfWidth + 0.1f);
        Debug.DrawRay(new Vector2(transform.position.x + _playerHalfWidth, transform.position.y), Vector2.down, Color.blue, _playerHalfWidth + 0.1f);
        return hit;
    }

    private bool IsTouchingWall(Vector2 direction)
    {
        bool hit = Physics2D.Raycast(transform.position, direction, _playerHalfHeight, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, direction, Color.aquamarine, _playerHalfHeight);
        return hit;
    }

    void Update()
    {
        Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f);
        if (IsTouchingWall(_direction))
        {
            move.x = 0;
            _animator.SetBool("IsWalking", false);
        }

        _animator.SetBool("IsGrounded", IsGrounded());         
        gameObject.transform.position += Time.deltaTime * speed * move;
    }
}
