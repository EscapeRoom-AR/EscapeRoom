using UnityEngine;
using System.Collections;

public class OpenTreasure : MonoBehaviour
{
    public GameObject chest;
    public GameController gameController;
    public Inventory inventory;

    public void OnMouseDown()
    {
        if (inventory.selectedItem.Tag.Equals("final_key")) {
            chest.GetComponent<Animation>().Play("ChestAnim");
            gameController.NextPhase();
        }
        
    }
}