namespace CommunicationNamespace
{
    public struct CommunicationItem
    {
        public float mass;
        public float speed;
        private string typeOfCollision;
        public float getMass()
        {
            return mass;
        }

        public float getSpeed()
        {
            return speed;
        }

        public void setType(string type)
        {
            typeOfCollision = type;
        }

        public string getType()
        {
            return typeOfCollision;
        }
    }

    public struct CommunicationSetup
    {
        public bool isMass;
        public float value;
        
    }

   
}