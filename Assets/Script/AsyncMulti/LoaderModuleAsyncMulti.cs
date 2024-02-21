using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class LoaderModuleAsyncMulti : MonoBehaviour
{
     ObjImporter objImporter = new ObjImporter();

    public async Task<GameObject> LoadAsset(string assetName,int i)
    {
        GameObject loadedAsset = objImporter.LoadObj(assetName);
        return loadedAsset;
    }
}
