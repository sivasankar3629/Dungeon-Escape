using System.Collections;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    [SerializeField] float _speed = 3;

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            IDamageable hit = collider.GetComponent<IDamageable>();
            if (hit != null)
            {
                 hit.Damage();
                 this.gameObject.SetActive(false);

            }
        }
    }


}
