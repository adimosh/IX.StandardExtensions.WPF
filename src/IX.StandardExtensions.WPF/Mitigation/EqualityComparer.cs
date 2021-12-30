// <copyright file="EqualityComparer.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Efficiency;
using IX.StandardExtensions.Extensions;

namespace IX.StandardExtensions.WPF.Mitigation
{
    internal class EqualityComparer : IEqualityComparer
    {
        private static readonly ConcurrentDictionary<Type, EqualityComparer> CachedComparers = new();

        private readonly object defaultComparer;
        private readonly MethodInfo comparerMethod;

        private EqualityComparer(Type type)
        {
            Type comparerType = typeof(EqualityComparer<>).MakeGenericType(Requires.NotNull(type));

            var comparer = comparerType.GetProperty(
                    "Default",
                    BindingFlags.Public | BindingFlags.Static)
                ?.GetValue(null);

            this.defaultComparer = comparer ??
                                   throw new InvalidOperationException(
                                       "The type's default comparer could not be created.");

            this.comparerMethod = comparerType.GetMethodWithExactParameters(
                                      "Equals",
                                      type,
                                      type) ??
                                  throw new InvalidOperationException(
                                      "The type's default comparer method could not be located.");
        }

        /// <summary>
        /// Gets a non-generic comparer for the given type.
        /// </summary>
        /// <param name="type">The type to get the comparer for.</param>
        /// <returns>The non-generic default equality comparer.</returns>
        public static EqualityComparer ForType(Type type) =>
            CachedComparers.GetOrAdd(
                type,
                (t) => new EqualityComparer(t));

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other.</exception>
        public new bool Equals(
            object x,
            object y) =>
            (bool)this.comparerMethod.Invoke(
                this.defaultComparer,
                new[]
                {
                    x,
                    y
                });

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.</exception>
        public int GetHashCode(object obj) =>
            Requires.NotNull(obj)
                .GetHashCode();
    }
}