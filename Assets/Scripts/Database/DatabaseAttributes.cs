using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyProject.Database
{
    /// <summary> �������Database�ĸ�field/property��Key </summary>
    public class DatabaseKey : Attribute
    {
        // TODO! ��һЩrequirement�������������class�ϣ������޶�ΪDatabaseAssetBase��DerivedClass
        public readonly string keyName;
        public DatabaseKey(string keyName)
        {
            this.keyName = keyName;
        }
    }
}
