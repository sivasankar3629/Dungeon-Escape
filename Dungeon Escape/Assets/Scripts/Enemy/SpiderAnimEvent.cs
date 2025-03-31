using UnityEngine;

public class SpiderAnimEvent : MonoBehaviour
{
    Spider _spider;

    private void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        _spider.Attack();
    }
}
