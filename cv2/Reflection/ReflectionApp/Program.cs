using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace ReflectionApp
{
	public class ColumnNameAttribute : Attribute
	{
        public string Name { get; }
		public ColumnNameAttribute(string name)
		{
            Name = name;
        }

    }

    public class TableNameAttribute : Attribute
    {
        public string Name { get; }
        public TableNameAttribute(string name)
        {
            Name = name;
        }

    }

    public class Person
	{
		[ColumnName("Jmeno")]
        public string Name { get; set; }
        public int Age { get; set; }
    }

	[TableName("Auto")]
    public class Car
    {
        public int Speed { get; set; }
    }

    public class Program
	{
		public static string CreateInsert(object obj)
		{
			Type type = obj.GetType();

			TableNameAttribute tableAttr = type.GetCustomAttribute<TableNameAttribute>();
			string tableName = tableAttr?.Name ?? type.Name;
			//if (tableAttr != null) tableName = tableAttr.Name;

			string sql = "INSERT INTO " + tableName + " (";

			sql += string.Join(", ", type.GetProperties().Select(x => {
				ColumnNameAttribute attr = x.GetCustomAttribute<ColumnNameAttribute>();
				if (attr?.Name != null) return attr.Name;
				return x.Name;
			}));
			sql += ") VALUES (";

			sql += string.Join(", ", type.GetProperties().Select(x => {
				object value = x.GetValue(obj);
				//if (value.GetType() == typeof(string))
				if (value is string)
				{
					return $"\"{value}\"";
				}
				return value;
			}));
			sql += ")";

			return sql;
		}


		public static void Main(string[] args)
		{
			/**
			 * 
			 *	SQL
			 * 
			 */

			Console.WriteLine(CreateInsert(new Person() { Name = "Jan", Age = 67 }));
			Console.WriteLine(CreateInsert(new Car() { Speed = 167 }));



            /**
			 * 
			 *	end SQL
			 * 
			 */


            /*//Type t = typeof(Program);


			string baseDir = Path.GetFullPath("../../../plugins/Debug/net9.0");

			// cesty k DLL souborům
			string[] dlls = Directory.GetFiles(baseDir, "*.dll");


			// LINQ lmao
			// 1. načíst všechny DLL
			Assembly[] assemblies = dlls.Select(x => Assembly.LoadFile(x)).ToArray();

			// 2. získat všechny typy z těchto DLL končící "Calculator"
			Type[] types = assemblies
				.SelectMany(x => x.GetTypes())
				.Where(x => x.Name.EndsWith("Calculator"))
				.ToArray();

			//3. vypsat názvy typů + číslo a nechat uživatele vybrat číslo
			for (int i = 0; i < types.Length; i++)
			{
				Console.WriteLine($"{i}: {types[i].Name}");
			}
			int index = int.Parse(Console.ReadLine());

			Assembly assembly = types[index].Assembly;
			Type calcType = types[index];



            /*string rectangleDllPath = Path.Combine(baseDir, "Rectangle.dll");
			Assembly assembly = Assembly.LoadFile(rectangleDllPath);
			Type calcType = assembly.GetType("Rectangle.RectangleCalculator");*/

            /*Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				Console.WriteLine(type.FullName);
			}


			MethodInfo[] minfos = calcType.GetMethods();
			foreach (MethodInfo mi in minfos)
			{
				Console.WriteLine($"{mi.ReturnType} {mi.Name}");
				foreach(ParameterInfo pi in mi.GetParameters())
				{
					Console.WriteLine($" - {pi.ParameterType} - {pi.Name}");
				}
			}

			Console.WriteLine("--------------------");
			foreach(PropertyInfo pi in calcType.GetProperties())
			{
				Console.WriteLine($"{pi.PropertyType} {pi.Name}");
			}
			Console.WriteLine("--------------------");
			foreach(FieldInfo fi in calcType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
			{
				Console.WriteLine($"{fi.FieldType} {fi.Name}");
			}*/




            //object instance = assembly.CreateInstance("Rectangle.RectangleCalculator");	// instance rigtendž kalkulejtr
            //object instance = assembly.CreateInstance(calcType.FullName);					// taky instance rigtendž kalkulejtr
            /*object instance = Activator.CreateInstance(calcType);                           // taky taky instance rigtendž kalkulejtr

            foreach (PropertyInfo pi in calcType.GetProperties())
            {
                Console.WriteLine(pi.Name);
				double tmp = double.Parse(Console.ReadLine());

				pi.SetValue(instance, tmp);

				Console.WriteLine($"Nastavená hodnota: {pi.GetValue(instance)}");
            }

			MethodInfo getAreaMi = calcType.GetMethod("GetArea", new Type[0]);     // hledá shit bez parametrů
			MethodInfo getAreaMi2 = calcType.GetMethod("GetArea", new Type[] { typeof(string) });

			double result = (double)getAreaMi.Invoke(instance, new object[0]);
			string result2 = (string)getAreaMi2.Invoke(instance, new object[] { "cm" });

			Console.WriteLine(result);
			Console.WriteLine(result2);
			
			*/

        }



    }
}