using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement player = collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.AddGems(gems);
                Destroy(gameObject);
            }
            
        }
    }
}
