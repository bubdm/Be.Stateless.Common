#region Copyright & License

// Copyright © 2012 - 2021 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Be.Stateless.Reflection
{
	[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "Public API.")]
	[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
	public static class Reflector
	{
		#region Get Field

		public static object GetField<T>(string fieldName)
		{
			return GetField(typeof(T), null, fieldName, STATIC_BINDING_FLAGS);
		}

		public static object GetField(Type type, string fieldName)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			return GetField(type, null, fieldName, STATIC_BINDING_FLAGS);
		}

		public static object GetField<T>(T instance, string fieldName)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			return GetField(typeof(T), instance, fieldName, INSTANCE_BINDING_FLAGS);
		}

		public static bool TryGetField<T>(string fieldName, out object value)
		{
			return TryGetField(typeof(T), null, fieldName, STATIC_BINDING_FLAGS, out value);
		}

		public static bool TryGetField(Type type, string fieldName, out object value)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			return TryGetField(type, null, fieldName, STATIC_BINDING_FLAGS, out value);
		}

		public static bool TryGetField<T>(T instance, string fieldName, out object value)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			return TryGetField(typeof(T), instance, fieldName, INSTANCE_BINDING_FLAGS, out value);
		}

		private static object GetField(IReflect type, object instance, string fieldName, BindingFlags flags)
		{
			if (TryGetField(type, instance, fieldName, flags, out var value)) return value;
			throw new ArgumentException($"Target type '{type}' does not contain a definition for field '{fieldName}'.");
		}

		private static bool TryGetField(IReflect type, object instance, string fieldName, BindingFlags flags, out object value)
		{
			var field = type.GetField(fieldName, flags);
			if (field == null && instance != null) field = instance.GetType().GetField(fieldName, flags);
			if (field == null)
			{
				value = default;
				return false;
			}
			value = field.GetValue(instance ?? type);
			return true;
		}

		#endregion

		#region Set Field

		public static void SetField<T>(string fieldName, object value)
		{
			SetField(typeof(T), null, fieldName, value, STATIC_BINDING_FLAGS);
		}

		public static void SetField(Type type, string fieldName, object value)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			SetField(type, null, fieldName, value, STATIC_BINDING_FLAGS);
		}

		public static void SetField<T>(T instance, string fieldName, object value)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			SetField(typeof(T), instance, fieldName, value, INSTANCE_BINDING_FLAGS);
		}

		private static void SetField(IReflect type, object instance, string fieldName, object value, BindingFlags flags)
		{
			var field = type.GetField(fieldName, flags);
			if (field == null && instance != null) field = instance.GetType().GetField(fieldName, flags);
			if (field == null) throw new ArgumentException($"Target type '{type}' does not contain a definition for field '{fieldName}'.");
			field.SetValue(instance ?? type, value);
		}

		#endregion

		#region Get Property

		public static object GetProperty<T>(string propertyName)
		{
			return GetProperty(typeof(T), null, propertyName, STATIC_BINDING_FLAGS);
		}

		public static object GetProperty(Type type, string propertyName)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			return GetProperty(type, null, propertyName, STATIC_BINDING_FLAGS);
		}

		public static object GetProperty<T>(T instance, string propertyName)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			return GetProperty(typeof(T), instance, propertyName, INSTANCE_BINDING_FLAGS);
		}

		public static bool TryGetProperty<T>(string propertyName, out object value)
		{
			return TryGetProperty(typeof(T), null, propertyName, STATIC_BINDING_FLAGS, out value);
		}

		public static bool TryGetProperty(Type type, string propertyName, out object value)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			return TryGetProperty(type, null, propertyName, STATIC_BINDING_FLAGS, out value);
		}

		public static bool TryGetProperty<T>(T instance, string propertyName, out object value)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			return TryGetProperty(typeof(T), instance, propertyName, INSTANCE_BINDING_FLAGS, out value);
		}

		private static object GetProperty(IReflect type, object instance, string propertyName, BindingFlags flags)
		{
			if (TryGetProperty(type, instance, propertyName, flags, out var value)) return value;
			throw new ArgumentException($"Target type '{type}' does not contain a definition for property '{propertyName}'.");
		}

		private static bool TryGetProperty(IReflect type, object instance, string propertyName, BindingFlags flags, out object value)
		{
			var property = type.GetProperty(propertyName, flags);
			if (property == null && instance != null) property = instance.GetType().GetProperty(propertyName, flags);
			if (property == null)
			{
				value = default;
				return false;
			}
			value = property.GetValue(instance ?? type, null);
			return true;
		}

		#endregion

		#region Set Property

		public static void SetProperty<T>(string propertyName, object value)
		{
			SetProperty(typeof(T), null, propertyName, value, STATIC_BINDING_FLAGS);
		}

		public static void SetProperty(Type type, string propertyName, object value)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			SetProperty(type, null, propertyName, value, STATIC_BINDING_FLAGS);
		}

		public static void SetProperty<T>(T instance, string propertyName, object value)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			SetProperty(typeof(T).IsInterface ? instance.GetType() : typeof(T), instance, propertyName, value, INSTANCE_BINDING_FLAGS);
		}

		private static void SetProperty(IReflect type, object instance, string propertyName, object value, BindingFlags flags)
		{
			var property = type.GetProperty(propertyName, flags);
			if (property == null && instance != null) property = instance.GetType().GetProperty(propertyName, flags);
			if (property == null) throw new ArgumentException($"Target type '{type}' does not contain a definition for property '{propertyName}'.");
			property.SetValue(instance ?? type, value, null);
		}

		#endregion

		#region Invoke Method

		public static object InvokeMethod<T>(string methodName, params object[] @params)
		{
			return InvokeMethod(typeof(T), (object) null, methodName, @params, STATIC_BINDING_FLAGS);
		}

		public static object InvokeMethod(Type type, string methodName, params object[] @params)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			return InvokeMethod(type, (object) null, methodName, @params, STATIC_BINDING_FLAGS);
		}

		public static object InvokeMethod<T>(T instance, string methodName, params object[] @params)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			return InvokeMethod(typeof(T), instance, methodName, @params, INSTANCE_BINDING_FLAGS);
		}

		public static bool TryInvokeMethod<T>(string methodName, object[] @params, out object result)
		{
			return TryInvokeMethod(typeof(T), null, methodName, @params, STATIC_BINDING_FLAGS, out result);
		}

		public static bool TryInvokeMethod(Type type, string methodName, object[] @params, out object result)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			return TryInvokeMethod(type, null, methodName, @params, STATIC_BINDING_FLAGS, out result);
		}

		public static bool TryInvokeMethod<T>(T instance, string methodName, object[] @params, out object result)
		{
			if (Equals(instance, default(T))) throw new ArgumentNullException(nameof(instance));
			return TryInvokeMethod(typeof(T), instance, methodName, @params, INSTANCE_BINDING_FLAGS, out result);
		}

		private static object InvokeMethod(IReflect type, object instance, string methodName, object[] @params, BindingFlags flags)
		{
			if (TryInvokeMethod(type, instance, methodName, @params, flags, out var result)) return result;
			throw new ArgumentException($"Target type '{type}' does not contain a definition for method '{methodName}'.");
		}

		private static bool TryInvokeMethod(IReflect type, object instance, string methodName, object[] @params, BindingFlags flags, out object result)
		{
			try
			{
				var method = type.GetMethod(methodName, flags);
				if (method == null && instance != null) method = instance.GetType().GetMethod(methodName, flags);
				if (method == null)
				{
					result = default;
					return false;
				}
				result = method.Invoke(instance, @params);
				return true;
			}
			catch (AmbiguousMatchException)
			{
				result = type.InvokeMember(methodName, flags | BindingFlags.InvokeMethod, null, instance, @params, null, null, null);
				return true;
			}
		}

		#endregion

		#region Binding Flags

		private const BindingFlags COMMON_BINDING_FLAGS = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public;
		private const BindingFlags INSTANCE_BINDING_FLAGS = BindingFlags.Instance | COMMON_BINDING_FLAGS;
		private const BindingFlags STATIC_BINDING_FLAGS = BindingFlags.Static | COMMON_BINDING_FLAGS;

		#endregion
	}
}
