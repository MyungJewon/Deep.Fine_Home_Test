using UnityEngine;
using UnityEditor;

public class AssetLoader : MonoBehaviour
{
    [SerializeField]
    private LoaderModule loaderModule;

    private void Start()
    {
        loaderModule = new LoaderModule();
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        Load(selectedAssetName);
    }
    public void Load(string assetName)
    {
        loaderModule.OnLoadCompleted += OnLoadCompleted;
        loaderModule.LoadAsset(assetName);
    }
    private void OnLoadCompleted(GameObject loadedAsset)
    {
        loadedAsset.transform.SetParent(transform);
        // To do
    }
}