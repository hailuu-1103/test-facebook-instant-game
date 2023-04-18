using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


namespace QMG
{

	/// <summary>
	/// 游戏初始化时调用的类,基本上就是Unity加载第一个场景时会被调用一次的方法
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class IGExporter : MonoBehaviour
	{
        public static string globalGoName = "__IGEXPORTER__";

        public static System.Action shareAsync_Callback = null;

        public static System.Action ShowInterstitialAd_Preload_Method = null;

        public static System.Action ShowRewaredVideoAd_Preload_Method = null;
        public static System.Action ShowRewaredVideoAd_Complete_Callback = null;
        public static System.Action<FBError> ShowRewaredVideoAd_Error_Callback = null;

        public static System.Action<bool> PreloadRewaredVideoAd_Ready_Callback = null;

        /// <summary>
        /// Game Logic Entry, must be set to a MonoBehaviour Object, can use typeof to get the type
        /// </summary>
        public static System.Type gameLogicEntryTypeName = null;

        /// <summary>
        /// 首个场景开始加载前调用此方法
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void IGExporterGameStart()
		{
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Debug.LogWarning("IGExporter|start|return|direct");
                return;
            }

            Debug.Log("BeforeSceneLoad|IGExporter|start");

			GameObject go = new GameObject();
			go.name = IGExporter.globalGoName;
			DontDestroyOnLoad(go);

			go.AddComponent<IGExporter>();

            Debug.Log("BeforeSceneLoad|IGExporter|end");
        }

		/// <summary>
		/// 首个场景完成加载后调用此方法
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void IGExporterGameEnd()
		{
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Debug.LogWarning("IGExporter|start|return|direct");
                return;
            }

            Debug.Log("AfterSceneLoad|IGExporter|start");

