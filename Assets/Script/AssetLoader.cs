using UnityEngine;
using UnityEditor; 

public class AssetLoader : MonoBehaviour
{
    [SerializeField]
    private LoaderModule loaderModule; 

    private void Start()
    {
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        Load(selectedAssetName);
    }

    public void Load(string assetName)
    {
        loaderModule.LoadAsset(assetName); 
        loaderModule.OnLoadCompleted += OnLoadCompleted; 
    }

    private void OnLoadCompleted(GameObject loadedAsset)
    {
        Debug.Log("sdfasdf");
        loadedAsset.transform.SetParent(transform);
        // To do
    }
}