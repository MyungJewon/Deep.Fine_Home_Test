using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

public class AssetLoaderAsyncMulti : MonoBehaviour
{
    [field: SerializeField]
<<<<<<< HEAD
    private LoaderModule loaderModuleasyncmulti;
=======
    private LoaderModule loaderModuleAsyncMulti;
>>>>>>> origin/main
    public string fileExtension = ".obj";

    private async void Start()
    {
<<<<<<< HEAD
        loaderModuleasyncmulti = new LoaderModule();
=======
        loaderModuleAsyncMulti = new LoaderModule();
>>>>>>> origin/main
        string selectedAssetName = EditorUtility.OpenFolderPanel("Select obj model", "", "obj");
        string[] directoryPath = LoadFiles(selectedAssetName);
        await Load(directoryPath);
    }

    public async Task Load(string[] assetNames)
    {
        var tasks = new List<Task<GameObject>>();
        
        for (int i = 0; i < assetNames.Length; i++)
        {
<<<<<<< HEAD
            tasks.Add(loaderModuleasyncmulti.LoadAssetAsync(assetNames[i]));
            Debug.Log(assetNames[i]);
=======
            tasks.Add(loaderModuleAsyncMulti.LoadAssetAsync(assetNames[i]));
>>>>>>> origin/main
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
    
    public string[] LoadFiles(string directoryPath)
    {
        string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension)
                                  .Where(path => Path.GetExtension(path).Equals(fileExtension))
                                  .ToArray();
        return files;
    }
}
