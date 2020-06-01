using System;

namespace RTM.FormXamarin.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string Icon { get; set; }
        public Type TargetType { get; set; }

    }
}