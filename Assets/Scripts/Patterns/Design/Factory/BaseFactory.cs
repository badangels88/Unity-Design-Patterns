using System;

namespace Patterns.Design.Factory
{
    /// <inheritdoc cref="IFactory"/>
    /// <summary>
    /// Base factory for unity factories
    /// </summary>
    public class BaseFactory : IFactory
    {
        public virtual IFactoryItem CreateItem(Type type, object[] parameters)
        {
            IFactoryItem item = (IFactoryItem)Activator.CreateInstance(type, parameters);
            return item;
        }

        public virtual IFactoryItem CreateItem<T>(object[] parameters) where T : IFactoryItem
            => CreateItem(typeof(T), parameters);
    }
}
