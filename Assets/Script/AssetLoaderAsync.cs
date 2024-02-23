using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;
using System;

public class AssetLoaderAsync : MonoBehaviour
{
    [field: SerializeField]
    private LoaderModule loaderModuleasync; 

    private async void Start()
    {
        loaderModuleasync = new LoaderModule();
        try{
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        await Load(selectedAssetName);
        }catch(Exception ex){   //Data 예외처리
            Debug.Log("Data type(obj) error");
        }
    }

    public async Task Load(string assetNames)
    {
         var loadedAssets = loaderModuleasync.LoadAssetAsync(assetNames);   //objread 실행
         GameObject loadedAsset = await loadedAssets;   //작업완료까지 Main threads 정지
         loadedAsset.transform.SetParent(transform);
    }
}
