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

#if NETFRAMEWORK

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using Be.Stateless.Security.Principal;

namespace Be.Stateless
{
	[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
	public static class Delegate
	{
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static void InvokeAs(string username, string password, Action action)
		{
			using (new WindowsIdentity(username, password).Impersonate())
			{
				action?.Invoke();
			}
		}
	}
}

#endif
