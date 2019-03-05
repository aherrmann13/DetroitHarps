namespace DetroitHarps.Api.Swagger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class AssignPropertyRequiredFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var requiredProperties = context.SystemType.GetProperties()
                .Where(x => x.PropertyType.IsValueType && !IsNullable(x.PropertyType))
                .Select(t => char.ToLowerInvariant(t.Name[0]) + t.Name.Substring(1));

            schema.Required = schema.Required ?? new List<string>();

            schema.Required = schema.Required.Union(requiredProperties).ToList();
        }

        private bool IsNullable(Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}
