using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

public class AssetLoaderAsync : MonoBehaviour
{
    [field: SerializeField]
    private LoaderModule loaderModuleasync; 

    private async void Start()
    {//예외 추가할것
        loaderModuleasync = new LoaderModule();
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        await Load(selectedAssetName);
    }

    public async Task Load(string assetNames)
    {
        var loadedAssets= Task.Run(()=> loaderModuleasync.LoadAssetAsync(assetNames));

        GameObject loadedAsset = await loadedAssets;
        loadedAsset.transform.SetParent(transform);
    }
}
