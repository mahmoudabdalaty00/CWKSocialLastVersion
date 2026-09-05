using Admin.Registers.Interface;

namespace Admin.Extensions
{
    /// <summary>
    /// Provides extension methods to automate the registration of services and middleware.
    /// This removes the need to manually add every new register class to Program.cs.
    /// </summary>
    public static class RegisterExtensions
    {
        /// <summary>
        /// Scans the assembly to find all classes implementing IWebApplicationBuilderRegister
        /// and executes their service registration logic.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder from Program.cs</param>
        /// <param name="scanningType">A type inside the assembly to scan (usually typeof(Program))</param>
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            // Use the helper method to find all "Service" registrars
            var registrars = GetRegistrars<IWebApplicationBuilderRegister>(scanningType);

            foreach (var registrar in registrars)
            {
                // Run the 'RegisterServices' method defined in each specific class (e.g., MvcRegister)
                registrar.RegisterServices(builder);
            }
        }

        /// <summary>
        /// Scans the assembly to find all classes implementing IWebApplicationRegister
        /// and executes their middleware/pipeline configuration logic.
        /// </summary>
        /// <param name="app">The WebApplication instance from Program.cs</param>
        /// <param name="scanningType">A type inside the assembly to scan (usually typeof(Program))</param>
        public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
        {
            // Use the helper method to find all "Pipeline" registrars
            var registrars = GetRegistrars<IWebApplicationRegister>(scanningType);

            foreach (var registrar in registrars)
            {
                // Run the 'RegisterPipelineComponents' method defined in each specific class (e.g., SessionRegister)
                registrar.RegisterPipelineComponents(app);
            }
        }

        /// <summary>
        /// The 'Magic' behind the scanner: It uses Reflection to find classes that match the interface T.
        /// </summary>
        /// <typeparam name="T">The interface type we are looking for (e.g., IRegistrar)</typeparam>
        /// <param name="scanningType">The entry point type to determine which assembly to scan</param>
        /// <returns>A list of instantiated classes that implement interface T</returns>
        private static IEnumerable<T> GetRegistrars<T>(Type scanningType)
           where T : IRegister
        {
            // 1. Get the Assembly where the scanningType (Program) lives
            // 2. Get all Types (Classes) in that assembly
            // 3. Filter for classes that:
            //    - Are assignable to the interface T
            //    - Are NOT abstract (we can't instantiate abstract classes)
            //    - Are NOT interfaces themselves
            return scanningType.Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                // 4. Create an actual instance of the class (like 'new MvcRegister()')
                .Select(Activator.CreateInstance)
                // 5. Cast it to the interface T so we can call the methods defined in that interface
                .Cast<T>();
        }
    }
}
