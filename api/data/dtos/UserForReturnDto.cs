namespace fotoservice.data.dtos;

public class UserForReturnDto
    {
       
        public  string? UserName { get; set; }
        public string? AllowedToSee {get; set;}
        public  string? Email {get; set;}
        public DateTime Created {get; set;}
        public  string? gender {get; set;}
        public  string? PhoneNumber {get; set;}
        public DateTime paidTill { get; set; } 
        
    }  
       