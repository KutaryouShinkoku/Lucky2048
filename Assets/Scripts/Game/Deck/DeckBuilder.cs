using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] Image cubeImage;
    [SerializeField] Text cubeName;
    [SerializeField] Text cubeDes;
    public Queue<Cube> addedCube { get; set; } = new Queue<Cube>();

    public Cube cube;
    public bool isPicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Cube randomCube)
    {
        isPicked = false;
        cube = randomCube;
        cubeImage.sprite = randomCube.Base.Sprite;
        cubeName.text = randomCube.Base.CubeName;
        cubeDes.text = randomCube.Base.Description;
    }
    public void PickCube()
    {
        addedCube.Enqueue(cube);
    }

}
