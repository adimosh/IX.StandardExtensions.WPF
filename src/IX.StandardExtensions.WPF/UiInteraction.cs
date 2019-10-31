// <copyright file="UiInteraction.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF
{
    /// <summary>
    /// A static class providing some methods that deal with possible UI interaction wherever needed.
    /// </summary>
    [PublicAPI]
    public static class UiInteraction
    {
        /// <summary>
        /// Invokes an action synchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <param name="toInvoke">The method to invoke.</param>
        public static void UiSensibleInvoke(Action toInvoke)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                toInvoke();
            }
            else
            {
                dispatcher.Invoke(toInvoke);
            }
        }

        /// <summary>
        /// Invokes an action synchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="toInvoke">The method to invoke.</param>
        /// <returns>The result of the invocation.</returns>
        public static TResult UiSensibleInvoke<TResult>(Func<TResult> toInvoke)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                return toInvoke();
            }
            else
            {
                return dispatcher.Invoke(toInvoke);
            }
        }

        /// <summary>
        /// Invokes an action synchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <param name="toInvoke">The method to invoke.</param>
        /// <param name="priority">The priority to invoke with. If the invocation does not take place on the dispatcher, this parameter is ignored.</param>
        public static void UiSensibleInvoke(Action toInvoke, DispatcherPriority priority)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                toInvoke();
            }
            else
            {
                dispatcher.Invoke(toInvoke, priority);
            }
        }

        /// <summary>
        /// Invokes an action synchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="toInvoke">The method to invoke.</param>
        /// <param name="priority">The priority to invoke with. If the invocation does not take place on the dispatcher, this parameter is ignored.</param>
        /// <returns>The result of the invocation.</returns>
        public static TResult UiSensibleInvoke<TResult>(Func<TResult> toInvoke, DispatcherPriority priority)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                return toInvoke();
            }
            else
            {
                return dispatcher.Invoke(toInvoke, priority);
            }
        }

        /// <summary>
        /// Invokes an action asynchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <param name="toInvoke">The method to invoke.</param>
        /// <returns>The task representing the invocation operation.</returns>
        public static async Task UiSensibleAsyncInvoke(Action toInvoke)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                toInvoke();
            }
            else
            {
                await dispatcher.InvokeAsync(toInvoke);
            }
        }

        /// <summary>
        /// Invokes an action asynchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="toInvoke">The method to invoke.</param>
        /// <returns>The result of the invocation.</returns>
        public static async Task<TResult> UiSensibleAsyncInvoke<TResult>(Func<TResult> toInvoke)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                return toInvoke();
            }

            return await dispatcher.InvokeAsync(toInvoke);
        }

        /// <summary>
        /// Invokes an action asynchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <param name="toInvoke">The method to invoke.</param>
        /// <param name="priority">The priority to invoke with. If the invocation does not take place on the dispatcher, this parameter is ignored.</param>
        /// <returns>The task representing the invocation operation.</returns>
        public static async Task UiSensibleAsyncInvoke(Action toInvoke, DispatcherPriority priority)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                toInvoke();
            }
            else
            {
                await dispatcher.InvokeAsync(toInvoke, priority);
            }
        }

        /// <summary>
        /// Invokes an action asynchronously on a dispatcher, if one exists, and on the current thread if one does not exist, or we are already on the UI thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="toInvoke">The method to invoke.</param>
        /// <param name="priority">The priority to invoke with. If the invocation does not take place on the dispatcher, this parameter is ignored.</param>
        /// <returns>The result of the invocation.</returns>
        public static async Task<TResult> UiSensibleAsyncInvoke<TResult>(Func<TResult> toInvoke, DispatcherPriority priority)
        {
            var dispatcher = global::System.Windows.Application.Current?.Dispatcher;

            if (dispatcher?.CheckAccess() ?? true)
            {
                return toInvoke();
            }

            return await dispatcher.InvokeAsync(toInvoke, priority);
        }
    }
}