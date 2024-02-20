// LoaderModule에서 LoadAssetAsync 메소드를 추가해 비동기 처리를 통해 obj 파일을 로드할 수 있도록 구현해 주세요.

// - 제약 조건: 모델 파일은 obj 파일이며 최종적으로 play scene에 obj 파일이 동적 로드된 형태로 보여야 함.
// - 프로젝트 환경 설정 방법: Unity project 형태로 생성해서 Unity editor에서 열어서 바로 실행할 수 있는 형태여야 함
// - 리뷰 포인트: 1번 구현과 비교해 성능 최적화 관점, 가독성 관점 등
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
    {//예외 추가할것
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        await Load(selectedAssetName);
    }

    public async Task Load(string assetName)
    {
        for (int i = 0; i < 10; i++) {
            GameObject loadedAsset = loaderModuleasync.LoadAsset(assetName);
            loadedAsset.transform.SetParent(transform);
         // To do
         }

    }
}
