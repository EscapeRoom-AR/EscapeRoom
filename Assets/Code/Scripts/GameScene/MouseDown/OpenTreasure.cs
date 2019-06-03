using UnityEngine;
using System.Collections;

public class OpenTreasure : MonoBehaviour
{
    public GameObject chest;
    public GameController gameController;
    public Inventory inventory;
    private bool isopen;

    private void Start()
    {
        isopen = false;
    }

    public void OnMouseDown()
    {
        if (!isopen && inventory.selectedItem != null && inventory.selectedItem.Tag.Equals("KEY")) {
            FXService.Instance.PlayOpenChest();
            chest.GetComponent<Animation>().Play("ChestAnim");
            gameController.NextPhase();
            isopen = true;
        }
        
    }
}