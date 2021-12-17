﻿using UnityEngine;

public class SpiralPlacer : PipeItemGenerator
{

    public PipeItem[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float start = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f);
        float direction = Random.value < 0.5f ? 1f : -1f;

        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;

        for (int i = 0; i < pipe.CurveSegmentCount; i++)
        {
            PipeItem item = Instantiate<PipeItem>(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
            float pipeRotation = (start + i * direction) * 360f / pipe.pipeSegmentCount;
            item.Position(pipe, i * angleStep, pipeRotation);
        }

        //gem generator
        if (Random.Range(0, 100) < SceneSwitcher.gemGenerationProbability)
        {
            for (int i = 1; i < pipe.CurveSegmentCount - 2; i++)
            {
                GameObject gemGameobject = Instantiate<GameObject>(pipe.gemsPrefab);
                PipeItem gemItem = gemGameobject.GetComponent<PipeItem>();
                float pipeRotation = (start + i * -direction) * 360f / pipe.pipeSegmentCount;
                gemItem.Position(pipe, i * angleStep, pipeRotation);
            }
        }
    }
}