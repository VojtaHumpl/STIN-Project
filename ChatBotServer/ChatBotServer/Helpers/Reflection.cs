﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Helpers {
	internal static class Reflection {
        
        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class {
            List<T> objects = new();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))) {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            return objects;
        }
        
    }
}
