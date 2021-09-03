using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

	public class IapManeger : MonoBehaviour, IStoreListener
	{
		private static IStoreController m_StoreController;          // The Unity Purchasing system.
		private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

		public static string PRODUCT_SuperShip = "supership";   
		public static string PRODUCT_NO_ADS = "noads";



		void Start()
		{
			if (m_StoreController == null)
			{
				InitializePurchasing();
			}
		}

		public void InitializePurchasing() 
		{
			if (IsInitialized())
			{
				return;
			}
				
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			builder.AddProduct(PRODUCT_SuperShip, ProductType.NonConsumable);

			builder.AddProduct(PRODUCT_NO_ADS, ProductType.NonConsumable);


			UnityPurchasing.Initialize(this, builder);
		}


		private bool IsInitialized()
		{

			return m_StoreController != null && m_StoreExtensionProvider != null;
		}


		public void BuyShip()
		{
			BuyProductID(PRODUCT_SuperShip);
		}


		public void BuyNoADS()
		{
			BuyProductID(PRODUCT_NO_ADS);
		}



		void BuyProductID(string productId)
		{
			if (IsInitialized())
			{

				Product product = m_StoreController.products.WithID(productId);

				if (product != null && product.availableToPurchase)
				{
					Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));

					m_StoreController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					// ... report the product look-up failure situation  
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			// Otherwise ...
			else
			{
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
				// retrying initiailization.
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}


		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			// Purchasing has succeeded initializing. Collect our Purchasing references.
			Debug.Log("OnInitialized: PASS");

			// Overall Purchasing system, configured with products for this application.
			m_StoreController = controller;
			// Store specific subsystem, for accessing device-specific store features.
			m_StoreExtensionProvider = extensions;
		}


		public void OnInitializeFailed(InitializationFailureReason error)
		{
			// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
			Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		}


		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
		{
			// A consumable product has been purchased by this user.
			if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_SuperShip, StringComparison.Ordinal))
			{
				Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
				PlayerPrefs.SetInt("ship2",1);
			}
			// Or ... a non-consumable product has been purchased by this user.
			else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_NO_ADS, StringComparison.Ordinal))
			{
				Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
				PlayerPrefs.SetInt("noads",1);
			}
			else 
			{
				Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
			}

			// Return a flag indicating whether this product has completely been received, or if the application needs 
			// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
			// saving purchased products to the cloud, and when that save is delayed. 
			return PurchaseProcessingResult.Complete;
		}


		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
			// this reason with the user to guide their troubleshooting actions.
			Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
		}
	}
