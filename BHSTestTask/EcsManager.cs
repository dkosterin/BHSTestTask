using BHSTestTask.Compoments;
using BHSTestTask.Systems;
using Leopotam.EcsLite;

namespace BHSTestTask
{
    class EcsManager
    {
        EcsWorld _world;
        EcsSystems _systems;
        public EcsManager(Scene scene)
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            var positionPool = _world.GetPool<PositionComponent>();
            var velocityPool = _world.GetPool<VelocityComponent>();
            var wallPool = _world.GetPool<WallComponent>();
            var collisionPool = _world.GetPool<ColliderComponent>();
            var sceneObjectPool = _world.GetPool<SceneObjectComponent>();
            foreach (var obj in scene.GetObjects())
            {
                if (obj is Ball ball)
                {
                    int entity = _world.NewEntity();
                    ref PositionComponent position = ref positionPool.Add(entity);
                    position.Value = ball.Center;
                    ref VelocityComponent velocity = ref velocityPool.Add(entity);
                    velocity.Value = ball.Velocity;
                    ref ColliderComponent collider = ref collisionPool.Add(entity);
                    collider.Radius = ball.Radius;
                    ref SceneObjectComponent sceneObj = ref sceneObjectPool.Add(entity);
                    sceneObj.Ref = ball;
                }
                else if (obj is Wall wall)
                {
                    int entity = _world.NewEntity();
                    ref WallComponent w = ref wallPool.Add(entity);
                    w.Begin = wall.Begin;
                    w.End = wall.End;
                    ref SceneObjectComponent sceneObj = ref sceneObjectPool.Add(entity);
                    sceneObj.Ref = wall;
                }
            }

            _systems.Add(new MovementSystem(0.2f)).Add(new CollisionSystem()).Add(new SceneUpdateSystem());
            _systems.Init();
        }
        public void RunSystem()
        {
            _systems?.Run();
        }
    }
}
