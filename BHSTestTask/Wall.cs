using System.Numerics;

namespace BHSTestTask
{
    class Wall : SceneObject
    {
        Vector2 _begin;
        Vector2 _end;
        public Wall(Vector2 begin, Vector2 end)
        {
            _begin = begin;
            _end = end;
        }
        public Vector2 Begin
        {
            get { return _begin; }
        }
        public Vector2 End
        {
            get { return _end; }
        }
    }
}
