using System;
using System.Reflection;
using UnityEngine;

namespace SUG.Essentials
{
    internal static class Injector
    {
        public static void Inject(object target)
        {
            if (target == null) return;

            var type = target.GetType();

            var fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                if (!Attribute.IsDefined(field, typeof(EInjectAttribute))) continue;

                var service = ServiceRegistry.Resolve(field.FieldType);

                if (service == null) continue;

                field.SetValue(target, service);
            }
        }
    }
}