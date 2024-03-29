// <copyright file="BindingLocalizer.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Globalization;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.WPF.ValueConverters;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Localization;

/// <summary>
///     A localizer converter.
/// </summary>
/// <seealso cref="ValueConverterBase" />
[PublicAPI]
public class BindingLocalizer : ValueConverterBase
{
#region Methods

    /// <summary>
    ///     Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    ///     A converted value. If the method returns <see langword="null" />, the valid <see langword="null" /> value is
    ///     used.
    /// </returns>
    public override object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        var key = Requires.ArgumentOfType<string>(parameter);

        if (this.IsInDesignMode)
        {
            return parameter;
        }

        var reader = Requires.ArgumentOfType<BindingAssemblyResourceReader>(value);

        return reader?.GetLocalizedResource(key) ?? parameter;
    }

#endregion
}