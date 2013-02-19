using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Stalker {
	public partial class Form1 {
		[Serializable]
		public struct LoginInfo {
			public string user;
			public string enc_pass;
			public string pass {
				get {
					RijndaelManaged rm = GetRM();
					using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(enc_pass)))
					using (CryptoStream cs = new CryptoStream(ms, rm.CreateDecryptor(), CryptoStreamMode.Read))
					using (StreamReader sr = new StreamReader(cs)) {
						return sr.ReadLine();
					}
				}

				set {
					RijndaelManaged rm = GetRM();
					using (MemoryStream ms = new MemoryStream())
					using (CryptoStream cs = new CryptoStream(ms, rm.CreateEncryptor(), CryptoStreamMode.Write))
					using (StreamWriter sw = new StreamWriter(cs)) {
						sw.WriteLine(value);
						sw.Flush();
						cs.FlushFinalBlock();
						ms.Flush();
						enc_pass = Convert.ToBase64String(ms.GetBuffer());
					}
				}
			}

			private static RijndaelManaged GetRM() {
				RijndaelManaged rm = new RijndaelManaged();
				rm.Padding = PaddingMode.Zeros;
				if (!File.Exists("key")) {
					rm.GenerateKey();
					using (FileStream fs = new FileStream("key", FileMode.Create)) {
						fs.Write(rm.Key, 0, rm.Key.Length);
					}
				}
				using (FileStream fs = new FileStream("key", FileMode.Open)) {
					byte[] key = new byte[(int)fs.Length];
					fs.Read(key, 0, key.Length);
					rm.Key = key;
				}

				if (!File.Exists("iv")) {
					rm.GenerateIV();
					using (FileStream fs = new FileStream("iv", FileMode.Create)) {
						fs.Write(rm.IV, 0, rm.IV.Length);
					}
				}
				using (FileStream fs = new FileStream("iv", FileMode.Open)) {
					byte[] iv = new byte[(int)fs.Length];
					fs.Read(iv, 0, iv.Length);
					rm.IV = iv;
				}

				return rm;
			}
			public string url;
		}
	}
}
