using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform _pointA, _pointB;
    [SerializeField] float _speed = 2;
    [SerializeField] bool _slippingBrick = false;

    Vector3 _destination;

    private void Start()
    {
        _destination = _pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
        if (transform.position == _destination)
        {
            _destination = (_destination == _pointA.position) ? _pointB.position : _pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !_slippingBrick)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !_slippingBrick)
        {
            collision.transform.parent = null;
        }
    }
}
