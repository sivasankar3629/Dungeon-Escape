using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float _fallWait = 2f;
    [SerializeField] float _destroyWait = 1f;

    bool _isFalling;
    Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isFalling && collision.gameObject.CompareTag("Player")){
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        _isFalling = true;
        yield return new WaitForSeconds(_fallWait);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(_destroyWait);
        Destroy(gameObject);
    }
}
