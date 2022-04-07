// <copyright file="DependencyPropertyChangedEventArgsExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Reflection;
using System.Windows;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Extensions;

/// <summary>
/// Extensions for dependency property changed event arguments.
/// </summary>
[PublicAPI]
public static class DependencyPropertyChangedEventArgsExtensions
{
    /// <summary>
    /// Unhooks an event from an old value of a dependency property, if the value is not nullable, and hooks it to the new value.
    /// </summary>
    /// <typeparam name="TDependencyProperty">The type of the dependency property.</typeparam>
    /// <param name="e">The event arguments of the dependency property change event handler.</param>
    /// <param name="eventName">The name of the desired event.</param>
    /// <param name="eventHandler">An event handler of the proper type.</param>
    /// <exception cref="ArgumentInvalidTypeException"><paramref name="eventName"/> does not represent an event within the type <see cref="DependencyPropertyChangedEventArgs"/>, or
    /// <paramref name="eventHandler"/> cannot be assigned to handle such an event type.</exception>
    /// <exception cref="ArgumentInvalidTypeException"><paramref name="eventName"/> or <paramref name="eventHandler"/> are <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
    public static void HookUnhookEvent<TDependencyProperty>(
        this DependencyPropertyChangedEventArgs e,
        string eventName,
        Delegate eventHandler)
    {
        var eventInfo = typeof(TDependencyProperty).GetEvent(Requires.NotNull(eventName));

        if (eventInfo is null)
        {
            throw new ArgumentInvalidTypeException(
                nameof(eventName));
        }

        HookUnhookEvent<TDependencyProperty>(e, eventInfo, eventHandler);
    }

    /// <summary>
    /// Unhooks an event from an old value of a dependency property, if the value is not nullable, and hooks it to the new value.
    /// </summary>
    /// <typeparam name="TDependencyProperty">The type of the dependency property.</typeparam>
    /// <param name="e">The event arguments of the dependency property change event handler.</param>
    /// <param name="eventInfo">The <see cref="EventInfo"/> representing the desired event.</param>
    /// <param name="eventHandler">An event handler of the proper type.</param>
    /// <exception cref="ArgumentInvalidTypeException"><paramref name="eventInfo"/> does not represent an event within the type <see cref="DependencyPropertyChangedEventArgs"/>, or
    /// <paramref name="eventHandler"/> cannot be assigned to handle such an event type.</exception>
    /// <exception cref="ArgumentInvalidTypeException"><paramref name="eventInfo"/> or <paramref name="eventHandler"/> are <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
    public static void HookUnhookEvent<TDependencyProperty>(
        this DependencyPropertyChangedEventArgs e,
        EventInfo eventInfo,
        Delegate eventHandler)
    {
        var ei = Requires.NotNull(eventInfo);
        var delegateType = Requires.NotNull(eventHandler)
            .GetType();

        Requires.ArgumentOfType<TDependencyProperty>(ei.DeclaringType, nameof(eventInfo));

        if (!ei.EventHandlerType.IsAssignableFrom(delegateType))
        {
            throw new ArgumentInvalidTypeException(nameof(eventHandler));
        }

        if (e.OldValue is TDependencyProperty oldValue)
        {
            eventInfo.RemoveEventHandler(
                oldValue,
                eventHandler);
        }

        if (e.NewValue is TDependencyProperty newValue)
        {
            eventInfo.AddEventHandler(
                newValue,
                eventHandler);
        }
    }
}