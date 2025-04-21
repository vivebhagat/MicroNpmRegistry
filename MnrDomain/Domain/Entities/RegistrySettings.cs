namespace MicroNpmRegistry.Domain.Entities
{
    public class RegistrySettings
    {
        public string LocalStaoragePath { get; set; }
        public string OrganizationName { get; set; }
    }

    public class StorageConfiguration
    {
        public string Type { get; set; }
        public string rootPath { get; set; }
    }
}
