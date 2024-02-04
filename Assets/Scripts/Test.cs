using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] Cube cube;

    // Start is called before the first frame update
    void Start()
    {
        cube.Init();
        Debug.Log($"名称：{cube.Base.CubeName}\n描述：{cube.Base.Description}\n");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void testCubeEffects()
    {
        cube.ResolveSkills();
    }
}
