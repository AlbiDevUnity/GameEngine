namespace GameEngine.ECS.First
{
    public class EntityWorld
    {
        private readonly List<Entity> m_Entities = new List<Entity>();
        private readonly List<ISystem> m_Systems = new List<ISystem>();

        public void Add(Entity entity)
        {
            m_Entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            m_Entities.Remove(entity);
        }

        public void Add(ISystem system)
        {
            m_Systems.Add(system);
        }

        public void Remove(ISystem system)
        {
            m_Systems.Remove(system);
        }

        public void Process()
        {
            foreach(ISystem system in m_Systems)
            {

            }
        }
    }
}
