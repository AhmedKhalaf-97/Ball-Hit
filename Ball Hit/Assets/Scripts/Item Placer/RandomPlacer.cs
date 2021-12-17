using UnityEngine;

public class RandomPlacer : PipeItemGenerator
{

    public PipeItem[] itemPrefabs;
    float[] itemPipeRotation;
    float[] itemAngleStep;

    public override void GenerateItems(Pipe pipe)
    {
        itemPipeRotation = new float[100];
        itemAngleStep = new float[100];

        int gemsCount = Random.Range(1, pipe.CurveSegmentCount - 2);

        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;

        for (int i = 0; i < pipe.CurveSegmentCount; i++)
        {
            PipeItem item = Instantiate<PipeItem>(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
            float pipeRotation = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) * 360f / pipe.pipeSegmentCount;
            float _angleStep = i * angleStep;

            itemPipeRotation[i] = pipeRotation;
            itemAngleStep[i] = _angleStep;

            item.Position(pipe, _angleStep, pipeRotation);
        }

        //gem generator
        if (Random.Range(0, 100) < SceneSwitcher.gemGenerationProbability)
        {
            for (int i = 0; i < gemsCount; i++)
            {
                GameObject gemGameobject = Instantiate<GameObject>(pipe.gemsPrefab);
                PipeItem gemItem = gemGameobject.GetComponent<PipeItem>();
                float pipeRotation = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) * 360f / pipe.pipeSegmentCount;
                float _angleStep = i * angleStep;

                //check for interception with obstacles
                for (int x = 0; x < itemPipeRotation.Length; x++)
                {
                    if (pipeRotation == itemPipeRotation[x] && _angleStep == itemAngleStep[x])
                    {
                        pipeRotation = pipeRotation + 90;
                    }
                }

                gemItem.Position(pipe, _angleStep, pipeRotation);
            }
        }
    }
}