using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingFromAssebly(assembly);//assembly - это наша сборка, которую нужно передать в конструктор.


        private void ApplyMappingFromAssebly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()  //сканируем по сборке все типы, реализующие IMapWith<>
                .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types) //в каждом типе вызываем метод mapping
            {
                var instance = Activator.CreateInstance(type); //вызываем конструктор метода (создаем инстанс)
                var methodInfo = type.GetMethod("Mapping"); //Находим нужный метод
                methodInfo?.Invoke(instance, new object[] { this }); //Вызываем метод, передавая в параметры текущий объект профиля
            }
        }
    }
}
