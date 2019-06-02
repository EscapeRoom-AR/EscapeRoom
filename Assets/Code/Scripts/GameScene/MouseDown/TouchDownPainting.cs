using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchDownPainting : MonoBehaviour
{
    public OrderController OrderController;
    private GameObject modal;
    private bool isModalShown;
    public GameObject modalPrefab;
    public Canvas canvas;
    public Sprite sprite;
    public Inventory inventory;

    public void OnMouseDown()
    {
        OrderController.ItemTapped(new OrderController.OrderedItem(
          gameObject.tag,
          () => gameObject.transform.Find("Circle").gameObject.SetActive(true),
          () => gameObject.transform.Find("Circle").gameObject.SetActive(false)
        ), () => {
            ShowModal();
            inventory.AddToInventory(new InteractiveItem(sprite, "CHAIR", () => ShowModal()));
        });
    }

    private void ShowModal()
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
