using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchMouseDown : MonoBehaviour
{
    public Inventory inventory;
    private GameObject modal;
    private bool isModalShown;
    public GameObject modalPrefab;
    public Canvas canvas;
    public Sprite sprite;
    public bool torchSolved;

    private void Start()
    {
        torchSolved = false;
    }
    private void OnMouseDown()
    {
        if (!torchSolved)
        {
            if (inventory.selectedItem.Tag.Equals("FULLPAPER"))
            {
                inventory.RemoveItemsWithTag("FULLPAPER");
                InteractiveItem item = new InteractiveItem(gameObject.GetComponent<Image>().sprite, "SECRETPAPER", () => ShowModal(sprite));
                inventory.AddToInventory(item);
                ShowModal(sprite);
                torchSolved = true;
            }
        }
    }

    private void ShowModal(Sprite sprite)
    {
        if (isModalShown)
            return;
        isModalShown = true;
        modal = Instantiate(modalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        modal.transform.SetParent(canvas.transform, false);
        modal.transform.Find("Content").Find("Image").GetComponent<Image>().sprite = sprite;
        modal.transform.Find("ButtonHolder").Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            Destroy(modal);
            isModalShown = false;
        });
    }


}
