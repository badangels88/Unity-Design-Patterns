using System;
using System.Collections.Generic;
using Patterns.Design.Factory;
using Patterns.Architectural.MVC.Interface;

namespace Patterns.Architectural.MVC
{
    /// <inheritdoc cref="AbstractFactory"/>
    /// <inheritdoc cref="IControllerFactory"/>
    /// <summary>
    /// Abstract controller factory for unity controllers
    /// </summary>
    /// <typeparam name="T">Type of the controller factory params</typeparam>
    public abstract class AbstractControllerFactory<T> : BaseFactory, IControllerFactory where T : IControllerFactoryParams
    {
        public List<IController> Controllers { get; private set; }

        protected AbstractControllerFactory()
        {
            Controllers = new List<IController>();
        }

        /// <summary>
        /// Create a controller from the parameters specified through the template parameter
        /// </summary>
        /// <param name="controllerType">The type of the controller to create</param>
        /// <param name="parameters">Parameters to create the controller</param>
        /// <returns></returns>
        protected abstract IController CreateController(Type controllerType, T parameters);

        /// <summary>
        /// Create a controller from the parameters specified through the template parameter
        /// </summary>
        /// <typeparam name="TController">The type of the controller to create</typeparam>
        /// <param name="parameters">Parameters to create the controller</param>
        /// <returns></returns>
        protected abstract TController CreateController<TController>(T parameters) where TController : IController;

        public IController CreateController(Type controllerType, IControllerFactoryParams parameters)
        {
            IController controller = CreateController(controllerType, (T)parameters);
            Controllers.Add(controller);
            return controller;
        }

        public TController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
            => (TController)CreateController(typeof(TController), parameters);
    }
}
