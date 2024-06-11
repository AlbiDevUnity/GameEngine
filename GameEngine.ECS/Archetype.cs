namespace GameEngine.ECS
{
    public class Archetype
    {
        private static readonly Dictionary<Type[], Archetype> s_Archetypes = new Dictionary<Type[], Archetype>();

        //signature, used to sign entity
        private readonly Type[] m_Signature;

        public Type[] Types => m_Signature;

        public static Archetype From(params Type[] types)
        {
            if (!s_Archetypes.TryGetValue(types, out Archetype? value))
            {
                value = new Archetype(types);
                s_Archetypes.Add(types, value);
            }

            return value;
        }

        private Archetype(params Type[] types)
        {
            m_Signature = types;
        }

    }
}
