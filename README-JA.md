<h1 align="center">URP Grab Pass</h1>

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](https://github.com/Haruma-K/URPGrabPass/blob/master/LICENSE.md)

[English Documents Available(英語ドキュメント)](README.md)

Universal Render Pipeline(URP)においてGrabPass相当の機能を提供します。

<p align="center">
  <img width=700 src="https://user-images.githubusercontent.com/47441314/126611072-5976db79-a7f7-4be1-b2ef-3132f25afda4.png" alt="Demo">
</p>

## 機能
* カメラのカラーテクスチャを以下のタイミングで取得できます。
  * 不透明オブジェクトの描画後
  * 半透明オブジェクトの描画後
* そしてそれをシェーダ内で使用できます。
* URPの\_CameraOpaqueTextureとの違いとして、半透明描画後のカラーテクスチャも取得できます。

## 要件
Universal RP 10.2.0 以上  
Unity2020.2.3f1 以上  
VRデバイスは非サポート

## 使い方

#### Universal Render Pipelineをセットアップ
* マニュアルに従ってURPをセットアップします
* https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.0/manual/InstallingAndConfiguringURP.html

#### URP Grab Passをインストール
1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下を入力
   * https://github.com/Haruma-K/URPGrabPass.git?path=/Assets/URPGrabPass

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/126619790-da212335-08c6-4544-80fc-1bccd0e15a9b.png" alt="Package Manager">
</p>

あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記します。

```json
{
    "dependencies": {
        "com.harumak.urpgrabpass": "https://github.com/Haruma-K/URPGrabPass.git?path=/Assets/URPGrabPass"
    }
}
```

バージョンを指定したい場合には以下のように記述します。

* https://github.com/Haruma-K/URPGrabPass.git?path=/Assets/URPGrabPass#1.0.0

#### Renderer Featureをセットアップ
* Forward Renderer DataのインスペクタからGrab Pass Renderer Featureを追加します。

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/126620288-00c1c4c2-ec09-4c51-ac61-34df06fdb225.png" alt="Add Renderer Feature">
</p>

* 必要に応じてRenderer Featureのプロパティを変更します。

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/126620820-15bf6d19-e69a-43e3-8c04-866403b2093a.png" alt="Set up Renderer Feature">
</p>

|プロパティ名|説明|
|-|-|
|Name|Renderer Featureの名前|
|Timing|カメラのカラーテクスチャを取得するタイミング|
|Grabbed Texture Name|取得したカラーテクスチャをシェーダで使う際の名前|
|Shader Light Modes|カラーテクスチャを使用するシェーダのLightMode|
|Sorting Criteria|カラーテクスチャを使用するオブジェクトのソート方法|

#### シェーダを書いて描画する
* カラーテクスチャを使うシェーダを作成する。
  * このシェーダはRenderer Featureで指定したLight Modeを持つ必要があります。
  * カラーテクスチャはRenderer Featureで指定した名前で取得できます。
  * サンプル: https://github.com/Haruma-K/URPGrabPass/blob/master/Assets/Demo/Shaders/shader_demo_usegrabbedtexture.shader
* このシェーダをマテリアルにアサインします。
  * このマテリアルがアサインされたオブジェクトはテクスチャが取得された直後のパスで描画されます。

## デモ
デモシーンは以下の手順で確認できます。

1. リポジトリをクローン
2. 以下のシーンを開く
   * https://github.com/Haruma-K/URPGrabPass/blob/master/Assets/Demo/Scenes/scene_demo_main.unity

## License
本ソフトウェアはMITライセンスで公開しています。  
ライセンスの範囲内で自由に使っていただいてかまいませんが、  
使用の際は以下の著作権表示とライセンス表示が必須となります。

* https://github.com/Haruma-K/URPGrabPass/blob/master/LICENSE.md
