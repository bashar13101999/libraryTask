using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities {
    public class Category {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;

        protected Category() { }

        public Category(string name) {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void UpdateName(string name) {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }
    }
}
