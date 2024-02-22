using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class AssetLoaderAsyncMulti : MonoBehaviour
{
    [field: SerializeField]
    private LoaderModule loaderModuleAsyncMulti;
    public string fileExtension = ".obj";
    public string[] LoadFiles(string directoryPath)
    {
        string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension)
                                  .Where(path => Path.GetExtension(path).Equals(fileExtension))
                                  .ToArray();

        foreach (var file in files)
        {
            Debug.Log("Found file: " + file);
        }
        return files;
    }
    private async void Start()
    {
        loaderModuleAsyncMulti = new LoaderModule();
        string selectedAssetName = EditorUtility.OpenFolderPanel("Select obj model", "", "obj");
        string[] directoryPath = LoadFiles(selectedAssetName);
        await Load(directoryPath);
    }

    public async Task Load(string[] assetNames)
    {
        var tasks = new List<Task<GameObject>>();
        
        for (int i = 0; i < assetNames.Length; i++)
        {
            tasks.Add(loaderModuleAsyncMulti.LoadAssetAsync(assetNames[i]));
        }
        while(tasks.Count>0){
            var loadedAssets = await Task.WhenAny(tasks);
            GameObject loadedAsset =await loadedAssets;
            loadedAsset.transform.SetParent(transform);
            tasks.Remove(loadedAssets);
        }
        Debug.Log("작업끝");

        

        // for (int i = 0; i < tasks.Length; i++)
        // {
        //     GameObject loadedAsset = loadedAssets[i];
        //     loadedAsset.name = "i=" + i; 
        //     loadedAsset.transform.SetParent(transform);
        //     // To do
        // }
    }
}
