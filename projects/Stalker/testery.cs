using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stalker {
	class testery {

		public enum DType {
			Disney,
			WarnerBros,
			JCStaff,
			KyoAni,
		}

		class Duck {
			public string Jump(bool clicked) {
				if (clicked) {
					MessageBox.Show("Clicked!");
				}
				Console.WriteLine("{0} Jump", m_name);
				return "Jump";
			}

			bool m_canQuack = true;
			public bool CanQuack {
				get {
					Console.WriteLine("get can quack {0}", m_canQuack);
					return m_canQuack;
				}
				set {
					Console.WriteLine("set can quack {0}", value);
					m_canQuack = value;
				}
			}

			string m_name = "donald";
			public string Name {
				get {
					Console.WriteLine("get name: {0}", m_name);
					return m_name;
				}
				set {
					Console.WriteLine("set name: {0}", value);
					m_name = value;
				}
			}

			int m_age = 3;
			public int Age {
				get {
					Console.WriteLine("get age: {0}", m_age);
					return m_age;
				}
				set {
					Console.WriteLine("set age: {0}", value);
					m_age = value;
				}
			}


			DType m_type = DType.Disney;
			public DType Type {
				get {
					Console.WriteLine("get type: {0}", m_type);
					return m_type;
				}
				set {
					Console.WriteLine("set type: {0}", value);
					m_type = value;
				}
			}
		}
	}
}
