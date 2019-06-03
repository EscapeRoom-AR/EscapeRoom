using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchDownPainting : MonoBehaviour
{
    public bool isPainting;
    public OrderController OrderController;
    private GameObject modal;
    private bool isModalShown;
    public GameObject modalPrefab;
    public Canvas canvas;
    public Sprite sprite;
    public Inventory inventory;
    public Sprite joinedSprite;
    public GameObject key;

    public void OnMouseDown()
    {
        print("tapped number");
        OrderController.ItemTapped(new OrderController.OrderedItem(
          gameObject.tag,
          () => gameObject.transform.Find("Circle").gameObject.SetActive(true),
          () => gameObject.transform.Find("Circle").gameObject.SetActive(false)),
          () => {
            ShowModal(sprite);
            inventory.AddToInventory(new InteractiveItem(sprite, "CHAIR", () => ShowModal(sprite)));
        }, () => {
            inventory.RemoveItemsWithTag("CHAIR");
            inventory.AddToInventory(new InteractiveItem(joinedSprite, "JOINEDCHAIR", () => ShowModal(joinedSprite)));
            key.SetActive(true);
        }, isPainting);
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
