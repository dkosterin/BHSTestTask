using BHSTestTask.Compoments;
using Leopotam.EcsLite;

namespace BHSTestTask.Systems
{
    class SceneUpdateSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var positionPool = world.GetPool<PositionComponent>();
            var velocityPool = world.GetPool<VelocityComponent>();
            var sceneObjectPool = world.GetPool<SceneObjectComponent>();

            var filter = world.Filter<SceneObjectComponent>().End();

            foreach (var entity in filter)
            {
                ref var sceneRef = ref sceneObjectPool.Get(entity);
                if (sceneRef.Ref is Ball ball)
                {
                    ref var position = ref positionPool.Get(entity);
                    ref var velocity = ref velocityPool.Get(entity);
                    ball.Center = position.Value;
                    ball.Velocity = velocity.Value;
                }
            }
        }
    }
}
