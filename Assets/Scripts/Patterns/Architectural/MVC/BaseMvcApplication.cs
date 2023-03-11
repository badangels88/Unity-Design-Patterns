using Patterns.Design.Singleton;
using Patterns.Architectural.MVC.Interface;

namespace Patterns.Architectural.MVC
{
    /// <inheritdoc cref="IApplication"/>
    /// <summary>
    /// Base class for any Unity Mvc application
    /// </summary>
    public class BaseMvcApplication<T> : Singleton<BaseMvcApplication<T>>, IApplication where T : BaseMvcApplication<T>
    {
        #region Properties
        public virtual IControllerFactory ControllerFactory { get; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public virtual bool DestroyController(IController controller)
            =>ControllerFactory.Controllers.Remove(controller);
        #endregion
    }
}
