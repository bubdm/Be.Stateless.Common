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

using System.Diagnostics.CodeAnalysis;

namespace Be.Stateless.Dummies.Reflection
{
	[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class ReflectedDummy
	{
		public static string StaticFieldSpy => _staticField;

		private static string StaticProperty { get; set; }

		public static string StaticPropertySpy => StaticProperty;

		private static void StaticMethod(string value)
		{
			_staticField = value;
		}

		private ReflectedDummy() { }

		public string FieldSpy => _field;

		public string PropertySpy => Property;

		private string Property { get; set; }

		private void Method(string value)
		{
			_field = value;
		}

		private void Method(int value)
		{
			_field = value.ToString();
		}

		public static readonly ReflectedDummy Instance = new ReflectedDummy();

		[SuppressMessage("Style", "IDE0044:Add readonly modifier")]
		private static string _staticField = nameof(_staticField);

		[SuppressMessage("Style", "IDE0044:Add readonly modifier")]
		private string _field = nameof(_field);
	}
}
