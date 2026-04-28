
namespace MinhaApi.Domain.employee.entitie
{

       public class Employee{

    
        public Guid Id { get; private set; }
        
        public string Name { get; private set; }

        public int Age { get; private set; }

        public string? Photo { get; private set; }

        public string Email { get; private set; }
        public string Password { get; private set; }

        private void Validate(string name, int age, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new ArgumentException("Nome inválido");

            if (age < 16)
                throw new ArgumentException("Idade inválida");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email obrigatório");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new ArgumentException("Senha inválida");
        }

        public void ToUpdate(string name, string email, int age)
        {
            this.Name = name;
            this.Email = email;
            this.Age = age;
        }
        public Employee(string name, int age, string photo, string email, string password )
        {
            Validate(name, age, email, password);
            
            Id = Guid.NewGuid();
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Age = age;
            this.Photo = photo;
            this.Email = email;
            this.Password = password;

        }
    }
}
