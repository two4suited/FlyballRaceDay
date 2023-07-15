using System;

namespace BlazorApp.Shared
{
    public class Ring
    {
        public string Id => Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Color { get; set; }
    }
}