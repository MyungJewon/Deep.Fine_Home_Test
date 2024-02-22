using UnityEngine;
using System;
using System.Threading.Tasks; // 이벤트 정의를 위해 추가

public class LoaderModule : MonoBehaviour
{
    ObjImporter objImporter= new ObjImporter();
    public event Action<GameObject> OnLoadCompleted; 

    public void LoadAsset(string assetName)
    {
        GameObject loadedAsset = objImporter.LoadObj(assetName);
        OnLoadCompleted?.Invoke(loadedAsset);      
    }
        public async Task<GameObject> LoadAssetAsync(string assetName)
    {
        GameObject loadedAsset = objImporter.LoadObj(assetName);
        return loadedAsset;
    }
}