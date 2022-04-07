// <copyright file="DependencyObjectExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Windows;
using System.Windows.Media;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Extensions;

/// <summary>
///     Extensions for <see cref="DependencyObject" />.
/// </summary>
[PublicAPI]
public static class DependencyObjectExtensions
{
#region Methods

#region Static methods

    /// <summary>
    ///     Get1sts the first level of level visual children.
    /// </summary>
    /// <typeparam name="T">The type of children to seek.</typeparam>
    /// <param name="parent">The parent.</param>
    /// <returns>A list of visual children.</returns>
    public static List<T> GetFirstLevelVisualChildren<T>(this DependencyObject parent)
        where T : Visual
    {
        var visualCollection = new List<T>();
        GetFirstLevelVisualChildren(
            parent,
            visualCollection);

        return visualCollection;
    }

    /// <summary>
    ///     Get1sts the first level of visual children in a specific list.
    /// </summary>
    /// <typeparam name="T">The type of children to seek.</typeparam>
    /// <param name="parent">The parent.</param>
    /// <param name="visualCollection">The visual collection.</param>
    /// <remarks>
    ///     <para>
    ///         This method is NOT thread-safe and should not be invoked when changes to the visual children lists are
    ///         expected.
    ///     </para>
    ///     <para>
    ///         It is relatively safe to invoke this method by dispatcher. Care must be taken to not invoke via non-exclusive
    ///         or non-owning dispatcher.
    ///     </para>
    /// </remarks>
    public static void GetFirstLevelVisualChildren<T>(
        this DependencyObject parent,
        List<T> visualCollection)
        where T : DependencyObject
    {
        var count = VisualTreeHelper.GetChildrenCount(parent);

        for (var i = 0; i < count; i++)
        {
            if (VisualTreeHelper.GetChild(
                    parent,
                    i) is T dependencyObject)
            {
                visualCollection.Add(dependencyObject);
            }
        }
    }

    /// <summary>
    ///     Gets the topmost visual parent of a specific type from the visual tree.
    /// </summary>
    /// <typeparam name="T">The type of the visual parent to get.</typeparam>
    /// <param name="childObject">The child object.</param>
    /// <returns>
    ///     A visual object of the specified type, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic)
    ///     if one does not exist.
    /// </returns>
    public static T GetTopmostVisualParent<T>(this DependencyObject childObject)
        where T : DependencyObject
    {
        if (childObject == null)
        {
            return null;
        }

        DependencyObject parent = VisualTreeHelper.GetParent(childObject);

        // Iteratively traverse the visual tree
        while (parent != null && parent is not T)
        {
            parent = VisualTreeHelper.GetParent(parent);
        }

        return parent as T;
    }

    /// <summary>
    ///     Gets a visual child of a certain type.
    /// </summary>
    /// <typeparam name="T">The type of the visual child to get.</typeparam>
    /// <param name="parent">The parent.</param>
    /// <returns>The visual child, if found, or <see langword="null" /> otherwise.</returns>
    public static T GetVisualChild<T>(this DependencyObject parent)
        where T : DependencyObject
    {
        if (parent == null)
        {
            return null;
        }

        var furtherDownTree = new List<DependencyObject>
        {
            parent
        };

        for (var q = 0; q < furtherDownTree.Count; q++)
        {
            DependencyObject localParent = furtherDownTree[q];
            var numVisuals = VisualTreeHelper.GetChildrenCount(localParent);
            for (var i = 0; i < numVisuals; i++)
            {
                // Traverse all visual tree and look for a child
                DependencyObject v = VisualTreeHelper.GetChild(
                    localParent,
                    i);

                if (v is T properChild)
                {
                    return properChild;
                }

                furtherDownTree.Add(v);
            }
        }

        return null;
    }

    /// <summary>
    ///     Gets the main window.
    /// </summary>
    /// <param name="current">The current dependency object.</param>
    /// <returns>The main window.</returns>
    public static Window GetWindow(this DependencyObject current)
    {
        if (current is Window window)
        {
            return window;
        }

        return GetTopmostVisualParent<Window>(current);
    }

    /// <summary>
    /// Gets the value of a dependency property as a specific type.
    /// </summary>
    /// <typeparam name="T">The type of value to get.</typeparam>
    /// <param name="dependencyObject">The dependency object that tries to get the value.</param>
    /// <param name="dependencyProperty">The dependency property to get the value of.</param>
    /// <returns>The value, if one exists, otherwise a default value.</returns>
    /// <exception cref="InvalidCastException">A value exists for that dependency property, but it cannot be cast to the desired type.</exception>
    public static T GetValue<T>(
        this DependencyObject dependencyObject,
        DependencyProperty dependencyProperty)
    {
        object rawValue = dependencyObject.GetValue(Requires.NotNull(dependencyProperty));

        if (rawValue is null)
        {
            return default;
        }

        if (rawValue is not T convertedValue)
        {
            throw new InvalidCastException();
        }

        return convertedValue;
    }

#endregion

#endregion
}