using Sebs.Toolkit.Wpf.Animations.Core;
using Sebs.Toolkit.Wpf.Animations.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace Sebs.Toolkit.Wpf.Animations
{
    public class AnimationService : IAnimationService
    {
        public AnimationService(float slideSeconds)
        {
            SlideSeconds = slideSeconds;
        }

        public bool IsAnimatingOut { get; set; }
        public float SlideSeconds { get; }

        /// <summary>
        /// Animate the element IN the region.
        /// </summary>
        /// <param name="element"> Element can be a control, view or any kind of framework element</param>
        public async Task AnimateInAsync(FrameworkElement element)
        {
            await element.SlideAndFadeInAsync(CardinalDirectionsEnum.Right, false, SlideSeconds, size: (int)Application.Current.MainWindow.Width);
        }

        /// <summary>
        /// Animate the element OUT of the region.
        /// </summary>
        /// <param name="element"> Element can be a control, view or any kind of framework element</param>
        public async Task AnimateOutAsync(FrameworkElement element)
        {
            await element.SlideAndFadeOutAsync(CardinalDirectionsEnum.Left, SlideSeconds);
        }
    }
}
