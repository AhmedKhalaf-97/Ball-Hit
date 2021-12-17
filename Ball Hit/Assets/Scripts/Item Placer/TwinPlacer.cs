using UnityEngine;

public class TwinPlacer : PipeItemGenerator
{

    public PipeItem[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        int itemInCircle = 2;
        int numOfCircles = Random.Range(2, 4);

        float angleStep = pipe.CurveAngle / numOfCircles;
        float direction = Random.value < 0.5f ? 1f : -1f;

        float gemsAngleStep = pipe.CurveAngle / pipe.CurveSegmentCount;

        for (int a = 0; a < numOfCircles; a++)
        {
            //float direction = Random.Range(-3, 3);

            for (int i = 0; i < itemInCircle; i++)
            {
                PipeItem item = Instantiate<PipeItem>(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);

                float theta = (2 * Mathf.PI / itemInCircle) * i;
                float xPos = Mathf.Sin(theta) * direction;
                float pipeRotation = xPos + i * (360 / itemInCircle);

                item.Position(pipe, a * angleStep, pipeRotation);
            }
        }

        //gem generator
        if (Random.Range(0, 100) < SceneSwitcher.gemGenerationProbability)
        {
            for (int i = 0; i < pipe.CurveSegmentCount; i++)
            {
                if (i % 3 == 0)
                {
                    GameObject gemGameobject = Instantiate<GameObject>(pipe.gemsPrefab);
                    PipeItem gemItem = gemGameobject.GetComponent<PipeItem>();
                    float pipeRotation = (0 + i * direction) * 1f / pipe.pipeSegmentCount;
                    gemItem.Position(pipe, i * gemsAngleStep, pipeRotation + 90);
                }
            }
        }
    }
}
