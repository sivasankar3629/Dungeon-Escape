using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
        IDamageable hit = collider.GetComponent<IDamageable>();
        if (hit != null)
        {
            if (_canDamage)
            {
                hit.Damage();
                StartCoroutine(DamageRoutine());
            }
            
        }
    }

    IEnumerator DamageRoutine()
    {
        _canDamage = false;
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
