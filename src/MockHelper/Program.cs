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
            foreach (var file in GetDlls())
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

        private static IEnumerable<string> GetDlls()
        {
            var reader = new StreamReader(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "mock.txt"));
            var dlls = reader.ReadToEnd();
            var paths = dlls.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var file in paths)
            {
                var info = new FileInfo(file);
                var ext = Path.GetExtension(info.Name).ToLower();
                if (ext != ".dll" && ext != ".exe") { continue; }
                yield return info.FullName;
            }
        }

        private static bool HasSymbols(string dll)
        {
            var pdb = Path.GetFileNameWithoutExtension(dll) + ".pdb";
            return File.Exists(pdb);
        }

        private static void OverWrite(string args, bool hasSymbols)
        {
            var asmDef = AssemblyDefinition.ReadAssembly(args, new ReaderParameters { ReadSymbols = hasSymbols });
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
                    method.IsFinal = false;
                    method.IsPrivate = false;
                    method.IsVirtual = true;
                    method.IsPublic = true;
                }

                foreach (var prop in type.Properties)
                {
                    if (prop.SetMethod != null)
                    {
                        prop.SetMethod.IsPrivate = false;
                        prop.SetMethod.IsFinal = false;
                        prop.SetMethod.IsVirtual = true;
                        prop.SetMethod.IsPublic = true;
                    }
                    if (prop.GetMethod != null)
                    {
                        prop.GetMethod.IsPrivate = false;
                        prop.GetMethod.IsFinal = false;
                        prop.GetMethod.IsVirtual = true;
                        prop.GetMethod.IsPublic = true;
                    }
                }

                foreach (var field in type.Fields)
                {
                    if (field.IsStatic) continue;
                    field.IsPrivate = false;
                    field.IsInitOnly = false;
                    field.IsPublic = true;
                }
            }

            asmDef.Write(args, new WriterParameters
            {
                WriteSymbols = hasSymbols
            });
        }
    }
}
