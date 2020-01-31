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
		#region Nested Type: IStub

		private interface IStub
		{
			string TargetEnvironment { get; }
		}

		#endregion

		#region Nested Type: Stub

		private class Stub : IStub
		{
			private Stub() { }

			#region IStub Members

			[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
			public string TargetEnvironment { get; private set; }

			#endregion

			[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
			public string Property { get; private set; }

			internal static readonly IStub Instance = new Stub();
		}

		#endregion

		[Fact]
		public void SetInterfaceReadOnlyProperty()
		{
			Reflector.SetProperty(Stub.Instance, "TargetEnvironment", "DEV");
			Stub.Instance.TargetEnvironment.Should().Be("DEV");
		}

		[Fact]
		[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
		public void SetInterfaceReadOnlyPropertyManually()
		{
			Stub.Instance.GetType().GetProperty("TargetEnvironment").SetValue(Stub.Instance, "DEV");
			Stub.Instance.TargetEnvironment.Should().Be("DEV");
		}

		[Fact]
		public void SetPrivateProperty()
		{
			Reflector.SetProperty(Stub.Instance, "Property", "DEV");
			((Stub) Stub.Instance).Property.Should().Be("DEV");
		}
	}
}
