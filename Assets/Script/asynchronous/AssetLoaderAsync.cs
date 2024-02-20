using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

public class AssetLoaderAsync : MonoBehaviour
{
    [field: SerializeField]
    private LoaderModuleAsync loaderModuleasync; 

    private async void Start()
    {
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        await Load(selectedAssetName);
    }

    public async Task Load(string assetName)
    {
        GameObject loadedAsset = loaderModuleasync.LoadAsset(assetName);
        loadedAsset.transform.SetParent(transform);
         // To do
    }
}
