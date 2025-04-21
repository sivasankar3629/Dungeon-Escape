using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;
    private PlayerMovement _player;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _player = collider.GetComponent<PlayerMovement>();
            if ( _player != null)
            {
                UIManager.Instance.OpenShop(_player._diamonds);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        currentSelectedItem = item;
        switch (item)
        {
            case 1:
                UIManager.Instance.UpdateShopSelection(32);
                currentItemCost = 200;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-82);
                currentItemCost = 400;
                break;
            case 3:
                UIManager.Instance.UpdateShopSelection(-190);
                currentItemCost = 100;
                break;
            default:
                Debug.Log("Invalid");
                break;
        }
    }

    public void BuyItem()
    {
       if (_player._diamonds >= currentItemCost)
        {
            if (currentSelectedItem == 3)
            {
                GameManager.Instance.HasKeyToCastle = true;
                Debug.Log("Door Unlocked");
            }
            _player._diamonds -= currentItemCost;
            UIManager.Instance.UpdateGemCount(_player._diamonds);
            shopPanel.SetActive(false);
        }
        else {
            Debug.Log("Not enough Gems");
            shopPanel.SetActive(false );
        }
    }
}
