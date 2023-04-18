using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QMG
{

    public class FBError
    {

        public string code;
        public string message;

        public override string ToString()
        {
            return "FBError: code=" + code + ",message=" + message;
        }


    }

}
