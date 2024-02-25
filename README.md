# Deep.Fine_Home_Test

## 개요

1. .obj파일의 내부 정점과, 메쉬를 읽고 유니티로 가져오는 코드
2. 동기, 비동기 시스템구현

## 특징

1. 복합 .obj파일을 그대로 import 가능

![유니티 import를 사용한 .obj읽기](![Untitled 1](https://github.com/MyungJewon/Deep.Fine_Home_Test/assets/54784949/d636ebe5-c406-4fab-90d4-aae3ff2a8eab)
)

유니티 import를 사용한 .obj읽기

![구현한 시스템을 사용한 .obj읽기](![Untitled](https://github.com/MyungJewon/Deep.Fine_Home_Test/assets/54784949/6890f40e-d2d4-4fcb-ac88-31b33aac3d96)
)

구현한 시스템을 사용한 .obj읽기

1. AssetLoaderAsyncMulti.cs 의 경우 복수의 .obj를 읽기 유용하게 하기 위해 파일이 아닌 폴더를 읽도록 함

```csharp
public string[] LoadFiles(string directoryPath) //Directory에서 fileExtension 확장자만 저장
    {
        string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension)
                                  .Where(path => Path.GetExtension(path).Equals(fileExtension))
                                  .ToArray();
        return files;
    }
```

## 코드 설명

### AssetLoader.cs

1. void Load(assetName): 경로를 받아 이벤트를 추가하고 LoaderModule를 실행 시키는 함수
2. void OnLoadCompleted(loadedAsset): 이벤트를 사용한  gameobject처리

### AssetLoaderAsync.cs

1. Task Load(assetNames): 경로를 받고 LoaderModule를 실행시키는 함수, gameobject처리

### AssetLoaderAsyncMulti.cs

1. string[] LoadFiles(directoryPath): 폴더에서 .obj파일을 읽고 배열화 하는 함수 
2. Task Load(assetNames[]): 모든 작업을 Task화 하고 실행시키는 함수

### LoaderModule.cs

1. void LoadAsset(path): 동기 방식에서 사용되는 함수
2. Task<GameObject> LoadAssetAsync(path): 비동기 방식에서 사용되는 함수
3. GameObject Addmeshtoarrayfromobj(loadedObject , gameobjectname) : 계산된 vertices와 mesh를 가지고 GameObject를 만드는 함수

### ObjImporter.cs

1. List<Verticesmap> LoadObj(path): LoaderModule에서 호출되는 함수, .obj를 읽고 vertices와 mesh, group을 추출하는 함
2. class Verticesmap: 그룹별 데이터(mesh, 그룹명) 저장편의를 위해 생성된 class선언
