// <copyright file="NegateBoolConverter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.ValueConverters
{
    /// <summary>
    ///     A value converter that negates a <see cref="bool" />.
    /// </summary>
    /// <seealso cref="ValueConverterBase" />
    [PublicAPI]
    public class NegateBoolConverter : ValueConverterBase
    {
#region Methods

        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property. This parameter is ignored.</param>
        /// <param name="parameter">The converter parameter to use. This parameter is ignored.</param>
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
            var convertedValue = value switch
            {
                bool b => b,
                null => false,
                _ => throw new ArgumentInvalidTypeException(nameof(value))
            };

            return !convertedValue;
        }

        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to. This parameter is ignored.</param>
        /// <param name="parameter">The converter parameter to use. This parameter is ignored.</param>
        /// <param name="culture">The culture to use in the converter. This parameter is ignored.</param>
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
            var convertedValue = value switch
            {
                bool b => b,
                null => true,
                _ => throw new ArgumentInvalidTypeException(nameof(value))
            };

            return !convertedValue;
        }

#endregion
    }
}