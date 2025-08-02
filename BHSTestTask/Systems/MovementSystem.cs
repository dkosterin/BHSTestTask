using BHSTestTask.Compoments;
using Leopotam.EcsLite;
using System;

namespace BHSTestTask.Systems
{
    class MovementSystem : IEcsRunSystem
    {
        float _deltaTime;
        public MovementSystem(float deltaTime)
        {
            _deltaTime = deltaTime;
        }
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var positionPool = world.GetPool<PositionComponent>();
            var velocityPool = world.GetPool<VelocityComponent>();

            var filter = world.Filter<PositionComponent>().Inc<VelocityComponent>().End();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity);
                ref var velocity = ref velocityPool.Get(entity);

                position.Value += velocity.Value * _deltaTime;
                Console.WriteLine($"Ball on position: ({position.Value.X}, {position.Value.Y})");
            }
        }
    }
}
