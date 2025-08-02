using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float ballSize = 0.75f;
    private int NumberBallInRow;

    public GameObject BallPool;
    public Vector3 SpawnPosition;
    GameObject[,] ListBall;
    int indexRow;
    public static BallSpawn Spawn;
    private bool[,] visited;

    private void Awake()
    {
        Spawn = this;
    }
    void Start()
    {
        float height, width;
        GetScreenSize(out height,out width);

        NumberBallInRow = Mathf.FloorToInt(width / ballSize /2);
        //  SpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        SpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane + 10));
        SpawnPosition.z = 0;
        SpawnPosition.x += ((ballSize / 2 + (width / 2 / 2)));
        int num = Mathf.FloorToInt(height / ballSize);
        ListBall = new GameObject[1000, NumberBallInRow];
        visited = new bool[1000, NumberBallInRow];
        ListBallSameColor = new List<GameObject>();
        SpawnGid(num);

        
        StartCoroutine(LoopEach5Second());
       // StartCoroutine(Checkloop());


    }
    void GetScreenSize(out float height,out float width)
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Screen.width / Screen.height;
    }

    void SpawnRow()
    {
        Vector3 SpawnPosition = this.SpawnPosition;
       if(indexRow > 0  )
        {
            SpawnPosition.y = ListBall[indexRow - 1, 0].transform.position.y + ballSize;
        }
        else
        {
            SpawnPosition.y += ballSize;
        }
        if (indexRow % 2 == 0)
        {
            for (int i = 0; i < NumberBallInRow; i++)
            {
                GameObject ballnew = BallController.controller.GetRamDomnBall();
                ballnew.transform.position = SpawnPosition;
                ListBall[indexRow, i] = ballnew;
                SpawnPosition.x += ballSize;
                ballnew.transform.parent = BallPool.transform;
            }
        }
        else
        {
            SpawnPosition.x += ballSize / 2;
            for (int i = 0; i < NumberBallInRow - 1; i++)
            {
                GameObject ballnew = BallController.controller.GetRamDomnBall();
                ballnew.transform.position = SpawnPosition;
                ListBall[indexRow, i] = ballnew;
                SpawnPosition.x += ballSize;
                ballnew.transform.parent = BallPool.transform;
            }
        }
        indexRow++;
    }

    void SpawnGid(int numberRow)
    {
        for (int j = 0; j < numberRow; j++) {

            Vector3 SpawnPosition = this.SpawnPosition;
            if (indexRow > 0)
            {
                SpawnPosition.y = ListBall[indexRow - 1, 0].transform.position.y + ballSize;
            }
            else
            {
                SpawnPosition.y += ballSize;
            }
            if (indexRow % 2 == 0)
            {
                for (int i = 0; i < NumberBallInRow; i++)
                {
                    GameObject ballnew = new GameObject();
                    ballnew.transform.position = SpawnPosition;
                    ListBall[indexRow, i] = ballnew;
                    SpawnPosition.x += ballSize;
                    ballnew.transform.parent = BallPool.transform;
                }
            }
            else
            {
                SpawnPosition.x += ballSize / 2;
                for (int i = 0; i < NumberBallInRow - 1; i++)
                {
                    GameObject ballnew = new GameObject();
                    ballnew.transform.position = SpawnPosition;
                    ListBall[indexRow, i] = ballnew;
                    SpawnPosition.x += ballSize;
                    ballnew.transform.parent = BallPool.transform;
                }
            }
            indexRow++;

        }
        Vector3 pos = BallPool.transform.position;
        pos.y -= ballSize * (numberRow - 1);
        BallPool.transform.position = pos;
    }

    void GetIndex(GameObject ball, out int row, out int col)
    {

        row = 0; col = 0;
        for(int i = 0; i< ListBall.GetLength(0); i++)
        {

            for(int j = 0;j < ListBall.GetLength(1); j++)
            {
                if (ListBall[i,j] != ball)
                {
                    continue;
                }
                row = i;
                col = j;
                return;
            }
        }

    }

    public void SoftBall(GameObject BallisShot, GameObject BallisPool)
    {
        int row, col;
        GetIndex(BallisPool,out row,out col);
        if(col == 0)
        {
            col = 1;
        }
        if(row == 0)
        {
            row = 1;
        }

        for(int i = row -1; i <= row + 1; i++)
        {
            for(int j = col -1; j <= col + 1; j++)
            {
                if (i < 0 || j < 0 || i >= ListBall.GetLength(0) || j >= ListBall.GetLength(1))
                    continue;

                // Ki?m tra null
                if (ListBall[i, j] == null)
                    continue;

                float distances = Vector3.Distance(BallisShot.transform.position, ListBall[i, j].transform.position);
                if (distances < 0.5 && !IsBall(ListBall[i,j]) )
                {
                    BallisShot.transform.position = ListBall[i, j].transform.position;
                    Destroy(ListBall[i, j]);
                    ListBall[i,j] = BallisShot;

                    ListBallSameColor.Add(CountBallSameColor(i, j, BallisShot.tag));

                    DesTroyBall();
                    return;

                }
            }
        }

    }
    IEnumerator LoopEach5Second()
    {
        while (true)
        {
            SpawnRow();
            yield return new WaitForSeconds(5);
        }
    }

    public List<GameObject> ListBallSameColor;
    GameObject CountBallSameColor(int row, int col, string color)
    {
        if(!isValidPosition(row, col) || !isSameColor(row, col,color) || isVisited(row, col))
        {
            return null;
        }
        MarkAsVisited(row, col);
        if(row % 2 ==1)
        {
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row + 1, col, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row + 1, col + 1, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row, col + 1, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row, col- 1, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row - 1, col, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row - 1, col + 1, color));

        }else
        {
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row + 1, col - 1, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row + 1, col  , color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row, col + 1, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row, col - 1, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row - 1, col - 1, color));
            AddIfNotNull(ListBallSameColor, CountBallSameColor(row - 1, col  , color));
        }
        return ListBall[row, col];  
    }
    void AddIfNotNull(List<GameObject> list, GameObject gameO)
    {
        if(gameO != null)
        {
            list.Add(gameO);
        }
    }

    void MarkAsVisited(int row, int col)
    {
        visited[row, col] = true;
    }
    bool isVisited(int row, int col)
    {
        return visited[row, col];
    }
    bool isSameColor(int row, int col , string color)
    {
        bool isSame;
        try
        {
            isSame = ListBall[row, col].tag == color;
        }catch(Exception e)
        {
            return false;
        }
        return isSame;
    }
    bool isValidPosition(int row, int col)
    {
        bool isValid;
        if (row % 2 == 0)
        {
            isValid = row >= 0 && row <= 1000 && col >= 0 && col < NumberBallInRow;
        }
        else
        {
            isValid = row >= 0 && row <= 1000 && col >= 0 && col < (NumberBallInRow - 1);
        }
        return isValid;
    }

    bool IsBall(GameObject Go)
    {
        if(Go.tag == "Untagged")
        {
            return false;
        }
        return true;
    }
    void DesTroyBall()
    {
        if (ListBallSameColor.Count <= 2)
        {
            ListBallSameColor = new();
        }
        else
        {
            foreach(var item in ListBallSameColor)
            {
                GameObject newGame = new();
                newGame.transform.position = item.transform.position;
                int row, col;
                GetIndex(item, out row, out col);
                ListBall[row, col] = newGame;
                Destroy(item.gameObject);
                GameController.controller.GetScoreText();
            }
        }
        ListBallSameColor = new();
        visited = new bool[1000, NumberBallInRow];
    }


    IEnumerator Checkloop()
    {
        while(true)
        {
            isAllowCheck =false;
            ListBallCurrent = new List<GameObject>();
            CheckBallAnchor(indexRow - 1, 0);
            isAllowCheck = true;

            yield return new WaitForSeconds(0.2f);
            
        }
    }



    public List<GameObject> ListBallCurrent;
    public bool isAllowCheck;
    void CheckBallAnchor(int row, int col)
    {
        if (!isValidPosition(row, col))
        {

            return;
        }
        GameObject a = ListBall[row, col];
        if(a == null || !IsBall (a) || ListBallCurrent.Contains(a))
        {
            return;
        }
        ListBallCurrent.Add(a);
        if(row % 2== 0)
        {
            CheckBallAnchor(row + 1, col - 1);
            CheckBallAnchor(row + 1, col  );
            CheckBallAnchor(row  , col + 1);
            CheckBallAnchor(row + 1, col - 1);
            CheckBallAnchor(row  , col - 1);
            CheckBallAnchor(row - 1, col - 1);

        }else
        {
            CheckBallAnchor(row + 1, col + 1);
            CheckBallAnchor(row + 1, col);
            CheckBallAnchor(row, col + 1);
            CheckBallAnchor(row  , col - 1);
            CheckBallAnchor(row -1, col );
            CheckBallAnchor(row - 1, col + 1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
