using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour
{
    public GameObject[] cubes;
    [SerializeField]
    [Range(0.0f, 20.0f)]
    public float rotSpeed = 5.0f;

    [SerializeField] private Canvas canvas;
    private UIController uiController;
    private GameObject[,,] _cube;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        uiController = canvas.GetComponent<UIController>();
        _cube = new GameObject[3, 3, 3];
        int currentCube = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    if (x == 1 && y == 1 && z == 1)
                        continue;
                    else
                        _cube[x, y, z] = cubes[currentCube];
                    currentCube++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Dictate the direction of face rotation. Shift inverts the direction
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            direction = -1;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            direction = 1;
        }

        // Controls to turn the faces of the cube
        //      Up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Up(direction);
            RotateY(2, direction);
        }

        //      Right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Right(direction);
            RotateX(2, direction);
        }

        //      Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateX(0, -direction);
        }
        //      Front
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RotateZ(0, direction);
        }

        // Controls to rotate the entire cube
        //      Rotate cube counterclockwise on y axis
        if (Input.GetKeyDown("a"))
        {
            for (int i = 0; i < 3; i++)
                RotateY(i, 1);
        }
        //      Rotate cube clockwise on y axis
        if (Input.GetKeyDown("d"))
        {
            for (int i = 0; i < 3; i++)
                RotateY(i, -1);
        }
        //      Rotate cube up (see bottom)
        if (Input.GetKeyDown("w"))
        {
            for (int i = 0; i < 3; i++)
                RotateX(i, 1);
        }
        //      Rotate cube down (see top)
        if (Input.GetKeyDown("s"))
        {
            for (int i = 0; i < 3; i++)
                RotateX(i, -1);
        }
        //      Rotate cube clockwise on z axis
        if (Input.GetKeyDown("e"))
        {
            for (int i = 0; i < 3; i++)
                RotateZ(i, 1);
        }
        //      Rotate cube counterclockwise on z axis
        if (Input.GetKeyDown("q"))
        {
            for (int i = 0; i < 3; i++)
                RotateZ(i, -1);
        }
    }

    private void RotateX(int layer, int turn)
    {
        // Rotates graphics 
        for (int y = 0; y < 3; y++)
            for (int z = 0; z < 3; z++)
                if (layer == 1 && y == 1 && z == 1)
                    continue;
                else
                {
                    _cube[layer, z, y].transform.Rotate(new Vector3(90 * turn, 0, 0), Space.World);
                }


        GameObject temp;
        if (turn < 0)
        {
            // Swap edges
            temp = _cube[layer, 1, 2];
            _cube[layer, 1, 2] = _cube[layer, 2, 1];
            _cube[layer, 2, 1] = _cube[layer, 1, 0];
            _cube[layer, 1, 0] = _cube[layer, 0, 1];
            _cube[layer, 0, 1] = temp;

            // Swap corners
            temp = _cube[layer, 0, 2];
            _cube[layer, 0, 2] = _cube[layer, 2, 2]; // This is exactly like Right() corner swap, except instead of 2 in x it's 0
            _cube[layer, 2, 2] = _cube[layer, 2, 0]; // EXCEPT ITS INVERTED
            _cube[layer, 2, 0] = _cube[layer, 0, 0];
            _cube[layer, 0, 0] = temp;
        }
        else
        {

            // Swap edge cubes in array
            temp = _cube[layer, 1, 2];
            _cube[layer, 1, 2] = _cube[layer, 0, 1];
            _cube[layer, 0, 1] = _cube[layer, 1, 0];
            _cube[layer, 1, 0] = _cube[layer, 2, 1];
            _cube[layer, 2, 1] = temp;

            // Swap corner cubes
            temp = _cube[layer, 0, 2];
            _cube[layer, 0, 2] = _cube[layer, 0, 0];
            _cube[layer, 0, 0] = _cube[layer, 2, 0];
            _cube[layer, 2, 0] = _cube[layer, 2, 2];
            _cube[layer, 2, 2] = temp;
        }
    }

    private void RotateY(int layer, int turn)
    {
        // Handle graphics rotation
        for (int x = 0; x < 3; x++)
            for (int z = 0; z < 3; z++)
                if (layer == 1 && x == 1 && z == 1)
                    continue;
                else
                    _cube[x, z, layer].transform.Rotate(new Vector3(0, 90 * turn, 0), Space.World);

        GameObject temp;
        if (turn > 0)
        {
            //Swap edge cubes in array
            temp = _cube[1, 0, layer];
            _cube[1, 0, layer] = _cube[2, 1, layer];
            _cube[2, 1, layer] = _cube[1, 2, layer];
            _cube[1, 2, layer] = _cube[0, 1, layer];
            _cube[0, 1, layer] = temp;

            // Swap corner cubes
            temp = _cube[0, 0, layer];
            _cube[0, 0, layer] = _cube[2, 0, layer];
            _cube[2, 0, layer] = _cube[2, 2, layer];
            _cube[2, 2, layer] = _cube[0, 2, layer];
            _cube[0, 2, layer] = temp;
        }
        else
        {

            //Swap edge cubes in array
            temp = _cube[1, 0, layer];
            _cube[1, 0, layer] = _cube[0, 1, layer];
            _cube[0, 1, layer] = _cube[1, 2, layer];
            _cube[1, 2, layer] = _cube[2, 1, layer];
            _cube[2, 1, layer] = temp;

            temp = _cube[0, 0, layer];
            _cube[0, 0, layer] = _cube[0, 2, layer];
            _cube[0, 2, layer] = _cube[2, 2, layer];
            _cube[2, 2, layer] = _cube[2, 0, layer];
            _cube[2, 0, layer] = temp;
        }
    }

    private void RotateZ(int layer, int turn)
    {
        // Handle graphics rotation
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                if (layer == 1 && y == 1 && x == 1)
                    continue;
                else
                    _cube[x, layer, y].transform.Rotate(new Vector3(0, 0, -90 * turn), Space.World);

        GameObject temp;
        if (turn > 0)
        {
            // Swap edges
            temp = _cube[1, layer, 2];
            _cube[1, layer, 2] = _cube[0, layer, 1];
            _cube[0, layer, 1] = _cube[1, layer, 0];
            _cube[1, layer, 0] = _cube[2, layer, 1];
            _cube[2, layer, 1] = temp;

            // Swap corners
            temp = _cube[0, layer, 2];
            _cube[0, layer, 2] = _cube[0, layer, 0];
            _cube[0, layer, 0] = _cube[2, layer, 0];
            _cube[2, layer, 0] = _cube[2, layer, 2];
            _cube[2, layer, 2] = temp;
        }
        else
        {
            // Swap edges
            temp = _cube[1, layer, 2];
            _cube[1, layer, 2] = _cube[2, layer, 1];
            _cube[2, layer, 1] = _cube[1, layer, 0];
            _cube[1, layer, 0] = _cube[0, layer, 1];
            _cube[0, layer, 1] = temp;

            // Swap corners
            temp = _cube[0, layer, 2];
            _cube[0, layer, 2] = _cube[2, layer, 2];
            _cube[2, layer, 2] = _cube[2, layer, 0];
            _cube[2, layer, 0] = _cube[0, layer, 0];
            _cube[0, layer, 0] = temp;
        }
    }

    public void Scramble()
    {
        for (int i = 0; i < 20; i++)
        {
            // Picks a random side and direction to turn and will scramble the cube
            int randomSide = Random.Range(1, 6);
            int randomDirection = (Random.Range(0, 1) * 2) - 1;

            switch (randomSide / 2)
            {
                case 0:
                    RotateX((randomSide % 2) * 2, randomDirection);
                    break;
                case 1:
                    RotateY((randomSide % 2) * 2, randomDirection);
                    break;
                case 2:
                    RotateZ((randomSide % 2) * 2, randomDirection);
                    break;
                default:
                    Debug.LogError("Error in switch statement of Scramble() method");
                    break;
            }
        }
    }
}
