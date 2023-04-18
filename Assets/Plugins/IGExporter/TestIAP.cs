using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QMG;

namespace QMG
{

    public class TestIAP : MonoBehaviour
    {
        private Product[] curProducts = null;
        private Purchase[] curPurchases = null;

        private Text debugText = null;

        private Button testShowIAPProductButton = null;
        private Button testPurchaseFirstProductButton = null;
        private Button testGetPurchasesButton = null;
        private Button testConsumeFirstPurchaseButton = null;


        private void Awake()
        {
            testShowIAPProductButton = GameObject.Find("Canvas/TestShowIAPProductButton").GetComponent<Button>();
            testShowIAPProductButton.onClick.AddListener(OnClick_TestShowIAPProductButton);

            testPurchaseFirstProductButton = GameObject.Find("Canvas/TestPurchaseFirstProductButton").GetComponent<Button>();
            testPurchaseFirstProductButton.onClick.AddListener(OnClick_TestPurchaseFirstProductButton);

            testGetPurchasesButton = GameObject.Find("Canvas/TestGetPurchasesButton").GetComponent<Button>();
            testGetPurchasesButton.onClick.AddListener(OnClick_TestGetPurchasesButton);

            testConsumeFirstPurchaseButton = GameObject.Find("Canvas/TestConsumeFirstPurchaseButton").GetComponent<Button>();
            testConsumeFirstPurchaseButton.onClick.AddListener(OnClick_TestConsumeFirstPurchaseButton);

            debugText = GameObject.Find("Canvas/Scroll View (1)/Viewport/Content/DebugText").GetComponent<Text>();
        }

        private void Start()
        {
            FBInstant.payments.onReady(On_PaymentsOnReady);
        }

        private void SetDebugText(string txt)
        {
            debugText.text += txt + "\n";
            debugText.gameObject.SetActive(true);
        }

        private void On_PaymentsOnReady()
        {
            Debug.Log("[On_PaymentsOnReady] isOnReadyOk=" + FBPayments.isOnReadyOk + ",isSupportPayments=" + FBInstant.payments.isSupportPayments());
            SetDebugText("[On_PaymentsOnReady] isOnReadyOk=" + FBPayments.isOnReadyOk + ",isSupportPayments=" + FBInstant.payments.isSupportPayments());
        }


        private void OnClick_TestShowIAPProductButton()
        {
            Debug.Log("OnClick_TestShowIAPProductButton");

            SetDebugText("isSupportPayments=" + FBInstant.payments.isSupportPayments());
            if (FBInstant.payments.isSupportPayments())
            {
                FBInstant.payments.getCatalogAsync(OnGetCatalogAsync);
                SetDebugText("geting catalog");
            }
            else
            {
                SetDebugText("do now support this platform");
            }
            
        }

        private void OnGetCatalogAsync( FBError err, Product[] productArr)
        {
            if (!FBInstant.payments.isSupportPayments())
            {
                SetDebugText("[OnGetCatalogAsync] do now support this platform");
                return;
            }

            curProducts = null;
            if (err == null || string.IsNullOrEmpty(err.code))
            {
                curProducts = productArr;
                for (int i = 0; i < curProducts.Length; i++)
                {
                    SetDebugText("[OnGetCatalogAsync] product=" + curProducts[i]);
                }
            }
            else
            {
                SetDebugText("[OnGetCatalogAsync] get catalog error:" + err);
            }
        }

        private void OnClick_TestPurchaseFirstProductButton()
        {
            Debug.Log("OnClick_TestPurchaseFirstProductButton");

            SetDebugText("isSupportPayments=" + FBInstant.payments.isSupportPayments());
            if (FBInstant.payments.isSupportPayments())
            {
                PurchaseConfig purchaseConfig = new PurchaseConfig()
                {
                    productID = curProducts[0].productID,
                    developerPayload = "" + Random.Range(100000, 999999),
                };
                FBInstant.payments.purchaseAsync(purchaseConfig, OnPurchaseAsync);
                SetDebugText("purchasing " + purchaseConfig + ", please wait...");
            }
            else
            {
                SetDebugText("do now support this platform");
            }
        }

        private void OnPurchaseAsync(FBError err, Purchase purchase)
        {
            if (!FBInstant.payments.isSupportPayments())
            {
                SetDebugText("[OnPurchaseAsync] do now support this platform");
                return;
            }

            if (err == null || string.IsNullOrEmpty(err.code))
            {
                SetDebugText("[OnPurchaseAsync] purchase=" + purchase);
            }
            else
            {
                SetDebugText("[OnPurchaseAsync] purchase error:" + err);
            }
        }

        private void OnClick_TestGetPurchasesButton()
        {
            Debug.Log("OnClick_TestGetPurchasesButton");

            SetDebugText("isSupportPayments=" + FBInstant.payments.isSupportPayments());
            if (FBInstant.payments.isSupportPayments())
            {
                FBInstant.payments.getPurchasesAsync(OnGetPurchasesAsync);
                SetDebugText("getting my purchases, please wait...");
            }
            else
            {
                SetDebugText("do now support this platform");
            }
        }

        private void OnGetPurchasesAsync(FBError err, Purchase[] purchases)
        {
            if (!FBInstant.payments.isSupportPayments())
            {
                SetDebugText("[OnGetPurchasesAsync] do now support this platform");
                return;
            }

            curPurchases = null;
            if (err == null || string.IsNullOrEmpty(err.code))
            {
                curPurchases = purchases;
                if (curPurchases.Length > 0)
                {
                    for (int i = 0; i < curPurchases.Length; i++)
                    {
                        SetDebugText("[OnGetPurchasesAsync] purchase=" + curPurchases[i]);
                    }
                }
                else
                {
                    SetDebugText("[OnGetPurchasesAsync] no purchase");
                }
            }
            else
            {
                SetDebugText("[OnGetPurchasesAsync] get purchases error:" + err);
            }
        }

        private void OnClick_TestConsumeFirstPurchaseButton()
        {
            Debug.Log("OnClick_TestConsumeFirstPurchaseButton");

            SetDebugText("isSupportPayments=" + FBInstant.payments.isSupportPayments());
            if (FBInstant.payments.isSupportPayments())
            {
                FBInstant.payments.consumePurchaseAsync(curPurchases[0].purchaseToken, OnConsumePurchaseAsync);
                SetDebugText("consuming my purchase, please wait...");
            }
            else
            {
                SetDebugText("do now support this platform");
            }
        }
        
        private void OnConsumePurchaseAsync(FBError err)
        {
            if (!FBInstant.payments.isSupportPayments())
            {
                SetDebugText("[OnConsumePurchaseAsync] do now support this platform");
                return;
            }

            if (err == null || string.IsNullOrEmpty(err.code))
            {
                SetDebugText("[OnConsumePurchaseAsync] consume purchase ok");
            }
            else
            {
                SetDebugText("[OnConsumePurchaseAsync] consume purchase error:" + err);
            }

            curPurchases = null;

            OnClick_TestGetPurchasesButton(); //refresh data from server
        }

    }

}
