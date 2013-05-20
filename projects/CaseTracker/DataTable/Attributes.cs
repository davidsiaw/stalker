using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stalker {

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
	public class AutoFillColumnAttribute : Attribute {

	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
	public class MinimumWidthAttribute : Attribute {
		public readonly int Size;
		public MinimumWidthAttribute(int size) {
			Size = size;
		}
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
	public class InitialWidthAttribute : Attribute {
		public readonly int Size;
		public InitialWidthAttribute(int size) {
			Size = size;
		}
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
	public class AutoSizeColumnAttribute : Attribute {

	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
	public class FixedSizeColumnAttribute : Attribute {
		public readonly int Size;
		public FixedSizeColumnAttribute(int size) {
			Size = size;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class RowColorAttribute : Attribute {

	}
}
