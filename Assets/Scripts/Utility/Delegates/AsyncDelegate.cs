using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Class that helps running async delegates
    /// </summary>
    public class AsyncDelegate
    {
        /// <summary>
        /// Async delegate with no inputs.
        /// </summary>
        /// <returns></returns>
        public delegate Task Del();

        /// <summary>
        /// Runs a delegate as async, waiting for all appended tasks to complete.
        /// </summary>
        /// <param name="targetDelegate"></param>
        public async Task RunAsyncDelegate(Del targetDelegate)
        {
            var delegateTasks = targetDelegate.GetInvocationList();
            foreach (Delegate del in delegateTasks)
            {
                Del task = (Del)del;
                await task.Invoke();
            }
        }
    }
}
