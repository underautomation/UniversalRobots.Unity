using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnderAutomation.UniversalRobots;
using UnderAutomation.UniversalRobots.PrimaryInterface;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.Rendering;

public class Test : MonoBehaviour
{
    private ConnectParameters connectParameters = new ConnectParameters();
    private readonly UR robot = new UR();

    private static float Rad(double degrees)
    {
        return (float)(degrees * Mathf.Deg2Rad);
    }

    private float x = 0;
    private float y = 0;
    private float z = 0;

    private readonly Dictionary<string, float> angles = new Dictionary<string, float>();

    private void Load(string name)
    {
        Dictionary<string, string> packages = new Dictionary<string, string>();
        packages["ur_description"] = "ur_description";

        var options = new URDFLoader.Options()
        {
            loadMeshCb = LoadMesh
        };

        var asset = Resources.Load<TextAsset>("ur_description/urdf/" + name + ".urdf");

        var robot = URDFLoader.Parse(asset.text, packages, options);

        robot.name = Path.GetFileNameWithoutExtension(asset.name);

        robot.transform.position = new Vector3(-x, y, z);

        var textPrefab = Resources.Load("Text");

        var textObject = Instantiate(textPrefab, robot.transform);
        textObject.GetComponent<TextMesh>().text = name;

        x += 0.8f;
    }

    void Start()
    {
        robot.PrimaryInterface.JointDataReceived += JointDataReceived;

        angles["shoulder_pan_joint"] = Rad(-84);
        angles["shoulder_lift_joint"] = Rad(-137);
        angles["elbow_joint"] = Rad(103);
        angles["wrist_1_joint"] = Rad(-50);
        angles["wrist_2_joint"] = Rad(-60);
        angles["wrist_3_joint"] = 0;


        y = 1.5f;

        Load("ur3e");
        Load("ur5e");
        Load("ur10e");
        Load("ur16e");
        Load("ur30");
        Load("ur20");

        x = 0;
        y = 0;

        Load("ur3");
        Load("ur5");
        Load("ur10");

    }

    private void JointDataReceived(object sender, JointDataPackageEventArgs e)
    {
        angles["shoulder_pan_joint"] = (float)e.Base.Position;
        angles["shoulder_lift_joint"] = (float)e.Shoulder.Position;
        angles["elbow_joint"] = (float)e.Elbow.Position;
        angles["wrist_1_joint"] = (float)e.Wrist1.Position;
        angles["wrist_2_joint"] = (float)e.Wrist2.Position;
        angles["wrist_3_joint"] = (float)e.Wrist3.Position;
    }


    private void LoadMesh(string path, string ext, Action<GameObject[]> done)
    {
        var resourceName = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)).Replace("\\", "/");

        if (ext != "dae")
        {
            throw new Exception("Filetype '" + ext + "' not supported");
        }

        var prefab = Resources.Load<GameObject>(resourceName);


        if (prefab == null)
        {
            Debug.LogError("Failed to load mesh: " + resourceName);
            return;
        }

        var gameObject = Instantiate(prefab);

        var childToDestroy = new List<Transform>();

        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<MeshFilter>() == null)
            {
                childToDestroy.Add(child);
            }

        }

        foreach (var child in childToDestroy)
        {
            DestroyImmediate(child.gameObject);
        }

        done(new[] { gameObject });
    }




    // Update is called once per frame
    void Update()
    {
        foreach (var robot in FindObjectsByType<URDFRobot>(FindObjectsSortMode.None))
        {
            robot.SetAnglesFromDictionary(angles);
        }
    }

    void OnGUI()
    {
        if (robot.Enabled) return;

        GUI.Label(new Rect(10, 10, 20, 20), "IP :");
        connectParameters.IP = GUI.TextField(new Rect(40, 10, 100, 20), connectParameters.IP ?? "", 50);

        if (GUI.Button(new Rect(150, 10, 80, 20), "Connect"))
        {
            robot.Connect(connectParameters);
        }
    }
}
