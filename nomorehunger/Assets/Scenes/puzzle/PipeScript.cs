using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;

    int possibleRots = 1;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        possibleRots = correctRotation.Length;
        RandomizeRotation();

        CheckCorrectPlacement();
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        Debug.Log(possibleRots);

        if (possibleRots > 1)
        {
            if ((Mathf.Approximately(transform.rotation.eulerAngles.z, correctRotation[0]) || Mathf.Approximately(transform.rotation.eulerAngles.z, correctRotation[1])) && !isPlaced)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
        else
        {
            if (Mathf.Approximately(transform.rotation.eulerAngles.z, correctRotation[0]) && !isPlaced)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
    }

    private void RandomizeRotation()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.rotation = Quaternion.Euler(0, 0, rotations[rand]);
    }

    private void CheckCorrectPlacement()
    {
        if (possibleRots > 1)
        {
            if (Mathf.Approximately(transform.rotation.eulerAngles.z, correctRotation[0]) || Mathf.Approximately(transform.rotation.eulerAngles.z, correctRotation[1]))
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else
            {
                isPlaced = false;
            }
        }
        else
        {
            if (Mathf.Approximately(transform.rotation.eulerAngles.z, correctRotation[0]))
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else
            {
                isPlaced = false;
            }
        }
    }
}
