using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    private static string premiumID = "premium";

    void Start()
    {
        if (m_StoreController == null)
            InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(premiumID, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyPremium()
    {
        m_StoreController.InitiatePurchase(m_StoreController.products.WithID(premiumID));
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        BuyPremium();
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("Error while initializing IAP:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        print("Premium purchased");
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Premium purchase failed: " + failureReason);
    }
}