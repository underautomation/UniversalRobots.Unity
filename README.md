# Universal Robots SDK for Unity

This Unity project connects a Unity scene to real Universal Robots arms with the
[UnderAutomation Universal Robots SDK](https://underautomation.com/universal-robots). Use it as a starting
point for digital twins, 3D simulations and supervision of robot cells.

<p align="center">
<img src="https://raw.githubusercontent.com/underautomation/UniversalRobots.Unity/refs/heads/main/.github/assets/ur-unity.gif" alt="UR demo Unity"/>
</p>

- Product page: [underautomation.com/universal-robots](https://underautomation.com/universal-robots)
- Documentation: [underautomation.com/universal-robots/documentation](https://underautomation.com/universal-robots/documentation)
- Also available for [.NET](https://github.com/underautomation/UniversalRobots.NET), [Python](https://github.com/underautomation/UniversalRobots.py) and [LabVIEW](https://github.com/underautomation/UniversalRobots.vi)

## What the project contains

- **The SDK:** `Assets/Plugins/UnderAutomation.UniversalRobots.dll`, the .NET Framework 3.5 build of the
  SDK. It gives the same interfaces as the .NET package: RTDE (up to 500 Hz), Primary Interface and
  URScript, Dashboard Server, REST API, SSH and SFTP, kinematics.
- **A demo scene** (`Assets/Scenes/LoadURDF.unity`, script `Assets/Test.cs`): it loads the UR3, UR5, UR10,
  UR3e, UR5e, UR10e, UR16e, UR20 and UR30 models, connects to a robot and moves the 3D model with the
  joint positions of the real robot.
- **3D models of the arms:** URDF descriptions and meshes taken from
  [ros-industrial/universal_robot](https://github.com/ros-industrial/universal_robot). Only the 3D files are
  used: the project does not need ROS. The meshes of some models come with the terms of use of Universal
  Robots (`LICENSE.txt` in their folder).
- **URDF loader:** [gkjohnson/urdf-loaders](https://github.com/gkjohnson/urdf-loaders), in
  `Assets/URDFLoader`, with support of the DAE files in the Unity Editor.

The DLL is fully managed, with no native dependency.

## How to start

1. Clone this repository and open the folder in Unity Hub.
2. Open the scene `Assets/Scenes/LoadURDF.unity`.
3. Press Play, type the IP address of your robot or of URSim in the field at the top left, then connect.

The Release workflow of the SDK updates the DLL of `Assets/Plugins` on the `main` branch at each release.
You can also replace it with the `net35` DLL of the [UniversalRobots.NET releases](https://github.com/underautomation/UniversalRobots.NET/releases).

## Compatibility

- **Robots:** CB-Series and e-Series robots and the newer models, with PolyScope or PolyScope X, and URSim.
- **Operating systems:** Windows, Linux, macOS, iOS, Android.
- **Unity:** the DLL targets .NET Framework 3.5.

## License

The SDK needs a commercial license. A 30-day trial starts at the first use, no key needed.

- License agreement: [underautomation.com/universal-robots/eula](https://underautomation.com/universal-robots/eula) and [License.md](License.md)
- Third-party code included in the DLL: [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt) (added at the next release)
- Trial, license key and source license: [underautomation.com/universal-robots/documentation/license](https://underautomation.com/universal-robots/documentation/license)
- Prices and quote: [underautomation.com/universal-robots](https://underautomation.com/universal-robots)

## Support

- Documentation: [underautomation.com/universal-robots/documentation](https://underautomation.com/universal-robots/documentation)
- Issues: [GitHub Issues](https://github.com/underautomation/UniversalRobots.Unity/issues)
- Contact: [underautomation.com/contact](https://underautomation.com/contact)
