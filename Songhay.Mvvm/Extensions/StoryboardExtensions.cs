using System;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="System.Windows.Media.Animation.Storyboard"/>.
    /// </summary>
    public static class StoryboardExtensions
    {
        /// <summary>
        /// Wraps a <see cref="System.Windows.Media.Animation.Storyboard"/>
        /// in a <see cref="System.Threading.Tasks.Task"/>.
        /// </summary>
        /// <param name="storyBoard">The story board.</param>
        /// <remarks>
        /// Based on code from “Async’ing Your Way to a Successful App with .NET”
        /// by Stephen Toub [http://channel9.msdn.com/Events/Build/2013/3-301]
        /// </remarks>
        public static async Task AsTask(this Storyboard storyBoard)
        {
            var tcs = new TaskCompletionSource<bool>();
            EventHandler handler = delegate { tcs.SetResult(true); };
            storyBoard.Completed += handler;
            storyBoard.Begin();
            await tcs.Task;
            storyBoard.Completed -= handler;
        }
    }
}
