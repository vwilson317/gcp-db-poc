using System;
using System.Linq;

namespace ClassLibrary1
{
    public static class CustomTypeExtensions
    {
        public static string CollectionName(this Type obj)
        {
            var propertyInfos = obj.BaseType.GetProperties();
            var collectionNamePropInfo = propertyInfos.FirstOrDefault(x => x.Name == nameof(FirestoreCollection.CollectionName));
            if (collectionNamePropInfo == null)
            {
                throw new NotImplementedException($"{nameof(CollectionName)} not implemented for type {obj.GetType()}");
            }

            return collectionNamePropInfo.GetValue(obj).ToString();
        }
    }
}