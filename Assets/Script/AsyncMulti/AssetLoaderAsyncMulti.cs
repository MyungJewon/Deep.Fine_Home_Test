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
    private LoaderModuleAsyncMulti loaderModuleAsyncMulti;
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
        loaderModuleAsyncMulti = new LoaderModuleAsyncMulti();
        string selectedAssetName = EditorUtility.OpenFolderPanel("Select obj model", "", "obj");
        string[] directoryPath = LoadFiles(selectedAssetName);
        await Load(directoryPath);
    }

    public async Task Load(string[] assetNames)
    {
        var tasks = new List<Task<GameObject>>();
        
        for (int i = 0; i < assetNames.Length; i++)
        {
            tasks.Add(loaderModuleAsyncMulti.LoadAsset(assetNames[i],i));
        }
        var loadedAssets = await Task.WhenAll(tasks);

        for (int i = 0; i < loadedAssets.Length; i++)
        {
            GameObject loadedAsset = loadedAssets[i];
            loadedAsset.name = "i=" + i; 
            loadedAsset.transform.SetParent(transform);
            // To do
        }
    }
}
