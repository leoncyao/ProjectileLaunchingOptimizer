using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : EvolutionaryAlgorithm{
    Camera mainCamera;
    float screenSize;
    Vector2 center;
    float centerX, centerY;
    public List<float> angles;

    public static Main instance;

    public GameObject ballPrefab;
    public Vector2 startPos;

    private void setCamera(float screenSize)
    {
        // Setting world fields
        centerX = screenSize / 2;
        centerY = screenSize / 2;
        center = new Vector2(centerX, centerY);


        // Setting camera
        Vector3 cameraPos = new Vector3(centerX, centerX, -screenSize / 2);
        mainCamera = Camera.main;
        mainCamera.transform.position = cameraPos;
        mainCamera.orthographic = true;
        mainCamera.orthographicSize = screenSize / 2 + 1;
        mainCamera.backgroundColor = Color.white;
    }
    void Start () {
        instance = this;
        numSamples = 50;
        // samples here are the angles at which the projectile is launched at
        oldSamples = new List<List<float>>();
        foreach(float angle in makeRandomVals2(0, Mathf.PI / 2, numSamples))
        {
            List<float> genes = new List<float>();
            genes.Add(angle);
            oldSamples.Add(genes);
        }
        newSamples = new List<List<float>>();

        // Setting world fields
        screenSize = 20f;
        centerX = screenSize / 2;
        centerY = screenSize / 2;
        center = new Vector2(centerX, centerY);
        startPos = new Vector2(0, centerY);

        setCamera(screenSize);

        GameObject ground1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground1.transform.position = center + Vector2.right*3;
        ground1.transform.localScale = new Vector3(screenSize, 0.1f, 1);

        GameObject ground2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground2.transform.position = center + Vector2.down * 1;
        ground2.transform.localScale = new Vector3(screenSize, 0.1f, 1);


        spawnBalls(numSamples);
	}

    void spawnBalls(int numBlocks)
    {
        for (int i = 0; i < numBlocks; i++)
        {
            Instantiate(ballPrefab, new Vector2(0, centerY), Quaternion.identity);
        }
    }

    public override void Update()
    {
        base.Update();
        // For indivdual Tests
        if (finishedTest)
        {
            finishedTest = false;
            cleanUpTest();
            startNewTest();
        }
    }

    //public override void Update () {
    //    if (Block.results1.Count == numItems)
    //    {            
    //        List<float> sortedResults1 = new List<float>(Block.results1);
    //        sortedResults1.Sort();
    //        List<float> BestHalfAngles = new List<float>();
    //        for (int k = sortedResults1.Count/2; k < sortedResults1.Count; k++)
    //        {
    //            BestHalfAngles.Add(Block.results2[Block.results1.IndexOf(sortedResults1[k])]);
    //        }
    //        List<float> children = makeChildren(BestHalfAngles);
    //        angles = new List<float>();

    //        for (int i = 0; i < numItems; i++)
    //        {
    //            angles.Add(children[children.Count - (i + 1)]);
    //        }
    //        float bestSubject = BestHalfAngles[BestHalfAngles.Count-1];
    //        print("best subject is " + bestSubject * Mathf.Rad2Deg);

    //        //float lowestAngle = BestHalfAngles.Min();
    //        //float greatestAngle = BestHalfAngles.Max();
    //        //print(string.Format("lowest Angle is {0} highest Angle is {1}", lowestAngle * Mathf.Rad2Deg, greatestAngle * Mathf.Rad2Deg));
    //        //angles = makeRandomVals2(lowestAngle, greatestAngle, numItems);


    //        Block.results1 = new List<float>();
    //        Block.results2 = new List<float>();

    //        spawnBlocks();

    //        // to show the best (for y = 0)
    //        //GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //        //temp.GetComponent<Collider>().enabled = false;
    //        //temp.transform.position = startPos;
    //        //temp.AddComponent<Rigidbody>();
    //        //temp.GetComponent<Rigidbody>().velocity = new Vector2(10 * Mathf.Cos(Mathf.PI/4), 10 * Mathf.Sin(Mathf.PI / 4)); new Vector2();

    //    }

    //}
    protected override void cleanUpTrial()
    {
    }

    protected override void cleanUpTest()
    {
    }

    protected override void startNewTest()
    {

    }

    protected override void startNewTrial()
    {
        Ball.i = 0;
        spawnBalls(numSamples);
        oldSamples = new List<List<float>>(newSamples);
        newSamples = new List<List<float>>();
    }

	void miscellaneous()
    {
        // For a cool looking fountain
        //if (Block.results.Count >= numItems) { 
        //    spawnBlocks();
        //    Block.results = new List<float>();
        //}
    }

    public static void listPrinter<T>(List<T> A)
    {
        string word = "";
        foreach (T temp in A)
        {
            word += temp + " ";
        }
        print(word);
    }
    public static void listPrinter(List<float> A, float b)
    {
        string word = "";
        foreach (float temp in A)
        {
            word += (b * temp) + " ";
        }
        print(word);
    }
    public List<float> makeRandomVals1(int a, int b)
    {
        // First Generate a list a...b
        float[] values = new float[b - a];
        for (int i = a; i < b - a; i++)
        {
            values[i] = i;
        }
        List<float> randomValues = new List<float>(values);

        // Next, swap the values randomly 
        System.Random random = new System.Random();
        for (int i = a; i < b - a; i++)
        {
            if (random.Next(0, 2) == 1)
            {
                int choice = random.Next(a, b);
                float temp = randomValues[i];
                randomValues[i] = randomValues[choice];
                randomValues[choice] = temp;
            }
        }
        return randomValues;
    }
    public List<float> makeRandomVals2(float a, float b, int n)
    {

        List<float> randomValues = new List<float>();

        // Next, swap the values randomly 
        System.Random random = new System.Random();
        for (int i = 0; i < n; i++) 
        {
            randomValues.Add((float)random.NextDouble() * (b-a) + a);
        }
        return randomValues;
    }

}
