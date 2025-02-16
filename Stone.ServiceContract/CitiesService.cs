

namespace Stone.ServiceContract
{
    public class CitiesService : ICitiesService
    {
        public Guid ServiceInstanceID 
        
        { get 
            {
                return _serviceInstanceID;
            } 
        }

        private Guid _serviceInstanceID;


        private List<string> _cities;

        public CitiesService()
        {
            _serviceInstanceID = Guid.NewGuid();

            _cities = new List<string>()
            {
                "Lodon",
                "Paris",
                "New York"
            };

        }


        public List<string> GetCities()
        {
            return _cities;
        }
    }
}
