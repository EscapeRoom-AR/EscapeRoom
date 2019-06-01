using UnityEngine;
using System.Collections;

public class OpenTreasure : MonoBehaviour
{
    public GameObject chest;

    public void OnMouseDown()
    {
        chest.GetComponent<Animation>().Play("ChestAnim");
    }
}