using System.Threading.Tasks;
using UnityEngine;

namespace Utility
{

    public class AsyncOpHelper
    {
        /// <summary>
        /// Runs an async operation and checks it's done every inputted tick.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public async Task<bool> CompleteAsyncOp(AsyncOperation op, int tick)
        {
            //Wait for the operation to be done
            while (!op.isDone)
            {
                await Task.Delay(tick);
            }
            return true;
        }
    }
}
