using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchasePremium : MonoBehaviour
{

    public void BuyComplete(Product product)
    {
        print("Premium bought");


    }

    public void BuyFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Premium buy error: " + failureReason);
    }
}
