// <copyright file="BoolVisibilityConverter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.ValueConverters;

/// <summary>
///     A value converter between <see cref="bool" /> and <see cref="Visibility" />.
/// </summary>
/// <seealso cref="ValueConverterBase" />
[PublicAPI]
public class BoolVisibilityConverter : ValueConverterBase
{
#region Methods

    /// <summary>
    ///     Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use. Should be of type <see cref="BoolVisibilityFilter"/>.</param>
    /// <param name="culture">The culture to use in the converter. This parameter is ignored.</param>
    /// <returns>
    ///     A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is
    ///     used.
    /// </returns>
    /// <exception cref="IX.StandardExtensions.ArgumentInvalidTypeException">
    ///     <paramref name="value" /> is not
    ///     <see cref="bool" /> or Nullable&lt;<see cref="bool" />&gt;.
    /// </exception>
    [SuppressMessage(
        "Performance",
        "HAA0601:Value type to reference type conversion causing boxing allocation",
        Justification = "Unavoidable in a WPF value converter.")]
    public override object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        var result = value switch
        {
            bool b => b,
            null => false,
            _ => throw new ArgumentInvalidTypeException(nameof(value))
        };

        BoolVisibilityFilter filter;

        if (parameter is BoolVisibilityFilter visibilityFilter)
        {
            filter = visibilityFilter;
        }
        else
        {
            filter = BoolVisibilityFilter.Collapsed;
        }

        return result ? Visibility.Visible :
            filter == BoolVisibilityFilter.Hidden ? Visibility.Hidden : Visibility.Collapsed;
    }

    /// <summary>
    ///     Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    ///     A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is
    ///     used.
    /// </returns>
    /// <exception cref="IX.StandardExtensions.ArgumentInvalidTypeException">
    ///     <paramref name="value" /> is not
    ///     <see cref="Visibility" /> or Nullable&lt;<see cref="Visibility" />&gt;.
    /// </exception>
    [SuppressMessage(
        "Performance",
        "HAA0601:Value type to reference type conversion causing boxing allocation",
        Justification = "Unavoidable in a WPF value converter.")]
    public override object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        Visibility result = value switch
        {
            Visibility visibility => visibility,
            null => Visibility.Collapsed,
            _ => throw new ArgumentInvalidTypeException(nameof(value))
        };

        BoolVisibilityFilter filter;

        if (parameter is BoolVisibilityFilter visibilityFilter)
        {
            filter = visibilityFilter;
        }
        else
        {
            filter = BoolVisibilityFilter.Collapsed;
        }

        return filter == BoolVisibilityFilter.Hidden
            ? result is Visibility.Visible or Visibility.Collapsed
            : result == Visibility.Visible;
    }

#endregion
}