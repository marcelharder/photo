namespace fotoservice.data.dtos;

public class UserForUpdateDto
    {
        [Required]
        public required string UserName { get; set; }
        public required int UserId {get; set;}
        public required string Email {get; set;}
        public required string Gender {get; set;}
        public required string PhoneNumber {get; set;}
        public required int[] AllowedToSee {get; set;}


       
       

    }