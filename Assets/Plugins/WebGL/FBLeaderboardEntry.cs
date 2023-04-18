using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QMG
{

    [System.Serializable]
    public class FBLeaderboardEntry
    {

        public int rank;
        public string avatarUrl;
        public string nickName;
        public int score;
        public Dictionary<string, object> extraData;


    }

}