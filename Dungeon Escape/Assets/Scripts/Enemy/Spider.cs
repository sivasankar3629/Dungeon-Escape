using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    [SerializeField] GameObject _acidPrefab;
    Queue<GameObject> AcidPool;

    // Use for initialization
    public override void Init()
    {
        base.Init();

        // Health
        Health = base.health;

        // Object pool for acid
        AcidPool = new Queue<GameObject>();
        GameObject acidParent = new GameObject("Acid Prefab");
        for ( int i = 0; i < 5; i++)
        {
            GameObject acid = Instantiate(_acidPrefab, transform.position, Quaternion.identity);
            acid.SetActive(false);
            AcidPool.Enqueue(acid);
            acid.transform.SetParent(acidParent.transform, true); 
        }
    }

    public override void Movement()
    {
        
    }

    public void Damage()
    {
        Debug.Log("Spider::Damage()");
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = 3;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 1.5f);
        }
    }

    public void Attack()
    {
        GameObject acidToFire = AcidPool.Dequeue();
        acidToFire.transform.position = transform.position;
        acidToFire.SetActive(true);
        StartCoroutine(DestroyAcid(acidToFire));
    }

    IEnumerator DestroyAcid(GameObject acid)
    {
        yield return new WaitForSeconds(5f);
        acid.SetActive(false);
        AcidPool.Enqueue(acid);
    }
}
