

namespace MinhaApi.Domain.employee.entitie
{

       public class Employee{

    
        public Guid Id { get; private set; }
        
        public string Name { get; private set; }

        public int Age { get; private set; }

        public string? Photo { get; private set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public Employee(string name, int age, string photo, string email, string password )
        {
            Id = Guid.NewGuid();
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Age = age;
            this.Photo = photo;
            this.Email = email;
            this.Password = password;

        }

    }
}
