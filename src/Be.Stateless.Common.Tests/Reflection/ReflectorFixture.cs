#region Copyright & License

// Copyright © 2012 - 2020 François Chabot
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
using FluentAssertions;
using Xunit;

namespace Be.Stateless.Reflection
{
	public class ReflectorFixture
	{
		[Fact]
		public void GetInstanceField()
		{
			Reflector.GetField(Stub.Instance, "_field").Should().Be(Stub.Instance.FieldSpy);
		}

		[Fact]
		public void GetInstanceProperty()
		{
			Reflector.GetProperty(Stub.Instance, "Property").Should().Be(Stub.Instance.PropertySpy);
		}

		[Fact]
		public void GetStaticFieldThroughGeneric()
		{
			Reflector.GetField<Stub>("_staticField").Should().Be(Stub.StaticFieldSpy);
		}

		[Fact]
		public void GetStaticFieldThroughType()
		{
			Reflector.GetField(typeof(Stub), "_staticField").Should().Be(Stub.StaticFieldSpy);
		}

		[Fact]
		public void GetStaticPropertyThroughGeneric()
		{
			Reflector.GetProperty<Stub>("StaticProperty").Should().Be(Stub.StaticPropertySpy);
		}

		[Fact]
		public void GetStaticPropertyThroughType()
		{
			Reflector.GetProperty(typeof(Stub), "StaticProperty").Should().Be(Stub.StaticPropertySpy);
		}

		[Fact]
		public void InvokeInstanceMethod()
		{
			Reflector.InvokeMethod(Stub.Instance, "Method", nameof(InvokeInstanceMethod));
			Stub.Instance.FieldSpy.Should().Be(nameof(InvokeInstanceMethod));
		}

		[Fact]
		public void InvokeStaticMethodThroughGeneric()
		{
			Reflector.InvokeMethod<Stub>("StaticMethod", nameof(InvokeStaticMethodThroughGeneric));
			Stub.StaticFieldSpy.Should().Be(nameof(InvokeStaticMethodThroughGeneric));
		}

		[Fact]
		public void InvokeStaticMethodThroughType()
		{
			Reflector.InvokeMethod(typeof(Stub), "StaticMethod", nameof(InvokeStaticMethodThroughType));
			Stub.StaticFieldSpy.Should().Be(nameof(InvokeStaticMethodThroughType));
		}

		[Fact]
		public void SetInstanceField()
		{
			Reflector.SetField(Stub.Instance, "_field", nameof(SetInstanceField));
			Stub.Instance.FieldSpy.Should().Be(nameof(SetInstanceField));
		}

		[Fact]
		public void SetInstanceProperty()
		{
			Reflector.SetProperty(Stub.Instance, "Property", nameof(SetInstanceProperty));
			Stub.Instance.PropertySpy.Should().Be(nameof(SetInstanceProperty));
		}

		[Fact]
		public void SetStaticFieldThroughGeneric()
		{
			Reflector.SetField<Stub>("_staticField", nameof(SetStaticFieldThroughGeneric));
			Stub.StaticFieldSpy.Should().Be(nameof(SetStaticFieldThroughGeneric));
		}

		[Fact]
		public void SetStaticFieldThroughType()
		{
			Reflector.SetField(typeof(Stub), "_staticField", nameof(SetStaticFieldThroughType));
			Stub.StaticFieldSpy.Should().Be(nameof(SetStaticFieldThroughType));
		}

		[Fact]
		public void SetStaticPropertyThroughGeneric()
		{
			Reflector.SetProperty<Stub>("StaticProperty", nameof(SetStaticPropertyThroughGeneric));
			Stub.StaticPropertySpy.Should().Be(nameof(SetStaticPropertyThroughGeneric));
		}

		[Fact]
		public void SetStaticPropertyThroughType()
		{
			Reflector.SetProperty(typeof(Stub), "StaticProperty", nameof(SetStaticPropertyThroughType));
			Stub.StaticPropertySpy.Should().Be(nameof(SetStaticPropertyThroughType));
		}

		[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
		[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
		[SuppressMessage("ReSharper", "UnusedMember.Local")]
		private class Stub
		{
			public static string StaticFieldSpy => _staticField;

			private static string StaticProperty { get; set; }

			public static string StaticPropertySpy => StaticProperty;

			private static void StaticMethod(string value)
			{
				_staticField = value;
			}

			private Stub() { }

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

			public static readonly Stub Instance = new Stub();

			[SuppressMessage("Style", "IDE0044:Add readonly modifier")]
			private static string _staticField = nameof(_staticField);

			[SuppressMessage("Style", "IDE0044:Add readonly modifier")]
			private string _field = nameof(_field);
		}
	}
}
