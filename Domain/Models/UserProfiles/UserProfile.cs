using Domain.Models.BaseEntities;

namespace Domain.Models.UserProfiles
{
    public class UserProfile : BaseEntity<Guid>
    {
        private UserProfile()
        {

        }
        public string IdentityUserId { get; private set; } 
        public BasicInfo BasicInfo { get; private set; } 




        //factory method to create a new UserProfile instance
        public static UserProfile Create(string identityUserId, BasicInfo basicInfo)
        {
            return new UserProfile
            {
                //TO Do:Add Validation,error handling ,error notification


                //Id = Guid.NewGuid(),
                IdentityUserId = identityUserId,
                BasicInfo = basicInfo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };
        }


        //public method to update the BasicInfo of the UserProfile
        public void UpdateBasicInfo(BasicInfo basicInfo)
        {
            //TO Do:Add Validation,error handling ,error notification
            BasicInfo = basicInfo;
            UpdatedAt = DateTime.UtcNow;
        }




    }
}
