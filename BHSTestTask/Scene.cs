using System.Collections.Generic;

namespace BHSTestTask
{
    class Scene
    {
        List<SceneObject> objects;
        public Scene()
        {
            objects = new List<SceneObject>();
        }
        public void AddObject(SceneObject obj)
        {
            objects.Add(obj);
        }
        public List<SceneObject> GetObjects()
        {
            return objects;
        }
    }
}
