using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health {  get; set; }
    // Use for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        Debug.Log("Skeleton::Damage()");
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if ( Health < 1)
        {
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = gems;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 1.5f);
        }
    }
}
