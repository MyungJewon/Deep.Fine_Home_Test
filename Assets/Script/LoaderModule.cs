using UnityEngine;
<<<<<<< HEAD
using System; // 이벤트 정의를 위해 추가
using Dummiesman;
using System.Threading.Tasks;

public class LoaderModule : MonoBehaviour
{
    bool temp = false;
=======
using System;
using System.Threading.Tasks; // 이벤트 정의를 위해 추가

public class LoaderModule : MonoBehaviour
{
>>>>>>> origin/main
    ObjImporter objImporter= new ObjImporter();
    public event Action<GameObject> OnLoadCompleted; 

    public void LoadAsset(string assetName)
    {
<<<<<<< HEAD
        //GameObject loadedObject = new OBJLoader().Load(assetName);
        //OnLoadCompleted?.Invoke(loadedObject);
        GameObject loadedAsset = objImporter.LoadObj(assetName);
        OnLoadCompleted?.Invoke(loadedAsset);
    }
    public async Task<GameObject> LoadAssetAsync(string assetName)
    {
        //GameObject loadedObject = new OBJLoader().Load(assetName);
        GameObject loadedObject = objImporter.LoadObj(assetName);
        return loadedObject;
=======
        GameObject loadedAsset = objImporter.LoadObj(assetName);
        OnLoadCompleted?.Invoke(loadedAsset);      
    }
        public async Task<GameObject> LoadAssetAsync(string assetName)
    {
        GameObject loadedAsset = objImporter.LoadObj(assetName);
        return loadedAsset;
>>>>>>> origin/main
    }
}