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

using Be.Stateless.Dummies.Reflection;
using FluentAssertions;
using Xunit;

namespace Be.Stateless.Reflection
{
	public class ReflectorFixture
	{
		[Fact]
		public void GetGenericStaticPropertyThruDerivedType()
		{
			Reflector.GetProperty(typeof(ReflectedDerivedGenericDummy), "Instance").Should().NotBeNull();
		}

		[Fact]
		public void GetInstanceField()
		{
			Reflector.GetField(ReflectedDummy.Instance, "_field").Should().Be(ReflectedDummy.Instance.FieldSpy);
		}

		[Fact]
		public void GetInstanceProperty()
		{
			Reflector.GetProperty(ReflectedDummy.Instance, "Property").Should().Be(ReflectedDummy.Instance.PropertySpy);
		}

		[Fact]
		public void GetStaticFieldThroughGeneric()
		{
			Reflector.GetField<ReflectedDummy>("_staticField").Should().Be(ReflectedDummy.StaticFieldSpy);
		}

		[Fact]
		public void GetStaticFieldThroughType()
		{
			Reflector.GetField(typeof(ReflectedDummy), "_staticField").Should().Be(ReflectedDummy.StaticFieldSpy);
		}

		[Fact]
		public void GetStaticPropertyThroughGeneric()
		{
			Reflector.GetProperty<ReflectedDummy>("StaticProperty").Should().Be(ReflectedDummy.StaticPropertySpy);
		}

		[Fact]
		public void GetStaticPropertyThroughType()
		{
			Reflector.GetProperty(typeof(ReflectedDummy), "StaticProperty").Should().Be(ReflectedDummy.StaticPropertySpy);
		}

		[Fact]
		public void InvokeInstanceMethod()
		{
			Reflector.InvokeMethod(ReflectedDummy.Instance, "Method", nameof(InvokeInstanceMethod));
			ReflectedDummy.Instance.FieldSpy.Should().Be(nameof(InvokeInstanceMethod));
		}

		[Fact]
		public void InvokeStaticMethodThroughGeneric()
		{
			Reflector.InvokeMethod<ReflectedDummy>("StaticMethod", nameof(InvokeStaticMethodThroughGeneric));
			ReflectedDummy.StaticFieldSpy.Should().Be(nameof(InvokeStaticMethodThroughGeneric));
		}

		[Fact]
		public void InvokeStaticMethodThroughType()
		{
			Reflector.InvokeMethod(typeof(ReflectedDummy), "StaticMethod", nameof(InvokeStaticMethodThroughType));
			ReflectedDummy.StaticFieldSpy.Should().Be(nameof(InvokeStaticMethodThroughType));
		}

		[Fact]
		public void SetInstanceField()
		{
			Reflector.SetField(ReflectedDummy.Instance, "_field", nameof(SetInstanceField));
			ReflectedDummy.Instance.FieldSpy.Should().Be(nameof(SetInstanceField));
		}

		[Fact]
		public void SetInstanceProperty()
		{
			Reflector.SetProperty(ReflectedDummy.Instance, "Property", nameof(SetInstanceProperty));
			ReflectedDummy.Instance.PropertySpy.Should().Be(nameof(SetInstanceProperty));
		}

		[Fact]
		public void SetStaticFieldThroughGeneric()
		{
			Reflector.SetField<ReflectedDummy>("_staticField", nameof(SetStaticFieldThroughGeneric));
			ReflectedDummy.StaticFieldSpy.Should().Be(nameof(SetStaticFieldThroughGeneric));
		}

		[Fact]
		public void SetStaticFieldThroughType()
		{
			Reflector.SetField(typeof(ReflectedDummy), "_staticField", nameof(SetStaticFieldThroughType));
			ReflectedDummy.StaticFieldSpy.Should().Be(nameof(SetStaticFieldThroughType));
		}

		[Fact]
		public void SetStaticPropertyThroughGeneric()
		{
			Reflector.SetProperty<ReflectedDummy>("StaticProperty", nameof(SetStaticPropertyThroughGeneric));
			ReflectedDummy.StaticPropertySpy.Should().Be(nameof(SetStaticPropertyThroughGeneric));
		}

		[Fact]
		public void SetStaticPropertyThroughType()
		{
			Reflector.SetProperty(typeof(ReflectedDummy), "StaticProperty", nameof(SetStaticPropertyThroughType));
			ReflectedDummy.StaticPropertySpy.Should().Be(nameof(SetStaticPropertyThroughType));
		}
	}
}
