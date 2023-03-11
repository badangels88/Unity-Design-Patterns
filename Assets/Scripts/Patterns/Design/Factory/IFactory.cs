using System;

namespace Patterns.Design.Factory
{
    /// <summary>
    /// Base interface for any factory
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Create an item from the factory
        /// </summary>
        /// <param name="item">Type of the item to create</param>
        /// <param name="paramters">Parameters to create the item</param>
        /// <returns></returns>
        IFactoryItem CreateItem(Type item, object[] paramters);

        /// <summary>
        /// Create an item from the factory
        /// </summary>
        /// <param name="parameters">Parameters to create the item</param>
        /// <returns>The created item</returns>
        IFactoryItem CreateItem<T>(object[] parameters) where T : IFactoryItem;
    }
}
