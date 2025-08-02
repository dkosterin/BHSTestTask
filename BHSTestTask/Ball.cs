using System.Numerics;

namespace BHSTestTask
{
    class Ball : SceneObject
    {
        Vector2 _center;
        Vector2 _velocity;
        float _radius;
        public Ball(Vector2 center, Vector2 velocity, float radius)
        {
            _center = center;
            _velocity = velocity;
            _radius = radius;
        }
        public Vector2 Center
        {
            get { return _center; }
            set { _center = value; }
        }
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
    }
}
