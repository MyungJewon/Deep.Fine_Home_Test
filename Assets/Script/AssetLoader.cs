using UnityEngine;
using UnityEditor;
using System;
public class AssetLoader : MonoBehaviour
{
    [SerializeField]
    private LoaderModule loaderModule;
    private void Start()
    {
        loaderModule = new LoaderModule();
        try{
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        Load(selectedAssetName);
        }catch(Exception ex){   //Data 예외처리
            Debug.Log("Data type(obj) error");
        }
    }

    public void Load(string assetName)
    {
        loaderModule.OnLoadCompleted += OnLoadCompleted;    //이벤트 추가
        loaderModule.LoadAsset(assetName);                  //함수호출
    }
    private void OnLoadCompleted(GameObject loadedAsset)
    {
        loadedAsset.transform.SetParent(transform);
        // To do
    }
}