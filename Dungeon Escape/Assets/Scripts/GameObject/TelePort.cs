using UnityEngine;

public class TelePort : MonoBehaviour
{
    [SerializeField] GameObject _portal;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) ){
            collider.transform.position = _portal.transform.position;
        }
    }
}
