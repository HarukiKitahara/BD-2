using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyProject.Database
{
    /// <summary> 用来标记Database哪个field/property是Key </summary>
    public class DatabaseKey : Attribute
    {
        // TODO! 加一些requirement，比如必须标记在class上，甚至限定为DatabaseAssetBase的DerivedClass
        public readonly string keyName;
        public DatabaseKey(string keyName)
        {
            this.keyName = keyName;
        }
    }
}
