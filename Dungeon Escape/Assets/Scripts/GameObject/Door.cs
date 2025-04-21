using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] bool _hasKey = false;

    private void Start()
    {
        GameManager.Instance.HasKeyToCastle = _hasKey;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ( collider.CompareTag("Player") && GameManager.Instance.HasKeyToCastle)
        {
            Debug.Log("Game Won");
            GameManager.Instance.GameWon();
        }
    }

    IEnumerator NextLevel()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
