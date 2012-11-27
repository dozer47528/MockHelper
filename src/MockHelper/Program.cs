using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Net;
using Mono.Cecil;
using System.IO;
using System.Diagnostics;

namespace MockHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = (args == null || args.Length == 0) ? string.Empty : args[0];
            foreach (var file in GetDlls(root))
            {
                try
                {
                    OverWrite(file, HasSymbols(file));
                    Debug.WriteLine(string.Concat(file, ":Success！"));
                    Console.WriteLine(string.Concat(file, ":Success！"));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(string.Concat(file, ":", e.Message));
                    Console.WriteLine(string.Concat(file, ":", e.Message));
                }
            }
        }

        /// <summary>
        /// read dll and exe from Mock.txt
        /// </summary>
        /// <param name="root">root path(can be empty)</param>
        /// <returns></returns>
        private static IEnumerable<string> GetDlls(string root)
        {
            var reader = new StreamReader(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "mock.txt"));
            var dlls = reader.ReadToEnd();
            var paths = dlls.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var file in paths)
            {
                var info = new FileInfo(Path.Combine(root ?? string.Empty, file));
                var ext = Path.GetExtension(info.Name).ToLower();
                if (ext != ".dll" && ext != ".exe") { continue; }
                yield return info.FullName;
            }
        }

        /// <summary>
        /// has .pdb file?
        /// </summary>
        /// <param name="dll"></param>
        /// <returns></returns>
        private static bool HasSymbols(string dll)
        {
            var pdb = Path.GetFileNameWithoutExtension(dll) + ".pdb";
            return File.Exists(pdb);
        }

        /// <summary>
        /// overwrite file
        /// </summary>
        /// <param name="file">file path</param>
        /// <param name="hasSymbols">has .pdb file</param>
        private static void OverWrite(string file, bool hasSymbols)
        {
            var asmDef = AssemblyDefinition.ReadAssembly(file, new ReaderParameters { ReadSymbols = hasSymbols });
            var classTypes = asmDef.Modules
                                   .SelectMany(m => m.Types)
                                   .Where(t => t.IsClass)
                                   .ToList();

            foreach (var type in classTypes)
            {
                if (type.IsSealed)
                {
                    type.IsSealed = false;
                }

                foreach (var method in type.Methods)
                {
                    if (method.IsStatic) continue;
                    if (method.IsConstructor) continue;
                    if (method.IsAbstract) continue;

                    if (!method.IsVirtual)
                    {
                        method.IsVirtual = true;
                        method.IsNewSlot = true;
                        method.IsReuseSlot = false;
                    }
                    else
                    {
                        method.IsFinal = false;
                    }
                }
            }

            asmDef.Write(file, new WriterParameters { WriteSymbols = hasSymbols });
        }
    }
}
