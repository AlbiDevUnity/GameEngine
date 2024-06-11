namespace GameEngine.ECS.First
{
    public class Entity : IReadOnlyEntity
    {
        private static int s_Counter = 0;
        private static readonly Queue<int> s_FreeSlots = new Queue<int>();

        private List<IComponent> m_Components = new List<IComponent>();

        public int Id { get; }
        public IReadOnlyList<IComponent> Components => m_Components;

        public Entity()
        {
            if (s_FreeSlots.Count > 0)
            {
                Id = s_FreeSlots.Dequeue();
            }
            else
            {
                Id = s_Counter++;
            }
        }

        public bool HasComponent<TComponent>() where TComponent : IComponent
        {
            return m_Components.GetType() == typeof(TComponent);
        }

        public void AddComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            if(HasComponent<TComponent>()) { return; }
            m_Components.Add(component);
        }

        public void RemoveComponent<TComponent>() where TComponent : IComponent
        {
            if(!HasComponent<TComponent>()) { return; }
            m_Components.Remove(m_Components.First(c => c.GetType() == typeof(TComponent)));
        }

        public TComponent GetComponent<TComponent>() where TComponent: IComponent
        {
            if(!HasComponent<TComponent>()) { throw new Exception("Component not found!"); }
            return (TComponent)m_Components.First(c => c.GetType() == typeof(TComponent));
        }
    }
}
