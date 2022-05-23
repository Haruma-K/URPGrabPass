<h1 align="center">URP Grab Pass</h1>

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](https://github.com/Haruma-K/URPGrabPass/blob/master/LICENSE.md)

[日本語ドキュメント(Japanese Documents Available)](README-JA.md)

Replacement for GrabPass in Unity's Universal Render Pipeline (URP).

<p align="center">
  <img width=700 src="https://user-images.githubusercontent.com/47441314/126611072-5976db79-a7f7-4be1-b2ef-3132f25afda4.png" alt="Demo">
</p>

## Features
* You can get the camera's color texture at the following timings.
  * After drawing opaque objects
  * After drawing transparent objects
* Then you can use it in your shader.
* In contrast to URP's \_CameraOpaqueTexture, you can also get the color texture after drawing semi-transparent objects.

## Requirement
Universal RP 10.2.0 or higher.  
Unity2020.2.3f1 or higher.  
VR is not supported.

## Usage

#### Setup Universal Render Pipeline
* Follow the manual below to set up the URP.
* https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.0/manual/InstallingAndConfiguringURP.html

#### Install URP Grab Pass
1. Open the Package Manager from Window > Package Manager
2. "+" button > Add package from git URL
3. Enter the following
   * https://github.com/Haruma-K/URPGrabPass.git?path=/Assets/URPGrabPass

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/126619790-da212335-08c6-4544-80fc-1bccd0e15a9b.png" alt="Package Manager">
</p>


Or, open Packages/manifest.json and add the following to the dependencies block.

```json
{
    "dependencies": {
        "com.harumak.urpgrabpass": "https://github.com/Haruma-K/URPGrabPass.git?path=/Assets/URPGrabPass"
    }
}
```

If you want to set the target version, specify it like follow.

* https://github.com/Haruma-K/URPGrabPass.git?path=/Assets/URPGrabPass#1.0.0

#### Setup Renderer Feature
* Add the Grab Pass Renderer Feature from the inspector of the Forward Renderer Data.

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/126620288-00c1c4c2-ec09-4c51-ac61-34df06fdb225.png" alt="Add Renderer Feature">
</p>

* Change the properties of the Renderer Feature if necessary.

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/126620820-15bf6d19-e69a-43e3-8c04-866403b2093a.png" alt="Set up Renderer Feature">
</p>

|Property Name|Description|
|-|-|
|Name|Name of the Renderer Feature.|
|Timing|When to get and use the camera's color texture.|
|Grabbed Texture Name|Name of the grabbed texture when it is used in the shader.|
|Shader Light Modes|LightModes for shaders that use the texture.|
|Sorting Criteria|How to sort objects during rendering.|

#### Write and use the shader
* Write the shader that uses the texture.
  * The shader must have the LightMode specified in the Renderer Feature.
  * You can get the texture by the name specified in the Renderer Feature.
  * Sample: https://github.com/Haruma-K/URPGrabPass/blob/master/Assets/Demo/Shaders/shader_demo_usegrabbedtexture.shader
* Assign this shader to the material to render it.
  * Objects with this material will be rendered immediately after the texture is grabbed.

## Demo
1. Clone this repository.
2. Open the following scene.
   * https://github.com/Haruma-K/URPGrabPass/blob/master/Assets/Demo/Scenes/scene_demo_main.unity

## License
This software is released under the MIT License.  
You are free to use it within the scope of the license.  
However, the following copyright and license notices are required for use.

* https://github.com/Haruma-K/URPGrabPass/blob/master/LICENSE.md
