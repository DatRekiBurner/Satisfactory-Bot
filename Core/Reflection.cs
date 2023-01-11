using System.Reflection;

namespace SatisfactoryBot.Core
{
    internal class Reflection
    {
        /// <summary>
        /// Initialize all classes in the application if they inherit/have a specific class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of initialized classes</returns>
        public static List<T> InitializeClasses<T>()
        {
            List<T> InitializedClasses = new();
            // If namespace contains '+' it's a sub class. We don't want to register these
            IEnumerable<Type> classes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(T)) && t.FullName != null && !t.FullName.Contains('+'));

            foreach (Type instance in classes)
            {
                if (CreateInstance(instance, out T createdInstance))
                    InitializedClasses.Add(createdInstance);
            }

            return InitializedClasses;
        }

        /// <summary>
        /// Create a generic instance from a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Instance">The instance you want to create</param>
        /// <param name="result">If function returns true this will output the initialized instance</param>
        /// <returns>Boolean which indicates if it was able to create the instance</returns>
        private static bool CreateInstance<T>(Type Instance, out T result)
        {
            T? created = (T?)Activator.CreateInstance(Instance);
            if (created == null)
            {
                result = Activator.CreateInstance<T>();
                return false;
            }
            else
            {
                result = created;
                return true;
            }
        }
    }
}
