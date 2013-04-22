using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Kiln.NET")]
[assembly: AssemblyDescription("Implements API for interacting with brand new version control system - Kiln (http://fogcreek.com/kiln)")]
[assembly: AssemblyCompany("Anton Moiseev")]
[assembly: AssemblyProduct("Kiln.NET")]
[assembly: AssemblyCopyright("Copyright © 2011 Anton Moiseev")]
[assembly: ComVisible(false)]
[assembly: Guid("a15c58dc-5e46-427c-b126-aa002b836a98")]

// Versioning convention:
//
// 1. Major and minor version numbers match Kiln API version which the library
//    is intended for.
// 2. The third and fourth numbers are actually the major and minor version of
//    the library.
//    Major number doesn't change frequently. It's incremented only if some
//    meaningful changes are applied. Versions with different major number can
//    be incompatible.
//    Minor version number increments if some bugs were fixed or minor
//    improvements were added.
[assembly: AssemblyVersion("1.0.1.1")]
