using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperChairController : MonoBehaviour
{
    public GameObject modalPrefab;
    public Canvas canvas;
    public Inventory inventory;
    public int papersPicked;
    private static string tag;
    private GameObject modal;
    public GameController gameController;
    public Sprite allPapersSprite;
    private bool isModalShown;

    private void Start()
    {
        papersPicked = 0;
        tag = "CHAIR";
        isModalShown = false;
    }

    public void PaperTapped(GameObject gameObject)
    {
        papersPicked += 1;
        Sprite sprite = gameObject.GetComponent<Image>().sprite;
        InteractiveItem item = new InteractiveItem(sprite, tag, () => ShowModal(sprite));
        Destroy(gameObject);
        inventory.AddToInventory(item);
        print("PapersPicked: " + papersPicked);
        if (papersPicked == 2) Complete();


    }

    private void Complete()
    {
        inventory.RemoveItemsWithTag(tag);
        InteractiveItem item = new InteractiveItem(allPapersSprite, "FULLCHAIR", () => ShowModal(allPapersSprite));
        inventory.AddToInventory(item);
        ShowModal(allPapersSprite);
        gameController.NextPhase();
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
