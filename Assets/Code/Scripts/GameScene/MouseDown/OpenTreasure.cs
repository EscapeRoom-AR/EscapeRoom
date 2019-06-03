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
            chest.GetComponent<Animation>().Play("ChestAnim");
            gameController.NextPhase();
            isopen = true;
        } else if (inventory.selectedItem != null)
        {
            print("TAG: " + inventory.selectedItem.Tag);
        }
        
    }
}