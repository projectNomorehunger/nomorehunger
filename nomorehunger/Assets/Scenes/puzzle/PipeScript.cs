using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    int[] positions = { 1, 2, 3, 4 };

    public float[] correctRotation;
    public int[] correctPosition;

    
    [SerializeField]
    bool isPlaced = false;
    [SerializeField] int currentPipePosition = 0;
    int possibleRots = 1;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        possibleRots = correctPosition.Length;
        RandomizeRotation();
        CheckCorrectPlacement();

    }


    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        ChangePipePosition();
        
        if (possibleRots == 2) // straightPipe
        {
            if (currentPipePosition == correctPosition[0] || currentPipePosition == correctPosition[1])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if(currentPipePosition != correctPosition[0] || currentPipePosition != correctPosition[1])
            {
                if (isPlaced) gameManager.wrongMove();
                isPlaced = false;
                
            }
            else
            {
                Debug.Log("OnMouseDown straightPipe error");
            }
        }
        else if(possibleRots == 1) //curvePipe
        {
            if (currentPipePosition == correctPosition[0])
            {
                isPlaced = true;
                gameManager.correctMove();
                
            }
            else if (currentPipePosition != correctPosition[0])
            {
                if (isPlaced) gameManager.wrongMove();
                isPlaced = false;
                
            }
            else
            {
                Debug.Log("OnMouseDown curvePipe error");
            }
         
        }
        else
        {
            Debug.Log("ERROR");
        }


    }

    private void RandomizeRotation()
    {
        int rand = Random.Range(0, positions.Length);
        currentPipePosition = rand + 1;
        transform.rotation = Quaternion.Euler(0, 0, rotations[rand]);
    }

    private void CheckCorrectPlacement()
    {
        if (possibleRots == 2) // straightPipe
        {
            if ( currentPipePosition == correctPosition[0] || currentPipePosition == correctPosition[1])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if(currentPipePosition != correctPosition[0] || currentPipePosition != correctPosition[1])
            {
                if(isPlaced) gameManager.wrongMove();
                isPlaced = false;
            }
            else
            {
                Debug.Log("CheckCorrectPlacement straightPipe Error");
            }
        }
        else if(possibleRots == 1) // curvePipe
        {
            if (currentPipePosition == correctPosition[0])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (currentPipePosition != correctPosition[0])
            {
                if(isPlaced) gameManager.wrongMove();
                isPlaced = false;

            }
            else
            {
                Debug.Log("CheckCorrectPlacement curvePipe Error");
            }
        }
        else
        {
            Debug.Log("ERROR2");
        }
    }

    private void ChangePipePosition()
    {
        if(currentPipePosition == 1)
        {
            currentPipePosition = 2;
        }else if (currentPipePosition == 2)
        {
            currentPipePosition = 3;
        }else if(currentPipePosition == 3)
        {
            currentPipePosition = 4;
        }else if(currentPipePosition == 4)
        {
            currentPipePosition = 1;
        }
    }
    
}
