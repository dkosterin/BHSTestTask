using Leopotam.EcsLite;
using BHSTestTask.Compoments;
using System;
using System.Numerics;

namespace BHSTestTask.Systems
{
    class CollisionSystem : IEcsRunSystem
    {
        Vector2 GetClosestPointOnLine(Vector2 begin, Vector2 end, Vector2 point)
        {
            var dirVector = end - begin;
            var t = Vector2.Dot(point - begin, dirVector) / dirVector.LengthSquared();
            t = Math.Clamp(t, 0f, 1f);
            return begin + dirVector * t;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var positionPool = world.GetPool<PositionComponent>();
            var velocityPool = world.GetPool<VelocityComponent>();
            var collisionPool = world.GetPool<ColliderComponent>();

            var wallPool = world.GetPool<WallComponent>();
            var ballFilter = world.Filter<PositionComponent>().Inc<ColliderComponent>().End();
            var wallFilter = world.Filter<WallComponent>().End();

            foreach (var ballEntity in ballFilter)
            {
                ref var position = ref positionPool.Get(ballEntity);
                ref var velocity = ref velocityPool.Get(ballEntity);
                ref var collision = ref collisionPool.Get(ballEntity);

                foreach (var wallEntity in wallFilter)
                {
                    ref var wall = ref wallPool.Get(wallEntity);

                    var closestPoint = GetClosestPointOnLine(wall.Begin, wall.End, position.Value);
                    var distVector = position.Value - closestPoint;
                    var length = distVector.Length();

                    if (length <= collision.Radius)
                    {
                        var dirVector = wall.End - wall.Begin;
                        var normal = new Vector2(-dirVector.Y, dirVector.X);
                        normal = Vector2.Normalize(normal);
                        velocity.Value -= 2 * Vector2.Dot(velocity.Value, normal) * normal;
                        position.Value += normal * (collision.Radius - length);
                        Console.WriteLine($"Collision with entity {wallEntity}");
                        break;
                    }
                }
            }
        }
    }
}
