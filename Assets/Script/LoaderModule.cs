using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
public class LoaderModule : MonoBehaviour
{
    public LoaderModule()
    {
        public event Action<GameObject> OnLoadCompleted; // 모델 로드 완료 이벤트

        public void LoadAsset(string assetName){}

    }
   
}