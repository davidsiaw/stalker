using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Stalker {
	public static class Utilities {

		public static string ToReadableString(this TimeSpan span) {
			string formatted = string.Format("{0}{1}{2}{3}",
				span.Days > 0 ? string.Format("{0:0} days, ", span.Days) : string.Empty,
				span.Hours > 0 ? string.Format("{0:0} hours, ", span.Hours) : string.Empty,
				span.Minutes > 0 ? string.Format("{0:0} minutes, ", span.Minutes) : string.Empty,
				span.Seconds > 0 ? string.Format("{0:0} seconds", span.Seconds) : string.Empty);

			if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

			return formatted;
		}

		// Display all public fields in data grid view
		public static void DisplayInDataGridView<T>(IEnumerable<T> coll, DataGridView dgv) {
			dgv.Rows.Clear();
			dgv.Columns.Clear();

			FieldInfo[] fis = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);

			foreach (FieldInfo fi in fis) {
				dgv.Columns.Add(fi.Name, fi.Name);
			}

			foreach (T item in coll) {
				object[] parms = new object[fis.Length];
				for (int i = 0; i < fis.Length; i++) {
					parms[i] = fis[i].GetValue(item);
				}
				dgv.Rows.Add(parms);
			}
		}

		interface IDynamicController {
			Action<DataGridViewRow, DataGridViewColumn> Changed { get; }
			Action<DataGridViewRow, DataGridViewColumn> Update { get; }
			Action<DataGridViewRow, DataGridViewColumn> Clicked { get; }
		}

		class ColumnSpecificRoutine<T> : IDynamicController {

			public ColumnSpecificRoutine(
				Action<DataGridViewRow, DataGridViewColumn> changed,
				Action<DataGridViewRow, DataGridViewColumn> update,
				Action<DataGridViewRow, DataGridViewColumn> clicked,
				T context) {
				this.Changed = changed;
				this.Update = update;
				this.Clicked = clicked;
				this.Context = context;
			}

			public Action<DataGridViewRow, DataGridViewColumn> Changed { get; private set; }
			public Action<DataGridViewRow, DataGridViewColumn> Update { get; private set; }
			public Action<DataGridViewRow, DataGridViewColumn> Clicked { get; private set; }
			public readonly T Context;
		}

		class CellHandlers {
			public CellHandlers(DataGridViewCellEventHandler clicked,
								DataGridViewCellEventHandler changed,
								DataGridViewCellCancelEventHandler editStart,
								DataGridViewCellEventHandler editEnd,
								DataGridViewEditingControlShowingEventHandler showEvent
				) {
				this.Clicked = clicked;
				this.Changed = changed;
				this.EditStart = editStart;
				this.EditEnd = editEnd;
				this.ShowEvent = showEvent;
			}
			public readonly DataGridViewCellEventHandler Clicked;
			public readonly DataGridViewCellEventHandler Changed;
			public readonly DataGridViewCellCancelEventHandler EditStart;
			public readonly DataGridViewCellEventHandler EditEnd;
			public readonly DataGridViewEditingControlShowingEventHandler ShowEvent;
		}


		// Create interactive data grid view that enables a GUI representation of the class
		public static void InterfaceToDataGridView<T>(IEnumerable<T> coll, DataGridView dgv) {
			dgv.Rows.Clear();
			dgv.Columns.Clear();

			RemoveHandlers(dgv);

			PropertyInfo[] pis = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			MethodInfo[] mis = typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public);

			foreach (PropertyInfo pi in pis) {

				if (!pi.CanRead) {
					continue;
				}

				DataGridViewColumn column;


				if (pi.PropertyType == typeof(bool)) {
					DataGridViewCheckBoxColumn dgvcbc = new DataGridViewCheckBoxColumn();
					dgvcbc.TrueValue = true;
					dgvcbc.FalseValue = false;
					dgvcbc.Tag = new ColumnSpecificRoutine<PropertyInfo>(
						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewCheckBoxCell;
							csp.Context.SetValue(r.Tag,
								cell.Value.Equals(cell.TrueValue)
								, null);
						},

						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewCheckBoxCell;
							bool val = (bool)csp.Context.GetValue(r.Tag, null);
							cell.Value = val ? cell.TrueValue : cell.FalseValue;
						},
						(r, c) => { },

						pi
						);
					column = dgvcbc;

				} else if (pi.PropertyType.IsEnum) {
					DataGridViewComboBoxColumn dgvcbc = new DataGridViewComboBoxColumn();

					Array vals = Enum.GetValues(pi.PropertyType);
					object[] e = new object[vals.Length];
					for (int i = 0; i < vals.Length; i++) {
						e[i] = vals.GetValue(i);
					}

					dgvcbc.Items.AddRange(e);
					dgvcbc.ValueType = pi.PropertyType;
					dgvcbc.Tag = new ColumnSpecificRoutine<PropertyInfo>(
						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewComboBoxCell;
							csp.Context.SetValue(r.Tag, cell.Value, null);
						},

						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewComboBoxCell;
							cell.Value = csp.Context.GetValue(r.Tag, null);
						},
						(r, c) => { },

						pi
						);
					column = dgvcbc;

				} else if (pi.PropertyType == typeof(int)) {
					DataGridViewTextBoxColumn dgvcbc = new DataGridViewTextBoxColumn();
					dgvcbc.Tag = new ColumnSpecificRoutine<PropertyInfo>(
						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewTextBoxCell;
							csp.Context.SetValue(r.Tag, int.Parse(cell.Value.ToString()), null);
						},

						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewTextBoxCell;
							cell.Value = csp.Context.GetValue(r.Tag, null).ToString();
						},
						(r, c) => { },

						pi
						);
					column = dgvcbc;


				} else if (pi.PropertyType == typeof(string)) {

					DataGridViewTextBoxColumn dgvcbc = new DataGridViewTextBoxColumn();
					dgvcbc.Tag = new ColumnSpecificRoutine<PropertyInfo>(
						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewTextBoxCell;
							csp.Context.SetValue(r.Tag, cell.Value.ToString(), null);
						},

						(r, c) => {
							var csp = c.Tag as ColumnSpecificRoutine<PropertyInfo>;
							var cell = r.Cells[c.Name] as DataGridViewTextBoxCell;
							cell.Value = csp.Context.GetValue(r.Tag, null);
						},
						(r, c) => { },

						pi
						);
					column = dgvcbc;

				} else {
					continue;
				}

				if (!pi.CanWrite) {
					column.ReadOnly = true;
				}

				column.Name = pi.Name;
				column.HeaderText = pi.Name;

				SetUpColumnWithAttributes(pi, column);

				dgv.Columns.Add(column);
			}

			foreach (MethodInfo mi in mis) {
				if (mi.GetParameters().Length != 1 || mi.ReturnType != typeof(string)) {
					continue;
				}

				if (mi.GetParameters()[0].ParameterType != typeof(bool)) {
					continue;
				}

				DataGridViewButtonColumn dgvbc = new DataGridViewButtonColumn();
				dgvbc.Tag = new ColumnSpecificRoutine<MethodInfo>(
					(r, c) => { },
					(r, c) => {
						var csp = c.Tag as ColumnSpecificRoutine<MethodInfo>;
						string a = csp.Context.Invoke(r.Tag, new object[] { false }) as string;
						var cell = r.Cells[c.Name] as DataGridViewButtonCell;
						cell.Value = a;
					},
					(r, c) => {
						var csp = c.Tag as ColumnSpecificRoutine<MethodInfo>;
						string a = csp.Context.Invoke(r.Tag, new object[] { true }) as string;
						var cell = r.Cells[c.Name] as DataGridViewButtonCell;
						cell.Value = a;
					},
					mi);

				dgvbc.Name = mi.Name;
				dgvbc.HeaderText = mi.Name;

				SetUpColumnWithAttributes(mi, dgvbc);

				dgv.Columns.Add(dgvbc);
			}
			InsertCollection<T>(coll, dgv);
			UpdateDataGrid(dgv, GetRowColorer<T>());

			ApplyHandlers(dgv);
		}

		private static void SetUpColumnWithAttributes(MemberInfo pi, DataGridViewColumn column) {
			if (pi.GetCustomAttributes(typeof(InitialWidthAttribute), false).Length != 0) {
				column.Width = GetAttr<InitialWidthAttribute>(pi).Size;
			}

			if (pi.GetCustomAttributes(typeof(AutoSizeColumnAttribute), false).Length != 0) {
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			} else if (pi.GetCustomAttributes(typeof(AutoFillColumnAttribute), false).Length != 0) {
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			} else if (pi.GetCustomAttributes(typeof(FixedSizeColumnAttribute), false).Length != 0) {
				column.Width = GetAttr<FixedSizeColumnAttribute>(pi).Size;
			}

			if (pi.GetCustomAttributes(typeof(MinimumWidthAttribute), false).Length != 0) {
				column.MinimumWidth = GetAttr<MinimumWidthAttribute>(pi).Size;
			}
		}

		private static T GetAttr<T>(MemberInfo pi) where T : Attribute {
			return ((T)pi.GetCustomAttributes(typeof(T), false)[0]);
		}

		private static void ApplyHandlers(DataGridView dgv) {
			CellHandlers handlers = new CellHandlers(

							//Clicked
							(o, d) => {
								DataGridView datagridview = o as DataGridView;
								if (d.ColumnIndex < 0 || d.RowIndex < 0) {
									return;
								}
								DataGridViewCell cell = datagridview[d.ColumnIndex, d.RowIndex];
								if (cell == null) {
									return;
								}
								IDynamicController controller = cell.OwningColumn.Tag as IDynamicController;
								controller.Clicked(cell.OwningRow, cell.OwningColumn);
								ForceDataGridViewOutOfEdit(datagridview);
							},

							//Changed
							(o, d) => {
								DataGridView datagridview = o as DataGridView;
								if (d.ColumnIndex < 0 || d.RowIndex < 0) {
									return;
								}
								DataGridViewCell cell = datagridview[d.ColumnIndex, d.RowIndex];
								if (cell == null) {
									return;
								}
								IDynamicController controller = cell.OwningColumn.Tag as IDynamicController;
								controller.Changed(cell.OwningRow, cell.OwningColumn);
							},

							//EditStart
							(o, d) => { },

							//EditEnd
							(o, d) => { },

							//ShowEvent
							(o, d) => {
								ComboBox cb = (d.Control as ComboBox);
								if (cb != null) {
									cb.Tag = o;
									EventHandler eh = null;
									eh = new EventHandler((oo, ee) => {
										ForceDataGridViewOutOfEdit((oo as ComboBox).Tag as DataGridView);

										//cb.BeginInvoke(new Action<ComboBox, EventHandler>((x, y) => {
										//    x.SelectedIndexChanged -= y;
										//}), cb, eh);
									});
									cb.SelectedIndexChanged += eh;
								}
							}
							);

			dgv.CellContentClick += handlers.Clicked;
			dgv.CellValueChanged += handlers.Changed;
			dgv.CellBeginEdit += handlers.EditStart;
			dgv.CellEndEdit += handlers.EditEnd;
			dgv.EditingControlShowing += handlers.ShowEvent;

			dgv.Tag = handlers;
		}

		private static void RemoveHandlers(DataGridView dgv) {
			CellHandlers oldhandlers = dgv.Tag as CellHandlers;
			if (oldhandlers != null) {
				dgv.CellContentClick -= oldhandlers.Clicked;
				dgv.CellValueChanged -= oldhandlers.Changed;
				dgv.CellBeginEdit -= oldhandlers.EditStart;
				dgv.CellEndEdit -= oldhandlers.EditEnd;
				dgv.EditingControlShowing -= oldhandlers.ShowEvent;
			}
		}

		private static void UpdateDataGrid(DataGridView dgv, Func<DataGridViewRow, Color> getColor) {

			foreach (DataGridViewRow row in dgv.Rows) {
				foreach (DataGridViewCell cell in row.Cells) {
					cell.Style.BackColor = getColor(row);
					IDynamicController controller = cell.OwningColumn.Tag as IDynamicController;
					controller.Update(cell.OwningRow, cell.OwningColumn);
				}
			}
		}

		private static void InsertCollection<T>(IEnumerable<T> coll, DataGridView dgv) {

			foreach (T item in coll) {
				int index = dgv.Rows.Add();
				DataGridViewRow row = dgv.Rows[index];
				row.Tag = item;
			}
		}

		public static void UpdateDataGridView<T>(IEnumerable<T> newset, DataGridView dgv, Func<T, string> keyExtractor, Func<DataGridViewRow, Color> colorer = null) {

			RemoveHandlers(dgv);

			IDictionary<string, T> fresh = newset.ToDictionary(x => keyExtractor(x));

			Dictionary<string, T> currentSet = new Dictionary<string, T>();
			foreach (DataGridViewRow row in dgv.Rows) {
				currentSet[keyExtractor((T)row.Tag)] = (T)row.Tag;
			}

			IDictionary<string, T> toInsert = newset
				.Where(x => !currentSet.ContainsKey(keyExtractor(x)))
				.ToDictionary(x => keyExtractor(x));

			IDictionary<string, T> toRemove = currentSet
				.Where(x => !newset.Any(y => x.Key == keyExtractor(y)))
				.ToDictionary(x => x.Key, x => x.Value);

			List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();
			foreach (DataGridViewRow row in dgv.Rows) {

				string key = keyExtractor((T)row.Tag);
				if (toRemove.ContainsKey(key)) {
					rowsToRemove.Add(row);
				} else {
					row.Tag = fresh[key];
				}
			}

			Func<DataGridViewRow, Color> getColorFunc = colorer ?? GetRowColorer<T>();

			rowsToRemove.ForEach(x => dgv.Rows.Remove(x));
			InsertCollection(toInsert.Select(x => x.Value), dgv);

			UpdateDataGrid(dgv, getColorFunc);

			ApplyHandlers(dgv);
		}

		private static Func<DataGridViewRow, Color> GetRowColorer<T>() {
			// Find the color attribute
			var colorprop = typeof(T)
				.GetProperties()
				.FirstOrDefault(
				x => x.GetCustomAttributes(typeof(RowColorAttribute), false).Length != 0);

			Func<DataGridViewRow, Color> getColorFunc = x => Color.White;
			if (colorprop != null) {
				getColorFunc = x => (Color)colorprop.GetValue(x.Tag, null);
			}
			return getColorFunc;
		}

		private static void ForceDataGridViewOutOfEdit(DataGridView datagridview) {

			//Console.WriteLine("Force");
			datagridview.BeginInvoke(new Action(() => {
				datagridview.EndEdit();
				datagridview.EndEdit();
				datagridview.EndEdit();
				datagridview.EndEdit();
				datagridview.EndEdit();
			}));
			datagridview.BeginInvoke(new Action(() => {
				datagridview.EndEdit();
			}));
			datagridview.BeginInvoke(new Action(() => {
				datagridview.EndEdit();
			}));
			datagridview.EndEdit();
		}

		public static DataGridViewRow FindRow(DataGridView dgv, string columnName, object stuff) {
			foreach (DataGridViewRow row in dgv.Rows) {
				if (row.Cells[columnName].Value.Equals(stuff)) {
					return row;
				}
			}
			return null;
		}

		public static void SetRowColor(DataGridViewRow row, Color color) {
			foreach (DataGridViewCell cell in row.Cells) {
				cell.Style.BackColor = color;
			}
		}

		public static void Funnel<TSrc, TRecv>(TSrc source, ref TRecv reciever) {

			Type sourceType = source.GetType();
			Type recvType = reciever.GetType();

			if (sourceType.IsArray && recvType.IsArray) {
				object srcBox = (object)source;
				Array srcArr = (Array)srcBox;
				reciever = (TRecv)(object)Array.CreateInstance(recvType.GetElementType(), srcArr.Length);
				object recvBox = (object)reciever;
				Array recvArr = (Array)recvBox;
				for (int i = 0; i < srcArr.Length; i++) {
					object val = Activator.CreateInstance(recvType.GetElementType());
					Funnel(srcArr.GetValue(i), ref val);
					recvArr.SetValue(val, i);
				}
				return;
			}

			FieldInfo[] fis = recvType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			foreach (FieldInfo fi in fis) {
				object value;
				Type srcMemberType;

				FieldInfo srcField = sourceType.GetField(fi.Name);
				if (srcField == null) {
					PropertyInfo srcProp = sourceType.GetProperty(fi.Name);
					value = srcProp.GetValue(source, null);
					srcMemberType = srcProp.PropertyType;
				} else {
					value = srcField.GetValue(source);
					srcMemberType = srcField.FieldType;
				}

				if (srcMemberType != fi.FieldType) {
					object field = Activator.CreateInstance(fi.FieldType);
					Funnel(value, ref field);
					fi.SetValue(reciever, field);
				} else {
					fi.SetValue(reciever, value);
				}
			}
		}


		const int WM_USER = 0x400;
		const int PBM_SETSTATE = WM_USER + 16;

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

		public static void ChangeProgressBarColor(ProgressBar pb, ProgressBarColor color) {
			SendMessage(pb.Handle, PBM_SETSTATE, new IntPtr((int)color), IntPtr.Zero);
		}

		public static void TryParse(string s, Action<int> parsed) {
			int n;
			if (int.TryParse(s, out n)) {
				parsed(n);
			}
		}

		[DllImport("user32.dll")]
		private static extern bool HideCaret(IntPtr hWnd);

		public static void HideCaret(TextBox tb) { HideCaret(tb.Handle); }
		public static void HideCaret(RichTextBox tb) { HideCaret(tb.Handle); }

		private const uint ECM_FIRST = 0x1500;
		private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

		public static void SetWatermark(this TextBox textBox, string watermarkText) {
			SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermarkText);
		}

	}

	public enum ProgressBarColor {
		PBST_NORMAL = 1,
		PBST_ERROR,
		PBST_PAUSED,
	}
}