            Debug.Log("AfterSceneLoad|IGExporter|end");
        }

        #region Common Promise Callback
        private void Promise_on_context_chooseAsync()
        {
            Debug.Log("Promise_on_context_chooseAsync");

            if (GameContext.chooseAsync_Callback != null)
            {
                GameContext.chooseAsync_Callback();
            }
        }

        private void Promise_on_context_getPlayersAsync(string jsonStr)
        {
            Debug.Log("Promise_on_context_getPlayersAsync");

            if (GameContext.getPlayersAsync_Callback != null)
            {
                ContextPlayerEntry[] playerArr = SimpleJson.SimpleJson.DeserializeObject<ContextPlayerEntry[]>(jsonStr);
                GameContext.getPlayersAsync_Callback(playerArr);
            }
        }

        private void Promise_on_leaderboard_setScoreAsync()
        {
            Debug.Log("Promise_on_leaderboard_setScoreAsync");

            if (FBLeaderboard.setScoreAsync_Callback != null)
            {
                FBLeaderboard.setScoreAsync_Callback();
            }
        }

        private void Promise_on_leaderboard_getPlayerEntryAsync(string jsonStr)
        {
            Debug.Log("Promise_on_leaderboard_getPlayerEntryAsync");
            if (FBLeaderboard.getPlayerEntryAsync_Callback != null)
            {   
                FBLeaderboardEntry entry = SimpleJson.SimpleJson.DeserializeObject<FBLeaderboardEntry>(jsonStr);
                FBLeaderboard.getPlayerEntryAsync_Callback(entry);
            }
        }

        private void Promise_on_leaderboard_getEntriesAsync(string jsonStr)
        {
            Debug.Log("Promise_on_leaderboard_getEntriesAsync");
            if (FBLeaderboard.getEntriesAsync_Callback != null)
            {
                FBLeaderboardEntry[] entries = SimpleJson.SimpleJson.DeserializeObject<FBLeaderboardEntry[]>(jsonStr);
                FBLeaderboard.getEntriesAsync_Callback(entries);
            }
        }

        private void Promise_on_leaderboard_getConnectedPlayerEntriesAsync(string jsonStr)
        {
            Debug.Log("Promise_on_leaderboard_getConnectedPlayerEntriesAsync");
            if (FBLeaderboard.getConnectedPlayerEntriesAsync_Callback != null)
            {
                FBLeaderboardEntry[] entries = SimpleJson.SimpleJson.DeserializeObject<FBLeaderboardEntry[]>(jsonStr);
                FBLeaderboard.getConnectedPlayerEntriesAsync_Callback(entries);
            }
        }

        private void Promise_on_player_getDataAsync(string jsonStr)
        {
            Debug.Log("Promise_on_player_getDataAsync");
            Dictionary<string, object> data = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, object>>(jsonStr);
            if (ContextPlayer.getDataAsync_Callback != null)
            {
                ContextPlayer.getDataAsync_Callback(data);
            }
        }

        private void Promise_on_player_setDataAsync()
        {
            Debug.Log("Promise_on_player_setDataAsync");
            if (ContextPlayer.setDataAsync_Callback != null)
            {
                ContextPlayer.setDataAsync_Callback();
            }
        }

        private void Promise_on_player_canSubscribeBotAsync(string jsonStr)
        {
            Debug.Log("Promise_on_player_canSubscribeBotAsync");
            bool can_subscribe = SimpleJson.SimpleJson.DeserializeObject<bool>(jsonStr);
            if (ContextPlayer.canSubscribeBotAsync_Callback != null)
            {
                ContextPlayer.canSubscribeBotAsync_Callback(can_subscribe);
            }
        }

        private void Promise_on_player_subscribeBotAsync_Success()
        {
            Debug.Log("Promise_on_player_subscribeBotAsync_Success");
            if (ContextPlayer.subscribeBot_Success_Callback != null)
            {
                ContextPlayer.subscribeBot_Success_Callback();
            }
        }

        private void Promise_on_player_subscribeBotAsync_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_player_subscribeBotAsync_Error");
            if (ContextPlayer.subscribeBot_Error_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                ContextPlayer.subscribeBot_Error_Callback(err);
            }
        }

        private void Promise_on_fbinstant_shareAsync()
        {
            Debug.Log("Promise_on_fbinstant_shareAsync");
            if (shareAsync_Callback != null)
            {
                shareAsync_Callback();
            }
        }

        private void Promise_on_fbinstant_showInterstitialAdAsync()
        {
            Debug.Log("Promise_on_fbinstant_showInterstitialAdAsync");
            if (ShowInterstitialAd_Preload_Method != null)
            {
                ShowInterstitialAd_Preload_Method();
            }
        }

        private void Promise_on_fbinstant_showRewardedVideoAsync_Complete()
        {
            Debug.Log("Promise_on_fbinstant_showRewardedVideoAsync_Complete");
            if (ShowRewaredVideoAd_Complete_Callback != null)
            {
                ShowRewaredVideoAd_Complete_Callback();
            }
        }

        private void Promise_on_fbinstant_showRewardedVideoAsync_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_fbinstant_showRewardedVideoAsync_Error");
            if (ShowRewaredVideoAd_Error_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                ShowRewaredVideoAd_Error_Callback(err);
            }
        }

        private void Promise_on_fbinstant_showRewardedVideoAsync_Preload()
        {
            Debug.Log("Promise_on_fbinstant_showRewardedVideoAsync_Preload");
            if (ShowRewaredVideoAd_Preload_Method != null)
            {
                ShowRewaredVideoAd_Preload_Method();
            }
        }

        private void Promise_on_fbinstant_getRewardedVideoAsync(string jsonStr)
        {
            Debug.Log("Promise_on_fbinstant_getRewardedVideoAsync");
            bool isReady = SimpleJson.SimpleJson.DeserializeObject<bool>(jsonStr);
            if (PreloadRewaredVideoAd_Ready_Callback != null)
            {
                PreloadRewaredVideoAd_Ready_Callback(isReady);
            }
        }


        private void Promise_on_payments_onReady_Callback()
        {
            FBPayments.isOnReadyOk = true;
            Debug.Log("Promise_on_payments_onReady_Callback");
            if (FBPayments.payments_onReady_Callback != null)
            {
                FBPayments.payments_onReady_Callback();
            }
        }

        private void Promise_on_getCatalogAsync_Callback(string jsonStr)
        {
            Debug.Log("Promise_on_getCatalogAsync_Callback");
            if (FBPayments.getCatalogAsync_Callback != null)
            {
                Product[] products = SimpleJson.SimpleJson.DeserializeObject<Product[]>(jsonStr);
                FBPayments.getCatalogAsync_Callback(null, products);
            }
        }

        private void Promise_on_getCatalogAsync_Callback_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_getCatalogAsync_Callback_Error");
            if (FBPayments.getCatalogAsync_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                FBPayments.getCatalogAsync_Callback(err, new Product[] { });
            }
        }

        private void Promise_on_purchaseAsync_Callback(string jsonStr)
        {
            Debug.Log("Promise_on_purchaseAsync_Callback");
            if (FBPayments.purchaseAsync_Callback != null)
            {
                Purchase purchase = SimpleJson.SimpleJson.DeserializeObject<Purchase>(jsonStr);
                FBPayments.purchaseAsync_Callback(null, purchase);
            }
        }

        private void Promise_on_purchaseAsync_Callback_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_purchaseAsync_Callback_Error");
            if (FBPayments.purchaseAsync_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                FBPayments.purchaseAsync_Callback(err, new Purchase());
            }
        }

        private void Promise_on_getPurchasesAsync_Callback(string jsonStr)
        {
            Debug.Log("Promise_on_getPurchasesAsync_Callback");
            if (FBPayments.getPurchasesAsync_Callback != null)
            {
                Purchase[] purchases = SimpleJson.SimpleJson.DeserializeObject<Purchase[]>(jsonStr);
                FBPayments.getPurchasesAsync_Callback(null, purchases);
            }
        }

        private void Promise_on_getPurchasesAsync_Callback_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_getPurchasesAsync_Callback_Error");
            if (FBPayments.getPurchasesAsync_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                FBPayments.getPurchasesAsync_Callback(err, null);
            }
        }

        private void Promise_on_consumePurchaseAsync_Callback()
        {
            Debug.Log("Promise_on_consumePurchaseAsync_Callback");
            if (FBPayments.consumePurchaseAsync_Callback != null)
            {
                FBPayments.consumePurchaseAsync_Callback(null);
            }
        }

        private void Promise_on_consumePurchaseAsync_Callback_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_consumePurchaseAsync_Callback_Error");
            if (FBPayments.consumePurchaseAsync_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                FBPayments.consumePurchaseAsync_Callback(err);
            }
        }
        #endregion

        /// <summary>
        /// 在WebGL的Facebook Instant测试中发现,加载的时候,已经把首个场景加载进来的了,所以这里写一个方法用于在startGameAsync触发游戏逻辑,对于非WebGL的,应该要判断
        /// </summary>
        void StartGameLogic()
        {
            Debug.Log("StartGameLogic|start");
            // CUSTOM CODE HERE START
            if (gameLogicEntryTypeName != null)
            {
                Debug.Log("StartGameLogic|type=" + gameLogicEntryTypeName);
                this.gameObject.AddComponent(gameLogicEntryTypeName); //Game Logic Entry
            }
            else
            {
                Debug.LogError("StartGameLogic|notype: Please open TestFB.cs [TestFBGameStart] get a sample code");
            }
            
            // CUSTOM CODE HERE END
            Debug.Log("StartGameLogic|end");
        }

	}

}
