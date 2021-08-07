using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// A version of AsyncDelegate thats in One input defined as T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncDelegateT<T> : AsyncDelegate
    {
        /// <summary>
        /// Async delegate with no inputs.
        /// </summary>
        /// <returns></returns>
        public delegate Task Del1(T input);

        /// <summary>
        /// Runs a delegate as async, waiting for all appended tasks to complete.
        /// </summary>
        /// <param name="targetDelegate"></param>
        public async Task RunAsyncDelegate(Del1 targetDelegate, T input)
        {
            var delegateTasks = targetDelegate.GetInvocationList();
            foreach (Delegate del in delegateTasks)
            {
                Del1 task = (Del1)del;
                await task.Invoke(input);
            }
        }
    }
}
