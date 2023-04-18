using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QMG
{

    /// <summary>
    /// Represents an individual purchase of a game product.
    /// </summary>
    [System.Serializable]
    public class Purchase
    {

        /// <summary>
        /// A developer-specified string, provided during the purchase of the product
        /// </summary>
        public string developerPayload = "";

        /// <summary>
        /// The identifier for the purchase transaction
        /// </summary>
        public string paymentID;

        /// <summary>
        /// The product's game-specified identifier
        /// </summary>
        public string productID;

        /// <summary>
        /// Unix timestamp of when the purchase occurred
        /// </summary>
        public long purchaseTime;

        /// <summary>
        /// A token representing the purchase that may be used to consume the purchase
        /// </summary>
        public string purchaseToken;

        /// <summary>
        /// Server-signed encoding of the purchase request
        /// </summary>
        public string signedRequest;

        public override string ToString()
        {
            return "Purchase: productID=" + productID + ",paymentID=" + paymentID + ",purchaseTime=" + purchaseTime
                + ",purchaseToken=" + purchaseToken + ",developerPayload=" + developerPayload + ",signedRequest=" + signedRequest;
        }

    }

}
