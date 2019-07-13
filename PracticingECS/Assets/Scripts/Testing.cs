﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class Testing : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    private void Start(){
        EntityManager entityManager = World.Active.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof (LevelComponent),
             typeof (Translation),
             typeof (RenderMesh),
             typeof (LocalToWorld)
             );
             NativeArray<Entity> entityArray = new NativeArray<Entity>(100,Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++){
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new LevelComponent{level = Random.Range(0, 2)});

            entityManager.SetSharedComponentData(entity, new RenderMesh{
                mesh = mesh,
                material = material
            });
        }
        entityArray.Dispose();
    }
}
