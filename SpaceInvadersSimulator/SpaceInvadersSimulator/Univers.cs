using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Threading;
using System.Reflection;

namespace SpaceInvadersSimulator
{
    class Univers
    {
        private String urlDir= @"C:\Users\Sebastian O. Sas\Documents\Visual Studio 2015\Projects\SpaceInvadersSimulator\SpaceInvadersSimulator\Beings";
        private List<String> wasRead = new List<String>();

        private void searchInUnivers()
        {
            Object objectBeing = null;

            while (true)
            {
                foreach (var being in Directory.GetFiles(urlDir, "*.cs"))
                {
                    String beingName = being.Substring(being.LastIndexOf("\\") + 1);


                    IEnumerable<TypeInfo> beingType = getObject(being);

                    if (beingType == null)
                    {
                        continue;
                    }


                    if (wasRead.Contains(beingName))
                        continue;

                    wasRead.Add(beingName);

                    objectBeing = createBeing(beingType);

                    Thread th = new Thread(() => beingAction(objectBeing));
                    th.Start();
                }
            }
        }
        public void runUnivers()
        {
            searchInUnivers();
        }
        private IEnumerable<System.Reflection.TypeInfo> getObject(String being)
        {
            IEnumerable<TypeInfo> beingTypes=null;

            //get ready for compile using CompilerParameters
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;

            //create compilerAssembly and compile
            var provider = new CSharpCodeProvider();
            try
            {
                String beingCode = File.ReadAllText(being);
                var results = provider.CompileAssemblyFromSource(parameters, beingCode);


                //using reflection get being Type(get object Type)
                var assembly = results.CompiledAssembly;
                beingTypes = assembly.DefinedTypes;

                if (!wasRead.Contains(beingTypes.First().Name+ ".cs"))
                    Console.WriteLine("The Univers observed a new being, it's called: " + beingTypes.First().Name);
            }
            catch
            {

            }

            return beingTypes;
        }
        private void beingAction(object being)
        {
            Boolean beingDied = false;
            while (true)
            {
                foreach (var method in being.GetType().GetMethods())
                {
                    if (method.Name.Equals("aging"))
                    {
                        #region call aging
                        int value = (int)method.Invoke(being, null);
                        if (value <= 0)
                        {
                            Console.WriteLine(being.GetType().Name + " has died.");
                            beingDied = true;
                            break;
                        }
                        else
                        {
                            Thread.Sleep(value * 1000);
                            Console.WriteLine(being.GetType().Name + " has aged with " + value + " year/s");
                        }
                        #endregion
                    }
                    else
                    {
                        Random rand = new Random();
                        if (rand.Next(1, 6) / 2 == 0 && (
                            !method.Name.Equals("ToString") && !method.Name.Equals("Equals") && 
                            !method.Name.Equals("GetHashCode") &&
                            ! method.Name.Equals("GetType")))
                        {
                            method.Invoke(being, null);
                        }
                    }
                }

                if(beingDied)
                {
                    break;
                }
            }
        }
        private Object createBeing(IEnumerable<TypeInfo> being)
        {
            TypeInfo typeBeingInfo = being.First();
            var instance = typeBeingInfo.Assembly.CreateInstance(typeBeingInfo.FullName);

            return instance;
        }
    }
}
