using System.Threading.Tasks;
using System.Windows;

namespace Sebs.Toolkit.Wpf.Animations
{
    public interface IAnimationService
    {
        /// <summary>
        /// A flag to indicate if this page should animate out on load. 
        /// Useful for when we are moving the page to another frame
        /// </summary>
        bool IsAnimatingOut { get; set; }

        public float SlideSeconds { get; }

        Task AnimateInAsync(FrameworkElement view);

        Task AnimateOutAsync(FrameworkElement view);
    }
}

